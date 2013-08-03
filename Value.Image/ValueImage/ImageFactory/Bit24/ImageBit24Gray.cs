using System;
using System.Drawing;
using System.Diagnostics;
using ValueImage.Interface;
using System.Drawing.Imaging;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IGray
    {

        #region 灰度图

        public void ConvertToGrayscale(Bitmap srcImage, GrayscaleType type)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }

            Double temp = 0D;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    switch (type)
                    {
                        case GrayscaleType.Maximum:
                            temp =  MathHelper.ValueMath.Max(tempb[i * singleWidth + j], tempg[i * singleWidth + j], tempr[i * singleWidth + j]);
                            break;
                        case GrayscaleType.Minimal:
                            temp =  MathHelper.ValueMath.Min(tempb[i * singleWidth + j], tempg[i * singleWidth + j], tempg[i * singleWidth + j]);
                            break;
                        case GrayscaleType.Average:
                            temp = MathHelper.ValueMath.Average(tempb[i * singleWidth + j], tempg[i * singleWidth + j], tempg[i * singleWidth + j]);
                            break;
                    }

                    rgbBytes[i * Width + j * 3] = (Byte)temp;
                    rgbBytes[i * Width + j * 3 + 1] = (Byte)temp;
                    rgbBytes[i * Width + j * 3 + 2] = (Byte)temp;
                }
            }
            UnlockBits(rgbBytes);

        }

        public void ConvertToGrayscale(Bitmap srcImage, float weightR, float weightG, float weightB)
        {
            Int32 result = (Int32)(weightB + weightG + weightR);
            Debug.Assert(result == 1.0, "三个权重必须等于1");
            if (result != 1) return;

            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }

            float temp = 0F;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    temp = weightR * tempb[i * singleWidth + j] + weightG * tempg[i * singleWidth + j] + weightB * tempr[i * singleWidth + j];

                    rgbBytes[i * Width + j * 3] = (Byte)temp;
                    rgbBytes[i * Width + j * 3 + 1] = (Byte)temp;
                    rgbBytes[i * Width + j * 3 + 2] = (Byte)temp;
                }
            }
            UnlockBits(rgbBytes);
        }

        #endregion

        #region 灰度拉伸

        public void GrayscaleStretch(Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            base.grayscaleStretch(ref tempb);
            base.grayscaleStretch(ref tempg);
            base.grayscaleStretch(ref tempr);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = tempb[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = tempg[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = tempr[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void GrayscaleStretch(Bitmap srcImage, int x1, int y1, int x2, int y2)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Int32 gr = 0, gg = 0, gb = 0;
            for (int i = 0; i < Length; i += 3)
            {
                if (i % RealWidth >= RealWidth)
                {
                    i = (i / Width + 1) * Width - 3;
                    continue;
                }

                gr = grayscaleStretchCalc(rgbBytes[i + 2], x1, y1, x2, y2);
                gg = grayscaleStretchCalc(rgbBytes[i + 1], x1, y1, x2, y2);
                gb = grayscaleStretchCalc(rgbBytes[i], x1, y1, x2, y2);

                rgbBytes[i + 2] = (Byte)gr;
                rgbBytes[i + 1] = (Byte)gg;
                rgbBytes[i] = (Byte)gb;
            }

            UnlockBits(rgbBytes);
        }

        /// <summary>
        ///  灰度拉伸拉伸点算法
        /// </summary>
        private Int32 grayscaleStretchCalc(Int32 x, Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            var g = 0;
            if (x < x1)
                g = (Int32)((y2 / x1) * x);
            else if (x >= x1 && x <= x2)
                g = (Int32)(((y2 - y1) / (x2 - x1)) * (x - x1) + y1);
            else if (x > x2)
                g = (Int32)(((255 - y2) / (255 - x2)) * (x - x2) + y2);
            return g;
        }

        #endregion

        public void Binarization(Bitmap srcImage, int throsholding)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            base.binarization(ref tempb, throsholding);
            base.binarization(ref tempg, throsholding);
            base.binarization(ref tempr, throsholding);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = tempb[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = tempg[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = tempr[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void OptimalThreshold(Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            Byte[] resub;
            base.optimalThreshold(ref tempb, singleWidth, Height, out resub);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resub[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void OstuThreshold(Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Int32 indexb = base.ostuThreshold(ref tempb, singleWidth, Height);
            Int32 indexg = base.ostuThreshold(ref tempg, singleWidth, Height);
            Int32 indexr = base.ostuThreshold(ref tempr, singleWidth, Height);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = rgbBytes[i * Width + j * 3] >= indexb ? (Byte)255 : (Byte)0;
                    rgbBytes[i * Width + j * 3 + 1] = rgbBytes[i * Width + j * 3 + 1] >= indexg ? (Byte)255 : (Byte)0;
                    rgbBytes[i * Width + j * 3 + 2] = rgbBytes[i * Width + j * 3 + 2] >= indexr ? (Byte)255 : (Byte)0;
                }
            }
            UnlockBits(rgbBytes);
        }
    }
}
