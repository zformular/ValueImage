using System;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  频率
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        protected void frequency(ref Byte[] data, out Int32[] result)
        {
            result = new Int32[256];
            Int32 f = 0;
            for (int i = 0; i < data.Length; i++)
            {
                f = data[i];
                result[f]++;
            }
        }

        /// <summary>
        ///  灰度拉伸
        /// </summary>
        /// <param name="data">二维数据</param>
        protected void grayscaleStretch(ref Byte[] data)
        {
            Int32[] freq;
            this.frequency(ref data, out freq);
            Int32 maxIndex = mathHelper.MaxIndex(freq);
            Int32 minIndex = mathHelper.MinIndex(freq);
            float p = 255.0F / (maxIndex - minIndex);
            Int32 temp = 0;
            for (int i = 0; i < data.Length; i++)
            {
                temp = (Int32)(p * (data[i] - minIndex) + 0.5);
                data[i] = (Byte)temp;
            }
        }

        /// <summary>
        ///  最优阈值化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void optimalThreshold(ref Byte[] data, Int32 width, Int32 height, out Byte[] result)
        {
            float T = 0F;
            float T1 = 0F;
            float utb = 0F;
            float uto = 0F;

            #region 首次迭代
            utb = data[0] + data[width - 1] + data[(height - 1) * width] + data[height * width - 1];
            for (int i = 0; i < data.Length; i++)
            {
                uto += data[i];
            }
            uto = (uto - utb) / (width * height - 4);
            utb = utb / 4;
            T = (utb + uto) / 2;
            #endregion

            #region 持续迭代直至满足条件
            Int32 nub = 0;
            Int32 nuo = 0;
            while (T1 != T)
            {
                utb = uto = nub = nuo = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    Int32 temp = data[i];
                    if (temp < T)
                    {
                        utb += temp;
                        nub++;
                    }
                    else
                    {
                        uto += temp;
                        nuo++;
                    }
                }

                utb = utb / nub;
                uto = uto / nuo;
                T1 = T;
                T = (utb + uto) / 2;
            }

            #endregion

            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                Int32 temp = data[i] >= T ? 255 : 0;
                result[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  获得Ostu阈值化的最佳阈值
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <returns>阈值</returns>
        protected Int32 ostuThreshold(ref Byte[] data, Int32 width, Int32 height)
        {
            Int32[] frequency;
            this.frequency(ref data, out frequency);
            Int32 radix = width * height;
            Int32 length = frequency.Length;
            float[] probability = new float[length];
            for (int i = 0; i < length; i++)
            {
                probability[i] = (float)frequency[i] / radix;
            }

            float max = 0F;
            Int32 index = 0;
            for (int i = 1; i < length; i++)
            {
                float w0 = 0F;
                for (int t = 0; t < i; t++)
                {
                    w0 += probability[i];
                }
                float A = 0F;
                for (int t = 0; t < i; t++)
                {
                    A += probability[i] * i;
                }
                float uA = A / w0;

                float w1 = 1 - w0;
                float B = 0F;
                for (int t = i; t < length; t++)
                {
                    B += probability[i] * i;
                }
                float uB = B / w1;

                float ft = w0 * (uA - i) * (uA - i) + w1 * (uB - i) * (uB - i);
                if (ft > max)
                {
                    max = ft;
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        ///  二值化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="thresholding">阈值</param>
        protected void binarization(ref Byte[] data, Int32 thresholding)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > thresholding)
                    data[i] = 255;
                else
                    data[i] = 0;
            }
        }
    }
}
