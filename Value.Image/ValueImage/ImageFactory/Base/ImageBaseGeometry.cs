using System;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  填充矩形
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="startRow">起始</param>
        /// <param name="startRow">起始行索引</param>
        /// <param name="startCol">起始列索引</param>
        /// <param name="endRow">终止行索引</param>
        /// <param name="endCol">终止列索引</param>
        /// <param name="color">颜色</param>
        protected void fillRectangle(ref Byte[] data, Int32 width, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol, Byte color)
        {
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startCol; j < endCol; j++)
                {
                    data[i * width + j] = color;
                }
            }
        }

        /// <summary>
        ///  对图像已指定像素按 kx+b 线性变换
        /// </summary>
        /// <param name="data">要处理的一维数据</param>
        /// <param name="slope">斜率</param>
        /// <param name="displacements">平移</param>
        protected void linearChange(ref Byte[] data, float slope, float displacements)
        {
            Double temp;
            for (int i = 0; i < data.Length; i++)
            {
                temp = data[i] * slope + displacements;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  最近邻插值法
        /// </summary>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        /// <param name="zoomingX">水平缩放倍数</param>
        /// <param name="zoomingY">垂直缩放倍数</param>
        protected void nearestNeighborZoom(ref Byte[] data, Int32 width, Int32 height, Double zoomingX, Double zoomingY, out Byte[] result)
        {
            Int32 xz = 0, yz = 0;
            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            Int32 tempWidth = 0, tempHeight = 0;
            result = new Byte[data.Length];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempHeight = i - medianHeight;
                    tempWidth = j - medianWidth;

                    if (tempWidth > 0)
                        xz = (Int32)(tempWidth / zoomingX + 0.5);
                    else
                        xz = (Int32)(tempWidth / zoomingX - 0.5);

                    if (tempHeight > 0)
                        yz = (Int32)(tempHeight / zoomingY + 0.5);
                    else
                        yz = (Int32)(tempHeight / zoomingY - 0.5);

                    tempWidth = xz + medianWidth;
                    tempHeight = yz + medianHeight;
                    if (tempWidth < 0 || tempWidth >= width || tempHeight < 0 || tempHeight >= height)
                        result[i * width + j] = 255;
                    else
                        result[i * width + j] = data[tempHeight * width + tempWidth];
                }
            }
        }

        protected void amphilinearityZoom(ref Byte[] data, Int32 width, Int32 height, Double zoomingX, Double zoomingY, out Byte[] result)
        {
            Int32 xz = 0, yz = 0;
            Double tempX, tempY, p, q;
            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            Int32 tempWidth = 0, tempHeight = 0;
            result = new Byte[data.Length];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempHeight = i - medianHeight;
                    tempWidth = j - medianWidth;
                    tempX = tempWidth / zoomingX;
                    tempY = tempHeight / zoomingY;

                    if (tempWidth > 0)
                        xz = (Int32)tempX;
                    else
                        xz = (Int32)(tempX - 1);
                    if (tempHeight > 0)
                        yz = (Int32)tempY;
                    else
                        yz = (Int32)(tempY - 1);

                    p = tempX - xz;
                    q = tempY - yz;

                    tempWidth = xz + medianWidth;
                    tempHeight = yz + medianHeight;
                    if (tempWidth < 0 || (tempWidth + 1) >= width || tempHeight < 0 || (tempHeight + 1) >= height)
                        result[i * width + j] = 255;
                    else
                    {
                        result[i * width + j] = (Byte)((1.0F - p) * (1.0F - q) * data[tempHeight * width + tempWidth] +
                               (1.0F - p) * q * data[(tempHeight + 1) * width + tempWidth] +
                               p * (1.0F - q) * data[tempHeight * width + tempWidth + 1] +
                               p * q * data[(tempHeight + 1) * width + tempWidth + 1]);
                    }
                }
            }
        }
    }
}
