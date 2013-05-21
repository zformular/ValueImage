using System;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  Roberts算子边缘锐化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值(阈值为零的话, 不进行二值化)</param>
        protected void robertsEdge(ref Byte[] data, Int32 width, Int32 height, Int32 thresholding, out Byte[] result)
        {
            Double[] tempArray = new Double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                Double gradX = 0, gradY = 0;
                Int32[] set = getFilterWindow2x2(i, width, height);
                for (int j = 0; j < set.Length; j++)
                {
                    gradX += data[set[j]] * OperatorSet.robertOperatorX[j];
                    gradY += data[set[j]] * OperatorSet.robertOperatorY[j];
                }
                tempArray[i] = System.Math.Sqrt(gradX * gradX + gradY * gradY);
            }

            // 判断是否进行二值化
            #region 是否二值化并赋值
            result = new Byte[data.Length];
            if (thresholding == 0)
            {
                // 阈值为0时不进行二值化
                for (int i = 0; i < data.Length; i++)
                {
                    Double temp = tempArray[i];
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i] = Convert.ToByte(temp);
                }
            }
            else
            {
                // 二值化
                for (int i = 0; i < data.Length; i++)
                {
                    if (tempArray[i] > thresholding)
                        result[i] = 255;
                    else
                        result[i] = 0;
                }
            }
            #endregion
        }

        /// <summary>
        ///  Prewitt算子边缘锐化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值(阈值为零的话, 不进行二值化)</param>
        protected void prewittEdge(ref Byte[] data, Int32 width, Int32 height, Int32 thresholding, out Byte[] result)
        {
            Double[] tempArray = new Double[data.Length];
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                Double gradX = 0, gradY = 0;
                set = getFilterWindow(i, width, height, TemplateType.T3x3);
                for (int j = 0; j < set.Length; j++)
                {
                    gradX += data[set[j]] * OperatorSet.prewittOperatorX[j];
                    gradY += data[set[j]] * OperatorSet.prewittOperatorY[j];
                }
                tempArray[i] = System.Math.Sqrt(gradX * gradX + gradY * gradY);
            }

            // 判断是否进行二值化
            #region 是否二值化并赋值
            result = new Byte[data.Length];
            if (thresholding == 0)
            {
                // 阈值为0时不进行二值化
                for (int i = 0; i < data.Length; i++)
                {
                    Double temp = tempArray[i];
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i] = Convert.ToByte(temp);
                }
            }
            else
            {
                // 二值化
                for (int i = 0; i < data.Length; i++)
                {
                    if (tempArray[i] > thresholding)
                        result[i] = 255;
                    else
                        result[i] = 0;
                }
            }
            #endregion
        }

        /// <summary>
        ///  Sobel算子边缘锐化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值(阈值为零的话, 不进行二值化)</param>
        protected void sobelEdge(ref Byte[] data, Int32 width, Int32 height, Int32 thresholding, out Byte[] result)
        {
            Double[] tempArray = new Double[data.Length];
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                Double gradX = 0, gradY = 0;
                set = getFilterWindow(i, width, height, TemplateType.T3x3);
                for (int j = 0; j < set.Length; j++)
                {
                    gradX += data[set[j]] * OperatorSet.sobelOperatorX[j];
                    gradY += data[set[j]] * OperatorSet.sobelOperatorY[j];
                }
                tempArray[i] = System.Math.Sqrt(gradX * gradX + gradY * gradY);
            }

            // 判断是否进行二值化
            #region 是否二值化并赋值
            result = new Byte[data.Length];
            if (thresholding == 0)
            {
                // 阈值为0时不进行二值化
                for (int i = 0; i < data.Length; i++)
                {
                    Double temp = tempArray[i];
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i] = Convert.ToByte(temp);
                }
            }
            else
            {
                // 二值化
                for (int i = 0; i < data.Length; i++)
                {
                    if (tempArray[i] > thresholding)
                        result[i] = 255;
                    else
                        result[i] = 0;
                }
            }
            #endregion
        }

        /// <summary>
        ///  Laplacian算子边缘锐化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值(阈值为零的话, 不进行二值化)</param>
        /// <param name="typeNum">算子类型(1型,2型,3型)</param>
        protected void laplacainEdge(ref Byte[] data, Int32 width, Int32 height, Int32 thresholding, Int32 typeNum, out Byte[] result)
        {
            if (typeNum > 3 || typeNum < 1)
                System.Diagnostics.Debug.Fail("只能实现拉普拉斯算子1,2,3");

            Double[] tempArray = new Double[data.Length];
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                Double grad = 0;
                set = getFilterWindow(i, width, height, TemplateType.T3x3);
                for (int j = 0; j < set.Length; j++)
                {
                    if (typeNum == 1)
                        grad += data[set[j]] * OperatorSet.laplacianOperator1[j];
                    else if (typeNum == 2)
                        grad += data[set[j]] * OperatorSet.laplacianOperator2[j];
                    else if (typeNum == 3)
                        grad += data[set[j]] * OperatorSet.laplacianOperator3[j];
                }
                tempArray[i] = grad;
            }

            // 判断是否进行二值化
            #region 是否二值化并赋值
            result = new Byte[data.Length];
            if (thresholding == 0)
            {
                // 阈值为0时不进行二值化
                for (int i = 0; i < data.Length; i++)
                {
                    Double temp = tempArray[i];
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i] = Convert.ToByte(temp);
                }
            }
            else
            {
                // 二值化
                for (int i = 0; i < data.Length; i++)
                {
                    if (tempArray[i] > thresholding)
                        result[i] = 255;
                    else
                        result[i] = 0;
                }
            }
            #endregion
        }

        /// <summary>
        ///  Kirsch算子边缘锐化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="thresholding">阈值(阈值为零的话, 不进行二值化)</param>
        protected void kirschEdge(ref Byte[] data, Int32 width, Int32 height, Int32 thresholding, out Byte[] result)
        {
            Double[] tempArray = new Double[data.Length];
            Int32[] set;
            for (int i = 0; i < data.Length; i++)
            {
                Double[] grad = new Double[6];
                set = getFilterWindow(i, width, height, TemplateType.T3x3);
                for (int j = 0; j < set.Length; j++)
                {
                    grad[0] += data[set[j]] * OperatorSet.kirschOperator1[j];
                    grad[1] += data[set[j]] * OperatorSet.kirschOperator2[j];
                    grad[2] += data[set[j]] * OperatorSet.kirschOperator3[j];
                    grad[3] += data[set[j]] * OperatorSet.kirschOperator4[j];
                    grad[4] += data[set[j]] * OperatorSet.kirschOperator5[j];
                    grad[5] += data[set[j]] * OperatorSet.kirschOperator6[j];
                }
                tempArray[i] = mathHelper.Max(grad);
            }

            // 判断是否进行二值化
            #region 是否二值化并赋值
            result = new Byte[data.Length];
            if (thresholding == 0)
            {
                // 阈值为0时不进行二值化
                for (int i = 0; i < data.Length; i++)
                {
                    Double temp = tempArray[i];
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[i] = Convert.ToByte(temp);
                }
            }
            else
            {
                // 二值化
                for (int i = 0; i < data.Length; i++)
                {
                    if (tempArray[i] > thresholding)
                        result[i] = 255;
                    else
                        result[i] = 0;
                }
            }
            #endregion
        }

        /// <summary>
        ///  计算高斯卷积
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="mask">高斯掩膜</param>
        protected void gaussConv(ref Byte[] data, Int32 width, Int32 height, Double[] mask, out Double[] result)
        {
            Int32 windWidth = Convert.ToInt16(System.Math.Sqrt(mask.Length));
            Int32 radius = windWidth / 2;
            Double temp;
            result = new Double[width * height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    temp = 0;
                    for (int x = -radius; x <= radius; x++)
                    {
                        for (int y = -radius; y <= radius; y++)
                        {
                            temp += data[(System.Math.Abs(i + x) % height) * width + System.Math.Abs(j + y) % width]
                                * mask[(x + radius) * windWidth + y + radius];
                        }
                    }
                    result[i * width + j] = temp;
                }
            }
        }

        #region 高斯滤波模板

        protected Double[] LogTemplate(Double sigma)
        {
            Double std2 = 2 * sigma * sigma;
            Int32 radius = Convert.ToInt16(System.Math.Ceiling(3 * sigma));
            Int32 filterWidth = 2 * radius + 1;
            Double[] filter = new Double[filterWidth * filterWidth];
            Double sum = 0, average = 0;

            // 因为模板是中心对称的, 所以先得到模板左上角的值,再赋到全部模板

            // 计算模板左上角
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    Int32 xx = (j - radius) * (j - radius);
                    Int32 yy = (i - radius) * (i - radius);
                    filter[i * filterWidth + j] = (xx + yy - std2) * System.Math.Exp(-(xx + yy) / std2);
                    sum += 4 * filter[i * filterWidth + j];
                }
            }

            // 水平和垂直对称轴单独处理
            for (int i = 0; i < radius; i++)
            {
                Int32 xx = (i - radius) * (i - radius);
                filter[i * filterWidth + radius] = (xx - std2) * System.Math.Exp(-xx / std2);
                sum += 2 * filter[i * filterWidth + radius];
            }

            for (int j = 0; j < radius; j++)
            {
                Int32 yy = (j - radius) * (j - radius);
                filter[radius * filterWidth + j] = (yy - std2) * System.Math.Exp(-yy / std2);
                sum += 2 * filter[radius * filterWidth + j];
            }
            // 中心点
            filter[radius * filterWidth + radius] = -std2;
            // 所以模板数据和
            sum += filter[radius * filterWidth + radius];
            // 计算平均值
            average = sum / filter.Length;
            // 赋值
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    filter[i * filterWidth + j] = filter[i * filterWidth + j] - average;
                    filter[filterWidth - 1 - j + i * filterWidth] = filter[i * filterWidth + j];
                    filter[j + (filterWidth - 1 - j) * filterWidth] = filter[i * filterWidth + j];
                    filter[filterWidth - 1 - j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                }
            }
            // 赋值水平和垂直对称轴
            for (int i = 0; i < radius; i++)
            {
                filter[i * filterWidth + radius] = filter[i * filterWidth + radius] - average;
                filter[(filterWidth - 1 - i) * filterWidth + radius] = filter[i * filterWidth + radius];
            }

            for (int j = 0; j < radius; j++)
            {
                filter[radius * filterWidth + j] = filter[radius * filterWidth + j] - average;
                filter[radius * filterWidth + filterWidth - 1 - j] = filter[radius * filterWidth + j];
            }
            // 赋值中心点
            filter[radius * filterWidth + radius] = filter[radius * filterWidth + radius] - average;
            return filter;
        }

        protected Double[] DogTemplate(Double sigma)
        {
            Double std2 = 2 * sigma * sigma;
            Int32 radius = Convert.ToInt16(System.Math.Ceiling(3 * sigma));
            Int32 filterWidth = 2 * radius + 1;
            Double[] filter = new Double[filterWidth * filterWidth];
            Double sum = 0, average = 0;

            // 因为模板是中心对称的,所以先得到模板左上角的值, 再赋值到全部模板

            // 计算模板左上角
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    Int32 xx = (j - radius) * (j - radius);
                    Int32 yy = (i - radius) * (i - radius);
                    filter[i * filterWidth + j] = 1.6 * System.Math.Exp(-(xx + yy) * 1.6 * 1.6 / std2) / sigma -
                        System.Math.Exp(-(xx + yy) / -std2) / sigma;
                    sum += 4 * filter[i * filterWidth + j];
                }
            }
            // 水平和垂直对称轴单独处理
            for (int i = 0; i < radius; i++)
            {
                Int32 xx = (i - radius) * (i - radius);
                filter[i * filterWidth + radius] = 1.6 * System.Math.Exp(-xx * 16 * 16 / std2) / sigma -
                    System.Math.Exp(-xx / std2) / sigma;
                sum += 2 * filter[i * filterWidth + radius];
            }

            for (int j = 0; j < radius; j++)
            {
                Int32 yy = (j - radius) * (j - radius);
                filter[radius * filterWidth + j] = 1.6 * System.Math.Exp(-yy * 1.6 * 1.6 / std2) / sigma -
                    System.Math.Exp(-yy / std2) / sigma;
                sum += 2 * filter[radius * filterWidth + j];
            }
            // 中心点
            filter[radius * filterWidth + radius] = 1.6 / sigma - 1 / sigma;
            // 所有模板数据和
            sum += filter[radius * filterWidth + radius];
            // 计算平均值
            average = sum / filter.Length;
            // 赋值
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    filter[i * filterWidth + j] = filter[i * filterWidth + j] - average;
                    filter[filterWidth - 1 - j + i * filterWidth] = filter[i * filterWidth + j];
                    filter[j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                    filter[filterWidth - 1 - j + (filterWidth - 1 - i) * filterWidth] = filter[i * filterWidth + j];
                }
            }
            // 赋值水平和垂直对称轴
            for (int i = 0; i < radius; i++)
            {
                filter[i * filterWidth + radius] = filter[i * filterWidth + radius] - average;
                filter[(filterWidth - 1 - i) * filterWidth + radius] = filter[i * filterWidth + radius];
            }
            for (int j = 0; j < radius; j++)
            {
                filter[radius * filterWidth + j] = filter[radius * filterWidth + j] - average;
                filter[radius * filterWidth + filterWidth - 1 - j] = filter[radius * filterWidth + j];
            }
            // 赋值中心点
            filter[radius * filterWidth + radius] = filter[radius * filterWidth + radius] * average;
            return filter;

        }

        #endregion
    }
}