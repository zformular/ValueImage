using System;
using ValueMathHelper.Infrastructure;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  快速傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        protected void FFT(ref Byte[] data, Int32 width, Int32 height, Boolean inv)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, inv, out tempComp);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (Byte)tempComp[i].Real;
            }
        }

        /// <summary>
        ///  快速傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        protected void FFT(ref Byte[] rgbBytes, Int32 width, Int32 height, Boolean inv, out Complex[] result)
        {
            Int32 length = width * height;
            Byte[] tempBytes = (Byte[])rgbBytes.Clone();
            Complex[] tempComp = new Complex[length];


            for (int i = 0; i < length; i++)
            {
                if (inv)
                {
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        tempComp[i] = new Complex(rgbBytes[i], 0);
                    else
                        tempComp[i] = new Complex(-rgbBytes[i], 0);
                }
                else
                    tempComp[i] = new Complex(rgbBytes[i], 0);
            }

            // 水平方向变化
            Complex[] tempCompH = new Complex[width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = mathHelper.FFT(tempCompH, width);
                for (int j = 0; j < width; j++)
                {
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            // 垂直方向变化
            Complex[] tempCompVe = new Complex[height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = mathHelper.FFT(tempCompVe, height);

                for (int j = 0; j < height; j++)
                {
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }
            result = tempComp;
        }

        /// <summary>
        ///  快速傅里叶你变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        protected void IFFT(ref Complex[] data, Int32 width, Int32 height, Boolean inv, out Byte[] result)
        {
            Int32 length = width * height;
            Complex[] tempComp = (Complex[])data.Clone();
            Complex[] tempCompH = new Complex[width];
            // 水平方向变化
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = mathHelper.IFFT(tempCompH, width);
                for (int j = 0; j < width; j++)
                {
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            Complex[] tempCompVe = new Complex[height];
            // 垂直方向变化
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = mathHelper.IFFT(tempCompVe, height);
                for (int j = 0; j < height; j++)
                {
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }

            result = new Byte[length];
            Double temp = 0;
            // 赋值,保留实数部分
            for (int i = 0; i < length; i++)
            {
                if (inv)
                {
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        temp = tempComp[i].Real;
                    else
                        temp = -tempComp[i].Real;
                }
                else
                    temp = tempComp[i].Real;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;

                result[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  幅度图像
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void amplitude(ref Byte[] data, Int32 width, Int32 height)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Double[] tempData = new Double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                tempData[i] = System.Math.Log(1 + tempComp[i].Abs(), 2);
            }

            #region 灰度拉伸

            Double max = mathHelper.Max(tempData);
            Double min = mathHelper.Min(tempData);
            Double p = 255.0 / (max - min);
            Double temp;
            for (int i = 0; i < data.Length; i++)
            {
                temp = p * (tempData[i] - min) + 0.5;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }

            #endregion
        }

        /// <summary>
        ///  相位图像
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据宽度</param>
        protected void phase(ref Byte[] data, Int32 width, Int32 height)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, false, out tempComp);
            Double[] tempData = new Double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                tempData[i] = tempComp[i].Angle() + 2 * System.Math.PI;
            }

            #region 灰度拉伸

            Double max = mathHelper.Max(tempData);
            Double min = mathHelper.Min(tempData);
            Double p = 255.0 / (max - min);
            Double temp;
            for (int i = 0; i < data.Length; i++)
            {
                temp = p * (tempData[i] - min) + 0.5;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }

            #endregion
        }

        protected void gabor(ref Byte[] data, Double sigma, Double thelta, Double lambda, Double psi, Double gamma, out Byte[] result)
        {
            //Double sigmaX = sigma, sigmaY = sigma / gamma;
            //Int32 nstds = 3;
            //Double xmax


            result = null;
        }

        /// <summary>
        ///  投影
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="orientType">投影的方向(水平或垂直)</param>
        /// <param name="result">投影的结果</param>
        protected void projection(ref Byte[] data, Int32 width, Int32 height, ValueImage.Infrastructure.OrientationType orientType, out Int32[] result)
        {
            if (orientType == Infrastructure.OrientationType.Horizontal)
            {
                result = new Int32[height];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        result[i] += data[i * width + j];
                    }
                    result[i] /= width;
                }
            }
            else if (orientType == Infrastructure.OrientationType.Vertical)
            {
                result = new Int32[width];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        result[i] += data[j * width + i];
                    }
                    result[i] /= height;
                }
            }
            else
            {
                result = new Int32[0];
            }
        }
    }
}