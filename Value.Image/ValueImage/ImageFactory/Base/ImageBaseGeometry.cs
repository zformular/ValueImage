using System;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  填充矩形
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
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

        protected void amphilinearityRotate(ref Byte[] data, Int32 width, Int32 height, Int32 degree, out Byte[] result)
        {
            Int32 xr = 0, yr = 0;
            Double tempX, tempY, p, q;
            Int32 medianWidth = width / 2;
            Int32 medianHeight = height / 2;
            Int32 tempWidth = 0, tempHeight = 0;
            result = new Byte[data.Length];
            Double angle = degree * System.Math.PI / 180;
            Double cos = System.Math.Cos(angle);
            Double sin = System.Math.Sin(angle);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempHeight = i - medianHeight;
                    tempWidth = j - medianWidth;
                    tempX = tempWidth * cos - tempHeight * sin;
                    tempY = tempHeight * cos + tempWidth * sin;
                    p = tempX - xr;
                    q = tempY - yr;
                    tempWidth = xr + medianWidth;
                    tempHeight = yr + medianHeight;
                    if (tempWidth < 0 || (tempWidth + 1) >= width || tempHeight < 0 || (tempHeight + 1) >= height)
                    {
                        result[i * width + j] = 255;
                    }
                    else
                    {
                        result[i * width + j] = (byte)((1.0 - q) * ((1.0 - p) * data[tempHeight * width + tempWidth] + p * data[tempHeight * width + tempWidth + 1]) +
                            q * ((1.0 - p) * data[(tempHeight + 1) * width + tempWidth] + p * data[(tempHeight + 1) * width + 1 + tempWidth]));
                    }
                }
            }
        }

        /// <summary>
        ///  双线性插值旋转
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="degree">逆时针旋转弧度</param>
        /// <param name="newWidth">新二维数据宽度</param>
        /// <param name="newHeight">新二维数据高度</param>
        /// <param name="result">新二维数据</param>
        protected void bilinearRotate(ref Byte[] data, Int32 width, Int32 height, double degree, Int32 newWidth, Int32 newHeight, Byte backColor, out Byte[] result)
        {
            Double sin = System.Math.Round(System.Math.Sin(degree), 5);
            Double cos = System.Math.Round(System.Math.Cos(degree), 5);

            Double medianX = (Double)(width - 1) / 2;
            Double medianY = (Double)(height - 1) / 2;
            Int32 xx, yy, x1, y1;
            Double x, y, dx, dy;

            // 由于这个得出的新坐标是在就图坐标系上的值 所以除去增量
            Int32 dirfX = (newWidth - width) / 2;
            Int32 dirfY = (newHeight - height) / 2;

            result = new Byte[newWidth * newHeight];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = backColor;
            }

            for (int j = 0; j < newHeight; j++)
            {
                for (int i = 0; i < newWidth; i++)
                {
                    x = ((i - dirfX) - medianX) * cos - ((j - dirfY) - medianY) * sin + medianX;
                    y = ((i - dirfX) - medianX) * sin + ((j - dirfY) - medianY) * cos + medianY;
                    if (x < 0 || x >= width || y < 0 || y >= height) continue;

                    xx = (Int32)System.Math.Floor(x);
                    yy = (Int32)System.Math.Floor(y);

                    dx = x - xx;
                    dy = y - yy;
                    x1 = (xx + 1) >= width ? (xx) : (xx + 1);
                    y1 = (yy + 1) >= height ? (yy) : (yy + 1);

                    Int32 value = (Int32)((1 - dx) * (1 - dy) * data[yy * width + xx] + (1 - dx) * dy * data[y1 * width + xx] +
                        dx * (1 - dy) * data[yy * width + x1] + dx * dy * data[y1 * width + x1]);
                    result[j * newWidth + i] = (Byte)value;
                }
            }
        }

        protected void bilinearRotate(ref Byte[] data, Int32 width, Int32 height, double degree, Int32 newWidth, Int32 newHeight, out Byte[] result)
        {
            bilinearRotate(ref data, width, height, degree, newWidth, newHeight, 255, out result);
        }

        /// <summary>
        ///  最近邻插值法
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        /// <param name="zoomingX">水平缩放倍数</param>
        /// <param name="zoomingY">垂直缩放倍数</param>
        /// <param name="result">处理后结果</param>
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

        /// <summary>
        ///  双线性插值法
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="newWidth">新二维数据宽度</param>
        /// <param name="newHeight">新二维数据高度</param>
        /// <param name="result">缩放后二维数据</param>
        protected void bileanerZoom(ref Byte[] data, Int32 width, Int32 height, Int32 newWidth, Int32 newHeight, out Byte[] result)
        {
            result = new Byte[newWidth * newHeight];
            float zoomWidth = (float)newWidth / width;
            float zoomHieght = (float)newHeight / height;
            float x, y, fx, fy;
            Int32 xx, yy, x1, y1;

            for (int i = 0; i < newWidth; i++)
            {
                for (int j = 0; j < newHeight; j++)
                {
                    x = i / zoomWidth;
                    y = j / zoomHieght;
                    xx = (Int32)System.Math.Floor(x);
                    yy = (Int32)System.Math.Floor(y);

                    fx = x - xx;
                    fy = y - yy;
                    x1 = (xx + 1) >= width ? (xx) : (xx + 1);
                    y1 = (yy + 1) >= height ? (yy) : (yy + 1);

                    Int32 value = (Int32)((1 - fx) * (1 - fy) * data[yy * width + xx] + (1 - fx) * fy * data[y1 * width + xx] +
                        fx * (1 - fy) * data[yy * width + x1] + fx * fy * data[y1 * width + x1]);

                    result[j * newWidth + i] = (Byte)value;
                }
            }
        }

        /// <summary>
        ///  系统内置算法缩放图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="orgImage">缩放的图片</param>
        /// <param name="format">缩放后格式</param>
        protected System.Drawing.Bitmap zoom(Int32 width, Int32 height, System.Drawing.Bitmap orgImage, System.Drawing.Imaging.PixelFormat format)
        {
            if (width == orgImage.Width && height == orgImage.Height) return orgImage;

            System.Drawing.Bitmap srcImage = new System.Drawing.Bitmap(width, height, format);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(srcImage);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawImage(orgImage, 0, 0, width, height);
            orgImage.Dispose();
            return srcImage;
        }

        /// <summary>
        ///  拼接图片
        /// </summary>
        /// <param name="width">拼接后图片内存法宽度</param>
        /// <param name="info">拼接图片的信息</param>
        protected void spliceImage(Int32 width, ref Byte[] datab, ref Byte[] datag, ref Byte[] datar, ref ImageInfo info)
        {
            Byte[] rgbBytes = LockBits(info.OrgImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    datab[(info.Location.Y + i) * width + info.Location.X + j] = rgbBytes[i * Width + j * 3];
                    datag[(info.Location.Y + i) * width + info.Location.X + j] = rgbBytes[i * Width + j * 3 + 1];
                    datar[(info.Location.Y + i) * width + info.Location.X + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            UnlockBits(rgbBytes);
        }
    }
}
