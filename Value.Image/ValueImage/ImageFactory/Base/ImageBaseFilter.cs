using System;
using ValueMathHelper.Infrastructure;
using ValueImage.Infrastructure;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {

        #region 傅里叶

        /// <summary>
        ///  二维快速傅里叶变换
        /// </summary>
        /// <param name="rgbBytes">图像序列</param>
        /// <param name="width">图像宽度</param>
        /// <param name="realWidth">图像实际宽度</param>
        /// <param name="height">图像长度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <returns></returns>
        public Complex[] FFT2(Byte[] rgbBytes, Int32 width, Int32 realWidth, Int32 height, Boolean inv)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) return null;

            Int32 length = width * height;
            Byte[] tempBytes = (Byte[])rgbBytes.Clone();
            Complex[] tempComp = new Complex[length];

            for (int i = 0; i < length; i += 3)
            {
                if ((i % width) > realWidth)
                {
                    tempComp[i] = new Complex(rgbBytes[i], 0);
                    tempComp[i + 1] = new Complex(rgbBytes[i + 1], 0);
                    tempComp[i + 2] = new Complex(rgbBytes[i + 2], 0);
                    continue;
                }

                if (inv)
                {
                    if ((i / width + i % realWidth) % 2 == 0)
                    {
                        tempComp[i] = new Complex(rgbBytes[i], 0);
                        tempComp[i + 1] = new Complex(rgbBytes[i + 1], 0);
                        tempComp[i + 2] = new Complex(rgbBytes[i + 2], 0);
                    }
                    else
                    {
                        tempComp[i] = new Complex(-rgbBytes[i], 0);
                        tempComp[i + 1] = new Complex(-rgbBytes[i + 1], 0);
                        tempComp[i + 2] = new Complex(-rgbBytes[i + 2], 0);
                    }
                }
                else
                {
                    tempComp[i] = new Complex(rgbBytes[i], 0);
                    tempComp[i + 1] = new Complex(rgbBytes[i + 1], 0);
                    tempComp[i + 2] = new Complex(rgbBytes[i + 2], 0);
                }
            }

            Int32 singleLength = realWidth / 3;
            Complex[] tempCompHr = new Complex[singleLength];
            Complex[] tempCompHg = new Complex[singleLength];
            Complex[] tempCompHb = new Complex[singleLength];
            // 水平方向变化
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < singleLength; j++)
                {
                    tempCompHb[j] = tempComp[i * width + j * 3];
                    tempCompHg[j] = tempComp[i * width + j * 3 + 1];
                    tempCompHr[j] = tempComp[i * width + j * 3 + 2];
                }
                tempCompHr = mathHelper.FFT(tempCompHr, singleLength);
                tempCompHg = mathHelper.FFT(tempCompHg, singleLength);
                tempCompHb = mathHelper.FFT(tempCompHb, singleLength);

                for (int j = 0; j < singleLength; j++)
                {
                    tempComp[i * width + j * 3] = tempCompHb[j];
                    tempComp[i * width + j * 3 + 1] = tempCompHg[j];
                    tempComp[i * width + j * 3 + 2] = tempCompHr[j];
                }
            }

            Complex[] tempCompVe = new Complex[height];
            // 垂直方向变化
            for (int i = 0; i < realWidth; i++)
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

            return tempComp;
        }

        /// <summary>
        ///  二维快速傅里叶逆变换
        /// </summary>
        /// <param name="freData">频域数据</param>
        /// <param name="width">图像宽度</param>
        /// <param name="realWidth">图像真是宽度</param>
        /// <param name="height">图像长度</param>
        /// <param name="inv">是否进行坐标位移变换,要与二维快速傅里叶正交变换一致</param>
        /// <returns></returns>
        public Byte[] IFFT2(Complex[] freData, Int32 width, Int32 realWidth, Int32 height, Boolean inv)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) return null;

            Int32 length = width * height;
            Complex[] tempComp = (Complex[])freData.Clone();

            Int32 singleLength = realWidth / 3;
            Complex[] tempCompHr = new Complex[singleLength];
            Complex[] tempCompHg = new Complex[singleLength];
            Complex[] tempCompHb = new Complex[singleLength];
            // 水平方向变化
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < singleLength; j++)
                {
                    tempCompHb[j] = tempComp[i * width + j * 3];
                    tempCompHg[j] = tempComp[i * width + j * 3 + 1];
                    tempCompHr[j] = tempComp[i * width + j * 3 + 2];
                }
                tempCompHr = mathHelper.IFFT(tempCompHr, singleLength);
                tempCompHg = mathHelper.IFFT(tempCompHg, singleLength);
                tempCompHb = mathHelper.IFFT(tempCompHb, singleLength);

                for (int j = 0; j < singleLength; j++)
                {
                    tempComp[i * width + j * 3] = tempCompHb[j];
                    tempComp[i * width + j * 3 + 1] = tempCompHg[j];
                    tempComp[i * width + j * 3 + 2] = tempCompHr[j];
                }
            }

            Complex[] tempCompVe = new Complex[height];
            // 垂直方向变化
            for (int i = 0; i < realWidth; i++)
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

            Byte[] rgbBytes = new Byte[length];
            Double tempr = 0, tempg = 0, tempb = 0;
            // 赋值,保留实数部分
            for (int i = 0; i < length; i += 3)
            {
                if (inv)
                {
                    if ((i / width + i % realWidth) % 2 == 0)
                    {
                        tempb = tempComp[i].Real;
                        tempg = tempComp[i + 1].Real;
                        tempr = tempComp[i + 2].Real;
                    }
                    else
                    {
                        tempb = -tempComp[i].Real;
                        tempg = -tempComp[i + 1].Real;
                        tempr = -tempComp[i + 2].Real;
                    }
                }
                else
                {
                    tempb = tempComp[i].Real;
                    tempg = tempComp[i + 1].Real;
                    tempr = tempComp[i + 2].Real;
                }
                tempb = tempb > 255 ? 255 : tempb < 0 ? 0 : tempb;
                tempg = tempg > 255 ? 255 : tempg < 0 ? 0 : tempg;
                tempr = tempr > 255 ? 255 : tempr < 0 ? 0 : tempr;

                rgbBytes[i] = Convert.ToByte(tempb);
                rgbBytes[i + 1] = Convert.ToByte(tempg);
                rgbBytes[i + 2] = Convert.ToByte(tempr);
            }

            return rgbBytes;
        }

        #endregion

        /// <summary>
        ///  低通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="radius">滤波圆周边界(大于该边界都不可通过)</param>
        protected void lowpassFilter(ref Byte[] data, Int32 width, Int32 height, Double radius, out Byte[] result)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) { result = (Byte[])data.Clone(); return; }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            radius = radius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++)
            {
                Int32 row = i / width;
                Int32 col = i % width;
                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);

                if (distance > radius)
                    tempComp[i] = new Complex(0.0, 0.0);
            }

            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  高通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="radius">滤波边界(小于该边界都不可通过)</param>
        protected void highpassFilter(ref Byte[] data, Int32 width, Int32 height, Double radius, out Byte[] result)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) { result = (Byte[])data.Clone(); return; }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            radius = radius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++)
            {
                Int32 row = i / width;
                Int32 col = i % width;

                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);
                if (distance < radius)
                    tempComp[i] = new Complex(0.0, 0.0);
            }

            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  带阻滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        protected void bandstopFilter(ref Byte[] data, Int32 width, Int32 height, Double innerRadius, Double outerRadius, out Byte[] result)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) { result = (Byte[])data.Clone(); return; }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            innerRadius = innerRadius * minLen / 100;
            outerRadius = outerRadius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++)
            {
                Int32 row = i / width;
                Int32 col = i % width;
                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);

                if (distance < outerRadius && distance > innerRadius)
                    tempComp[i] = new Complex(0.0, 0.0);
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  带通滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        /// <param name="result"></param>
        protected void bandpassFilter(ref Byte[] data, Int32 width, Int32 height, Double innerRadius, Double outerRadius, out Byte[] result)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) { result = (Byte[])data.Clone(); return; }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Int32 minLen = System.Math.Min(width, height);
            innerRadius = innerRadius * minLen / 100;
            outerRadius = outerRadius * minLen / 100;

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++)
            {
                Int32 row = i / width;
                Int32 col = i % width;

                Double distance = (Double)((col - medianWidth) * (col - medianWidth) + (row - medianHeight) * (row - medianHeight));
                distance = System.Math.Sqrt(distance);
                if (distance < innerRadius || distance > outerRadius)
                    tempComp[i] = new Complex(0.0, 0.0);
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  方位滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="startOrient">起始方位</param>
        /// <param name="endOrient">终止方位</param>
        protected void orientationFilter(ref Byte[] data, Int32 width, Int32 height, Int32 startOrient, Int32 endOrient, out Byte[] result)
        {
            Debug.Assert(mathHelper.IsPow2(width), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!mathHelper.IsPow2(width)) { result = (Byte[])data.Clone(); return; }

            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);

            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            for (int i = 0; i < data.Length; i++)
            {
                Int32 row = i / width;
                Int32 col = i % width;
                Double currOrient = (System.Math.Atan2(medianHeight - row, col - medianWidth)) * 180 / System.Math.PI;

                // 终止角度小于0
                if (endOrient <= 0)
                {
                    if ((currOrient <= endOrient && currOrient >= startOrient) || (currOrient <= (endOrient + 180) && currOrient >= (startOrient + 180)))
                    {
                        tempComp[i] = new Complex(0.0, 0.0);
                    }
                }
                // 终止角度小于90
                else if (endOrient <= 90)
                {
                    if ((currOrient <= endOrient && currOrient >= startOrient) || (currOrient <= (endOrient - 180) && currOrient > (startOrient + 180)))
                        tempComp[i] = new Complex(0.0, 0.0);
                }
                // 终止角度小于180
                else if (endOrient <= 180)
                {
                    if ((currOrient <= endOrient && currOrient >= startOrient) || (currOrient <= endOrient - 180) && (currOrient >= startOrient - 180))
                        tempComp[i] = new Complex(0.0, 0.0);
                }
                // 终止角度大于180
                else
                {
                    if (((currOrient <= endOrient - 180) && (currOrient >= startOrient - 180)) ||
                   (currOrient <= endOrient - 360 && currOrient >= -180) ||
                   (currOrient >= startOrient && currOrient <= 180))
                        tempComp[i] = new Complex(0.0, 0.0);
                }
            }
            this.IFFT(ref tempComp, width, height, true, out result);
        }

        /// <summary>
        ///  均值滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="type">滤波窗口大小</param>
        protected void meanFilter(ref Byte[] data, Int32 width, Int32 height, TemplateType type, out Byte[] result)
        {
            result = new Byte[data.Length];
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                set = getFilterWindow(i, width, height, type);
                Int32 average = 0;
                for (int j = 0; j < set.Length; j++)
                {
                    average += data[set[j]];
                }
                average /= set.Length;
                average = average > 255 ? 255 : average < 0 ? 0 : average;
                result[i] = Convert.ToByte(average);
            }
        }

        /// <summary>
        ///  中值滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="type">滤波窗口类型</param>
        protected void medianFilter(ref Byte[] data, Int32 width, Int32 height, TemplateType type, out Byte[] result)
        {
            result = new Byte[data.Length];
            System.Collections.Generic.IList<Int32> list = new System.Collections.Generic.List<Int32>();
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                set = getFilterWindow(i, width, height, type);
                for (int j = 0; j < set.Length; j++)
                {
                    list.Add(data[set[j]]);
                }
                mathHelper.BubbleSort(list, SortMode.Ascending);
                Int32 median = list[set.Length / 2];
                result[i] = Convert.ToByte(median);
                list.Clear();
            }
        }

        /// <summary>
        ///  高斯平滑
        /// </summary>
        /// <param name="borderLength">二维数据边长(必须是正方形)</param>
        /// <param name="sigma">方差</param>
        protected void gaussSmooth(ref Byte[] data, Int32 borderLength, Double sigma, out Byte[] result)
        {
            if (data.Length / borderLength != borderLength)
                Debug.Fail("必须输入正方形二维数据");

            // 方差
            Double std2 = 2 * sigma * sigma;
            // 半径=3sigma(方差的3倍)
            Int32 radius = Convert.ToInt16(System.Math.Ceiling(3 * sigma));
            Int32 filterWidth = 2 * radius + 1;
            Double[] filter = new Double[filterWidth];
            Double[] tempData = new Double[data.Length];

            Double sum = 0;
            // 产生一维高斯函数
            for (int i = 0; i < filterWidth; i++)
            {
                Int32 xx = (i - radius) * (i - radius);
                filter[i] = System.Math.Exp(-xx / std2);
                sum += filter[i];
            }
            // 归一化
            for (int i = 0; i < filterWidth; i++)
            {
                filter[i] = filter[i] / sum;
            }
            // 水平方向滤波
            for (int i = 0; i < borderLength; i++)
            {
                for (int j = 0; j < borderLength; j++)
                {
                    Double temp = 0;
                    for (int k = -radius; k <= radius; k++)
                    {
                        // 循环延拓
                        Int32 rem = (System.Math.Abs(j + k)) % borderLength;
                        // 计算卷积和
                        temp += data[i * borderLength + rem] * filter[k + radius];
                    }
                    tempData[i * borderLength + j] = temp;
                }
            }

            // 垂直方向滤波
            result = new Byte[data.Length];
            for (int j = 0; j < borderLength; j++)
            {
                for (int i = 0; i < borderLength; i++)
                {
                    Double temp = 0;
                    for (int k = -radius; k <= radius; k++)
                    {
                        // 循环延拓
                        Int32 rem = (System.Math.Abs(i + k)) % borderLength;
                        // 计算卷积和
                        temp += tempData[rem * borderLength + j] * filter[k + radius];
                    }

                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i * borderLength + j] = Convert.ToByte(temp);
                }
            }
        }

        /// <summary>
        ///  统计方法滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值</param>
        /// <param name="type">滤波窗口大小</param>
        protected void statisticFilter(ref Byte[] data, Int32 width, Int32 height, Double thresholding, TemplateType type, out Byte[] result)
        {
            result = (Byte[])data.Clone();
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                Double mu = 0, sigma = 0;
                set = getFilterWindow(i, width, height, type);
                for (int j = 0; j < set.Length; j++)
                {
                    mu += data[set[j]];
                }
                mu /= set.Length;

                // 计算sigma
                for (int j = 0; j < set.Length; j++)
                {
                    sigma += System.Math.Pow(data[set[j]] - mu, 2);
                }
                sigma = System.Math.Sqrt(sigma / set.Length);

                // 过滤
                if (System.Math.Abs(data[i] - mu) < sigma * thresholding)
                    result[i] = data[i];
                else
                    result[i] = Convert.ToByte(mu);
            }
        }
    }
}