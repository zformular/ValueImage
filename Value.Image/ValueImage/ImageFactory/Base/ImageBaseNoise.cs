using System;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  高斯噪声
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="mean">均值</param>
        /// <param name="meanDeviation">均方差</param>
        protected void gaussNoise(ref Byte[] data, Double mean, Double meanDeviation)
        {
            Random r1, r2;
            r1 = new Random(unchecked((Int32)DateTime.Now.Ticks));
            r2 = new Random(~unchecked((Int32)DateTime.Now.Ticks));

            Double temp;
            Double v1, v2;
            for (int i = 0; i < data.Length; i++)
            {
                do
                {
                    v1 = r1.NextDouble();
                }
                while (v1 <= 0.00000000001);

                v2 = r2.NextDouble();
                temp = System.Math.Sqrt(-2 * System.Math.Log(v1)) * System.Math.Cos(2 * System.Math.PI * v2) * meanDeviation + mean;
                temp += data[i];
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  瑞利噪声
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="paramA">参数A</param>
        /// <param name="paramB">参数B</param>
        protected void rayleighNoise(ref Byte[] data, Double paramA, Double paramB)
        {
            Double v;
            Double temp;
            Random r = new Random(unchecked((Int32)DateTime.Now.Ticks));

            for (int i = 0; i < data.Length; i++)
            {
                do
                {
                    v = r.NextDouble();
                } while (v >= 0.9999999999);

                temp = paramA + System.Math.Sqrt(-1 * paramB * System.Math.Log(1 - v));
                temp += data[i];
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  指数噪声
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="param">参数a(a>0)</param>
        protected void indexNoise(ref Byte[] data, Double param)
        {
            Debug.Assert(param > 0, "参数param必须大于零");
            if (param < 0) return;

            Double v;
            Double temp;
            Random r = new Random(unchecked((Int32)DateTime.Now.Ticks));
            for (int i = 0; i < data.Length; i++)
            {
                do
                {
                    v = r.NextDouble();
                } while (v >= 0.9999999999);
                temp = -1 * System.Math.Log(1 - v) / param;

                temp += data[i];
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  椒盐噪声
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="pepper">椒量</param>
        /// <param name="salt">盐量</param>
        protected void pepperNoise(ref Byte[] data, Double pepper, Double salt)
        {
            Double v;
            Double temp = 0D;
            Random r = new Random(unchecked((Int32)DateTime.Now.Ticks));

            for (int i = 0; i < data.Length; i++)
            {
                v = r.NextDouble();
                if (v <= pepper)
                    temp -= 500;
                else if (v >= (1 - salt))
                    temp = 500;
                else
                    temp = 0;
                temp += data[i];
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }
        }
    }
}