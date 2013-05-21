using System;
using ValueImage.Interface;
using System.Drawing.Imaging;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IOther
    {
        public void InvertColor(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = Convert.ToByte(255 - rgbBytes[i * Width + j * 3]);
                    rgbBytes[i * Width + j * 3 + 1] = Convert.ToByte(255 - rgbBytes[i * Width + j * 3 + 1]);
                    rgbBytes[i * Width + j * 3 + 2] = Convert.ToByte(255 - rgbBytes[i * Width + j * 3 + 2]);
                }
            }
            UnlockBits(rgbBytes);
        }

        #region 直方图

        public void HistEqualization(System.Drawing.Bitmap srcImage)
        {
            //Int32[] frequency = getFrequency(srcImage);

            //Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);

            //Byte temp;
            //var tempArray = new Int32[256];
            //// 映射的像素集
            //var pixelMap = new Byte[256];

            //// 生成累计归一化直方图
            //// 并生成映射表
            //for (int i = 0; i < 256; i++)
            //{
            //    if (i % RealWidth >= RealWidth)
            //    {
            //        i = (i / Width + 1) * Width - 3;
            //        continue;
            //    }

            //    if (i != 0)
            //    {
            //        tempArray[i] = tempArray[i - 1] + frequency[i];
            //    }
            //    else
            //        tempArray[0] = frequency[0];
            //    pixelMap[i] = (Byte)(255.0 * tempArray[i] / Length + 0.5);
            //}

            //for (int i = 0; i < Length; i++)
            //{
            //    temp = rgbBytes[i];
            //    rgbBytes[i] = pixelMap[temp];
            //}
            //UnlockBits(rgbBytes);
        }

        public void HistMatch(System.Drawing.Bitmap srcImage, Int32[] histogram)
        {
            //// 获得源图像直方图
            //Int32[] frequency = this.getFrequency(srcImage);
            //Int32 maxPixel = mathHelper.MaxIndex(frequency);
            //Int32 length = frequency.Length;

            //// 内存法操作图像
            //Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);

            //// 计算该直方图各灰度的累计分布函数
            //Double[] Hc = new Double[length];
            //Hc[0] = frequency[0];
            //for (int i = 1; i < length; i++)
            //{
            //    Hc[i] = (Hc[i - 1] + frequency[i]) / (Double)Length;
            //}

            //// 直方图匹配算法
            //Double diffA = 0D, diffB = 0D;
            //Int32 k = 0;
            //Byte[] mapPixel = new Byte[length];
            //for (int i = 0; i < length; i++)
            //{
            //    diffB = 1;
            //    for (int j = 0; j < length; j++)
            //    {
            //        diffA = System.Math.Abs(Hc[i] - histogram[j]);

            //        //                 1.0乘以10的-8次方
            //        // 找到2个累计分布函数最相似的位置
            //        if (diffA - diffB < 1.0E-08)
            //        {
            //            // 记下差值
            //            diffB = diffA;
            //            k = j;
            //        }
            //        else
            //        {
            //            // 已找到为相似位置,记录并退出
            //            k = j - 1;
            //            break;
            //        }
            //    }

            //    // 如果达到最大灰度级,标志未处理灰度数,并推出循环
            //    if (k == 255)
            //    {
            //        for (int l = 0; l < length; l++)
            //        {
            //            mapPixel[l] = (Byte)k;
            //        }
            //        break;
            //    }
            //    mapPixel[i] = (Byte)k;
            //}

            //for (int i = 0; i < rgbBytes.Length; i++)
            //{
            //    rgbBytes[i] = mapPixel[rgbBytes[i]];
            //}

            //UnlockBits(rgbBytes);
        }

        #endregion
    }
}
