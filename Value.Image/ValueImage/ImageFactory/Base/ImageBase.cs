using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MathHelper;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        protected ValueMath valueMath = ValueMath.GetInstance();

        #region 内存法处理图像

        private BitmapData bmpData;
        private Bitmap sourceImage;
        private IntPtr ptr;
        // 图像总长度
        protected Int32 Length;
        // 图像长度
        protected Int32 Height;
        // 图像宽度
        protected Int32 Width;
        // 图像实际宽度(已x3)
        protected Int32 RealWidth;
        // 图像实际半宽度(已x3)
        protected Int32 HalfWidth;
        // 图像长度
        protected Int32 HalfHeight;

        protected virtual Byte[] LockBits(Bitmap srcImage, ImageLockMode mode)
        {
            sourceImage = srcImage;

            Int32 tempWidth = sourceImage.Width;
            Int32 tempHeight = sourceImage.Height;
            Rectangle rect = new Rectangle(0, 0, tempWidth, tempHeight);
            bmpData = sourceImage.LockBits(rect, mode, sourceImage.PixelFormat);
            ptr = bmpData.Scan0;

            Int32 byteLength = bmpData.Stride * tempHeight;
            Byte[] rgbBytes = new Byte[byteLength];
            Marshal.Copy(ptr, rgbBytes, 0, byteLength);

            Length = rgbBytes.Length;
            Height = srcImage.Height;
            Width = bmpData.Stride;

            // 奇偶取中值的区别
            if (RealWidth / 3 % 2 == 0)
                HalfWidth = RealWidth / 6 * 3;
            else
                HalfWidth = ((RealWidth / 3 - 1) / 2) * 3;
            // 奇偶取中值的区别
            if (Height % 2 == 0)
                HalfHeight = Height / 2;
            else
                HalfHeight = (Height - 1) / 2;

            return rgbBytes;
        }

        protected void UnlockBits(Byte[] rgbData)
        {
            try
            {
                Marshal.Copy(rgbData, 0, ptr, rgbData.Length);
                sourceImage.UnlockBits(bmpData);
                sourceImage = null;
                bmpData = null;
                ptr = IntPtr.Zero;
            }
            catch
            {

            }
        }

        protected void UnlockBits()
        {
            sourceImage.UnlockBits(bmpData);
            sourceImage = null;
            bmpData = null;
            ptr = IntPtr.Zero;
        }

        #endregion

        #region 零交叉方法

        /// <summary>
        ///  零交叉法阈值处理(二值化)
        /// </summary>
        /// <param name="rectangleData">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        /// <param name="thresh">阈值</param>
        protected void zeroCross(ref Double[] rectangleData, Int32 width, Int32 height, Double thresh, out Byte[] result)
        {
            result = new Byte[width * height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (rectangleData[i * width + j] < 0 && rectangleData[((i + 1) % height) * width + j] > 0 && System.Math.Abs(rectangleData[i * width + j] - rectangleData[((i + 1) % height) * width + j]) > thresh)
                    {
                        result[i * width + j] = 255;
                    }
                    else if (rectangleData[i * width + j] < 0 && rectangleData[((System.Math.Abs(i - 1)) % height) * width + j] > 0 && System.Math.Abs(rectangleData[i * width + j] - rectangleData[((System.Math.Abs(i - 1)) % height) * width + j]) > thresh)
                    {
                        result[i * width + j] = 255;
                    }
                    else if (rectangleData[i * width + j] < 0 && rectangleData[i * width + ((j + 1) % width)] > 0 && System.Math.Abs(rectangleData[i * width + j] - rectangleData[i * width + ((j + 1) % width)]) > thresh)
                    {
                        result[i * width + j] = 255;
                    }
                    else if (rectangleData[i * width + j] < 0 && rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)] > 0 && System.Math.Abs(rectangleData[i * width + j] - rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)]) > thresh)
                    {
                        result[i * width + j] = 255;
                    }
                    else if (rectangleData[i * width + j] == 0)
                    {
                        if (rectangleData[((i + 1) % height) * width + j] > 0 && rectangleData[((System.Math.Abs(i - 1)) % height) * width + j] < 0 && System.Math.Abs(rectangleData[((System.Math.Abs(i - 1)) % height) * width + j] - rectangleData[((i + 1) % height) * width + j]) > 2 * thresh)
                        {
                            result[i * width + j] = 255;
                        }
                        else if (rectangleData[((i + 1) % height) * width + j] < 0 && rectangleData[((System.Math.Abs(i - 1)) % height) * width + j] > 0 && System.Math.Abs(rectangleData[((System.Math.Abs(i - 1)) % height) * width + j] - rectangleData[((i + 1) % height) * width + j]) > 2 * thresh)
                        {
                            result[i * width + j] = 255;
                        }
                        else if (rectangleData[i * width + ((j + 1) % width)] > 0 && rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)] < 0 && System.Math.Abs(rectangleData[i * width + ((j + 1) % width)] - rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)]) > 2 * thresh)
                        {
                            result[i * width + j] = 255;
                        }
                        else if (rectangleData[i * width + ((j + 1) % width)] < 0 && rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)] > 0 && System.Math.Abs(rectangleData[i * width + ((j + 1) % width)] - rectangleData[i * width + ((System.Math.Abs(j - 1)) % width)]) > 2 * thresh)
                        {
                            result[i * width + j] = 255;
                        }
                        else
                        {
                            result[i * width + j] = 0;
                        }
                    }
                    else
                    {
                        result[i * width + j] = 0;
                    }
                }
            }
        }

        #endregion

        #region 获得滤波窗口

        /// <summary>
        ///  获得滤波窗口
        /// </summary>
        /// <param name="index">当前像素索引</param>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        /// <param name="type">窗口类型</param>
        protected Int32[] getFilterWindow(Int32 index, Int32 width, Int32 height, FilterWindowType type)
        {
            Int32 row = index / width;
            Int32 col = index % width;
            Int32[] set = null;

            switch (type)
            {
                case FilterWindowType.Hori3:
                    set = new Int32[3];
                    set[0] = row * width + System.Math.Abs(col - 1) % width;
                    set[1] = index;
                    set[2] = row * width + (col + 1) % width;
                    break;
                case FilterWindowType.Vert3:
                    set = new Int32[3];
                    set[0] = System.Math.Abs(row - 1) % height * width + col;
                    set[1] = index;
                    set[2] = (row + 1) % height * width + col;
                    break;
                case FilterWindowType.Cros3:
                    set = new Int32[5];
                    set[0] = System.Math.Abs(row - 1) % height * width + col;
                    set[1] = row * width + System.Math.Abs(col - 1) % width;
                    set[2] = index;
                    set[3] = row * width + (col + 1) % width;
                    set[4] = (row + 1) % height * width + col;
                    break;
                case FilterWindowType.Rect3:
                    set = new Int32[9];
                    set[0] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[1] = System.Math.Abs(row - 1) % height * width + col;
                    set[2] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;
                    set[3] = row * width + System.Math.Abs(col - 1) % width;
                    set[4] = index;
                    set[5] = row * width + (col + 1) % width;
                    set[6] = (row + 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[7] = (row + 1) % height * width + col;
                    set[8] = (row + 1) % height * width + (col + 1) % width;
                    break;
                case FilterWindowType.Hori5:
                    set = new Int32[5];
                    set[0] = row * width + System.Math.Abs(col - 2) % width;
                    set[1] = row * width + System.Math.Abs(col - 1) % width;
                    set[2] = index;
                    set[3] = row * width + (col + 1) % width;
                    set[4] = row * width + (col + 2) % width;
                    break;
                case FilterWindowType.Vert5:
                    set = new Int32[5];
                    set[0] = System.Math.Abs(row - 2) % height * width + col;
                    set[1] = System.Math.Abs(row - 1) % height * width + col;
                    set[2] = index;
                    set[3] = (row + 1) % height * width + col;
                    set[4] = (row + 2) % height * width + col;
                    break;
                case FilterWindowType.Cros5:
                    set = new Int32[9];
                    set[0] = System.Math.Abs(row - 2) % height * width + col;
                    set[1] = System.Math.Abs(row - 1) % height * width + col;
                    set[2] = row * width + System.Math.Abs(col - 2) % width;
                    set[3] = row * width + System.Math.Abs(col - 1) % width;
                    set[4] = index;
                    set[5] = row * width + (col + 1) % width;
                    set[6] = row * width + (col + 2) % width;
                    set[7] = (row + 1) % height * width + col;
                    set[8] = (row + 2) % height * width + col;
                    break;
                case FilterWindowType.Rect5:
                    set = new Int32[25];
                    set[0] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[1] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[2] = System.Math.Abs(row - 2) % height * width + col;
                    set[3] = System.Math.Abs(row - 2) % height * width + (col + 1) % width;
                    set[4] = System.Math.Abs(row - 2) % height * width + (col + 2) % width;
                    set[5] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[6] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[7] = System.Math.Abs(row - 1) % height * width + col;
                    set[8] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;
                    set[9] = System.Math.Abs(row - 1) % height * width + (col + 2) % width;
                    set[10] = row * width + System.Math.Abs(col - 2) % width;
                    set[11] = row * width + System.Math.Abs(col - 1) % width;
                    set[12] = index;
                    set[13] = row * width + System.Math.Abs(col + 1) % width;
                    set[14] = row * width + System.Math.Abs(col + 2) % width;
                    set[15] = (row + 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[16] = (row + 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[17] = (row + 1) % height * width + col;
                    set[18] = (row + 1) % height * width + System.Math.Abs(col + 1) % width;
                    set[19] = (row + 1) % height * width + System.Math.Abs(col + 2) % width;
                    set[20] = (row + 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[21] = (row + 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[22] = (row + 2) % height * width + col;
                    set[23] = (row + 2) % height * width + System.Math.Abs(col + 1) % width;
                    set[24] = (row + 2) % height * width + System.Math.Abs(col + 2) % width;
                    break;
            }
            return set;
        }

        /// <summary>
        ///  获得滤波窗口
        /// </summary>
        /// <param name="index">当前像素索引</param>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        /// <param name="type">窗口类型</param>
        protected Int32[] getFilterWindow(Int32 index, Int32 width, Int32 height, TemplateType type)
        {
            switch (type)
            {
                case TemplateType.T3x3:
                    return this.getFilterWindow(index, width, height, FilterWindowType.Rect3);
                case TemplateType.T5x5:
                    return this.getFilterWindow(index, width, height, FilterWindowType.Rect5);
                case TemplateType.T7x7:
                    Int32 row = index / width;
                    Int32 col = index % width;
                    Int32[] set = null;
                    set = new Int32[49];
                    set[0] = System.Math.Abs(row - 3) % height * width + System.Math.Abs(col - 3) % width;
                    set[1] = System.Math.Abs(row - 3) % height * width + System.Math.Abs(col - 2) % width;
                    set[2] = System.Math.Abs(row - 3) % height * width + System.Math.Abs(col - 1) % width;
                    set[3] = System.Math.Abs(row - 3) % height * width + col;
                    set[4] = System.Math.Abs(row - 3) % height * width + (col + 1) % width;
                    set[5] = System.Math.Abs(row - 3) % height * width + (col + 2) % width;
                    set[6] = System.Math.Abs(row - 3) % height * width + (col + 3) % width;

                    set[7] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 3) % width;
                    set[8] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[9] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[10] = System.Math.Abs(row - 2) % height * width + col;
                    set[11] = System.Math.Abs(row - 2) % height * width + (col + 1) % width;
                    set[12] = System.Math.Abs(row - 2) % height * width + (col + 2) % width;
                    set[13] = System.Math.Abs(row - 2) % height * width + (col + 3) % width;

                    set[14] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 3) % width;
                    set[15] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[16] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[17] = System.Math.Abs(row - 1) % height * width + col;
                    set[18] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;
                    set[19] = System.Math.Abs(row - 1) % height * width + (col + 2) % width;
                    set[20] = System.Math.Abs(row - 1) % height * width + (col + 3) % width;

                    set[21] = row * width + System.Math.Abs(col - 3) % width;
                    set[22] = row * width + System.Math.Abs(col - 2) % width;
                    set[23] = row * width + System.Math.Abs(col - 1) % width;
                    set[24] = index;
                    set[25] = row * width + System.Math.Abs(col + 1) % width;
                    set[26] = row * width + System.Math.Abs(col + 2) % width;
                    set[27] = row * width + System.Math.Abs(col + 3) % width;

                    set[28] = (row + 1) % height * width + System.Math.Abs(col - 3) % width;
                    set[29] = (row + 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[30] = (row + 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[31] = (row + 1) % height * width + col;
                    set[32] = (row + 1) % height * width + System.Math.Abs(col + 1) % width;
                    set[33] = (row + 1) % height * width + System.Math.Abs(col + 2) % width;
                    set[34] = (row + 1) % height * width + System.Math.Abs(col + 3) % width;

                    set[35] = (row + 2) % height * width + System.Math.Abs(col - 3) % width;
                    set[36] = (row + 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[37] = (row + 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[38] = (row + 2) % height * width + col;
                    set[39] = (row + 2) % height * width + System.Math.Abs(col + 1) % width;
                    set[40] = (row + 2) % height * width + System.Math.Abs(col + 2) % width;
                    set[41] = (row + 2) % height * width + System.Math.Abs(col + 3) % width;

                    set[42] = (row + 3) % height * width + System.Math.Abs(col - 3) % width;
                    set[43] = (row + 3) % height * width + System.Math.Abs(col - 2) % width;
                    set[44] = (row + 3) % height * width + System.Math.Abs(col - 1) % width;
                    set[45] = (row + 3) % height * width + col;
                    set[46] = (row + 3) % height * width + System.Math.Abs(col + 1) % width;
                    set[47] = (row + 3) % height * width + System.Math.Abs(col + 2) % width;
                    set[48] = (row + 3) % height * width + System.Math.Abs(col + 3) % width;
                    return set;
            }
            return null;
        }

        /// <summary>
        ///  获得2x2索引 当前元素为左下角
        /// </summary>
        /// <param name="index">当前元素索引</param>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        protected Int32[] getFilterWindow2x2(Int32 index, Int32 width, Int32 height)
        {
            Int32 row = index / width;
            Int32 col = index % width;
            Int32[] set = new Int32[4];
            set[0] = System.Math.Abs(row - 1) % height * width + col;
            set[1] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;
            set[2] = index;
            set[3] = row * width + System.Math.Abs(col + 1) % width;
            return set;
        }

        /// <summary>
        ///  获得3x3索引 当前元素为正中心
        /// </summary>
        /// <param name="index">当前元素索引</param>
        /// <param name="width">二维数据宽度</param>
        /// <param name="height">二维数据高度</param>
        /// <param name="type">顺时针或者逆时针</param>
        protected Int32[] getFilterWindow3x3(Int32 index, Int32 width, Int32 height, DirectType type)
        {
            Int32 row = index / width;
            Int32 col = index % width;
            Int32[] set = new Int32[9];
            Int32 rm1 = (row - 1) >= 0 ? (row - 1) : row;
            Int32 rp1 = (row + 1) < height ? (row + 1) : row;
            Int32 cm1 = (col - 1) >= 0 ? (col - 1) : col;
            Int32 cp1 = (col + 1) < width ? (col + 1) : col;

            switch (type)
            {
                case DirectType.Clock:
                    set[0] = index;
                    set[1] = rm1 * width + col;
                    set[2] = rm1 * width + cp1;
                    set[3] = row * width + cp1;
                    set[4] = rp1 * width + cp1;
                    set[5] = rp1 * width + col;
                    set[6] = rp1 * width + cm1;
                    set[7] = row * width + cm1;
                    set[8] = rm1 * width + cm1;
                    break;
                case DirectType.UntiClock:
                    set[0] = index;
                    set[1] = rm1 * width + col;
                    set[2] = rm1 * width + cm1;
                    set[3] = row * width + cm1;
                    set[4] = rp1 * width + cm1;
                    set[5] = rp1 * width + col;
                    set[6] = rp1 * width + cp1;
                    set[7] = row * width + cp1;
                    set[8] = rm1 * width + cp1;
                    break;
            }
            return set;
        }

        /// <summary>
        ///  获得3x3索引 当前元素为正中心 只获取存在的邻域索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected Int32[] getExistFilterWindow3x3(Int32 index, Int32 width, Int32 height, DirectType type)
        {
            System.Collections.Generic.List<Int32> list = new System.Collections.Generic.List<int>();
            Int32 row = index / width;
            Int32 col = index % width;
            list.Add(index);
            switch (type)
            {
                case DirectType.Clock:
                    if (row > 0)
                        list.Add((row - 1) * width + col);
                    if (row > 0 && col < width - 1)
                        list.Add((row - 1) * width + col + 1);
                    if (col < width - 1)
                        list.Add(row * width + col + 1);
                    if (row < height - 1 && col < width - 1)
                        list.Add((row + 1) * width + col + 1);
                    if (row < height - 1)
                        list.Add((row + 1) * width + col);
                    if (row < height - 1 && col > 0)
                        list.Add((row + 1) * width + col - 1);
                    if (col > 0)
                        list.Add(row * width + col - 1);
                    if (row > 0 && col > 0)
                        list.Add((row - 1) * width + col - 1);
                    break;
                case DirectType.UntiClock:
                    if (row > 0)
                        list.Add((row - 1) * width + col);
                    if (row > 0 && col > 0)
                        list.Add((row - 1) * width + col - 1);
                    if (col > 0)
                        list.Add(row * width + col - 1);
                    if (row < height - 1 && col > 0)
                        list.Add((row + 1) * width + col - 1);
                    if (row < height - 1)
                        list.Add((row + 1) * width + col);
                    if (row < height - 1 && col < width - 1)
                        list.Add((row + 1) * width + col + 1);
                    if (col < width - 1)
                        list.Add(row * width + col + 1);
                    if (row > 0 && col < width)
                        list.Add((row - 1) * width + col + 1);
                    break;
            }
            return list.ToArray();
        }

        /// <summary>
        ///  获取3x3窗体
        ///  统一正上方为突入外围点(即当前点索引为0, 正上方点索引为1)
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="x">当前点横坐标</param>
        /// <param name="y">当前点纵坐标</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="overstepColor">超出边界的点的填充值</param>
        /// <param name="type">窗口索引的方向</param>
        protected Byte[] getFilterWindow3x3(ref Byte[] data, Int32 x, Int32 y, Int32 width, Int32 height, Byte overstepColor, DirectType type)
        {
            Byte[] set = new Byte[9];
            Int32 index = y * width + x;
            switch (type)
            {
                case DirectType.Clock:
                    set[0] = data[index];
                    set[1] = (y > 0) ? data[(y - 1) * width + x] : overstepColor;
                    set[2] = (y > 0) ? ((x < (width - 1)) ? data[(y - 1) * width + x + 1] : overstepColor) : overstepColor;
                    set[3] = (x < (width - 1)) ? data[y * width + x + 1] : overstepColor;
                    set[4] = (y < (height - 1) ? (x < (width - 1) ? data[(y + 1) * width + x + 1] : overstepColor) : overstepColor);
                    set[5] = (y < (height - 1)) ? data[(y + 1) * width + x] : overstepColor;
                    set[6] = (y < (height - 1)) ? ((x > 0) ? data[(y + 1) * width + x - 1] : overstepColor) : overstepColor;
                    set[7] = (x > 0) ? data[y * width + x - 1] : overstepColor;
                    set[8] = (y > 0) ? ((x > 0) ? data[(y - 1) * width + x - 1] : overstepColor) : overstepColor;
                    break;
                case DirectType.UntiClock:
                    set[0] = data[index];
                    set[1] = (y > 0) ? data[(y - 1) * width + x] : overstepColor;
                    set[2] = (y > 0) ? ((x > 0) ? data[(y - 1) * width + x - 1] : overstepColor) : overstepColor;
                    set[3] = (x > 0) ? data[y * width + x - 1] : overstepColor;
                    set[4] = (y < (height - 1)) ? ((x > 0) ? data[(y + 1) * width + x - 1] : overstepColor) : overstepColor;
                    set[5] = (y < (height - 1)) ? data[(y + 1) * width + x] : overstepColor;
                    set[6] = (y < (height - 1) ? (x < (width - 1) ? data[(y + 1) * width + x + 1] : overstepColor) : overstepColor);
                    set[7] = (x < (width - 1)) ? data[y * width + x + 1] : overstepColor;
                    set[8] = (y > 0) ? ((x < (width - 1)) ? data[(y - 1) * width + x + 1] : overstepColor) : overstepColor;
                    break;
            }

            return set;
        }

        protected Int32[] getFilterWindow5x5(Int32 index, Int32 width, Int32 height, DirectType type)
        {
            Int32 row = index / width;
            Int32 col = index % width;
            Int32[] set = new Int32[25];
            switch (type)
            {
                case DirectType.Clock:
                    set[0] = index;
                    #region 内圈

                    set[1] = System.Math.Abs(row - 1) % height * width + col;
                    set[2] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;
                    set[3] = row * width + (col + 1) % width;
                    set[4] = (row + 1) % height * width + (col + 1) % width;
                    set[5] = (row + 1) % height * width + col;
                    set[6] = (row + 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[7] = row * width + System.Math.Abs(col - 1) % width;
                    set[8] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 1) % width;

                    #endregion
                    #region 外圈

                    set[9] = System.Math.Abs(row - 2) % height * width + col;
                    set[10] = System.Math.Abs(row - 2) % height * width + (col + 1) % width;
                    set[11] = System.Math.Abs(row - 2) % height * width + (col + 2) % width;
                    set[12] = System.Math.Abs(row - 1) % height * width + (col + 2) % width;
                    set[13] = row * width + (col + 2) % width;
                    set[14] = (row + 1) % height * width + (col + 2) % width;
                    set[15] = (row + 2) % height * width + (col + 2) % width;
                    set[16] = (row + 2) % height * width + (col + 1) % width;
                    set[17] = (row + 2) % height * width + col;
                    set[18] = (row + 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[19] = (row + 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[20] = (row + 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[21] = row * width + System.Math.Abs(col - 2) % width;
                    set[22] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[23] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[24] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 1) % width;

                    #endregion
                    break;
                case DirectType.UntiClock:
                    set[0] = index;
                    #region 圈内

                    set[1] = System.Math.Abs(row - 1) % height * width + col;
                    set[2] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[3] = row * width + System.Math.Abs(col - 1) % width;
                    set[4] = (row + 1) % height * width + System.Math.Abs(col - 1) % width;
                    set[5] = (row + 1) % height * width + col;
                    set[6] = (row + 1) % height * width + (col + 1) % width;
                    set[7] = row * width + (col + 1) % width;
                    set[8] = System.Math.Abs(row - 1) % height * width + (col + 1) % width;

                    #endregion
                    #region 圈外

                    set[9] = System.Math.Abs(row - 2) % height * width + col;
                    set[10] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[11] = System.Math.Abs(row - 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[12] = System.Math.Abs(row - 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[13] = row * width + System.Math.Abs(col - 2) % width;
                    set[14] = (row + 1) % height * width + System.Math.Abs(col - 2) % width;
                    set[15] = (row + 2) % height * width + System.Math.Abs(col - 2) % width;
                    set[16] = (row + 2) % height * width + System.Math.Abs(col - 1) % width;
                    set[17] = (row + 2) % height * width + col;
                    set[18] = (row + 2) % height * width + (col + 1) % width;
                    set[19] = (row + 2) % height * width + (col + 2) % width;
                    set[20] = (row + 1) % height * width + (col + 2) % width;
                    set[21] = row * width + (col + 2) % width;
                    set[22] = System.Math.Abs(row - 1) % height * width + (col + 2) % width;
                    set[23] = System.Math.Abs(row - 2) % height * width + (col + 2) % width;
                    set[24] = System.Math.Abs(row - 2) % height * width + (col + 1) % width;

                    #endregion
                    break;
            }
            return set;
        }

        #endregion
    }
}
