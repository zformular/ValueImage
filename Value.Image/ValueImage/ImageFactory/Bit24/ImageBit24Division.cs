using System;
using ValueImage.Interface;
using ValueImage.Infrastructure;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IDivision
    {
        #region IDivision 成员

        public System.Drawing.Bitmap CutRectangle(System.Drawing.Bitmap srcImage, int startRow, int startCol, int endRow, int endCol)
        {
            Debug.Assert(endCol > startCol && endRow > startRow, "终止坐标必须大于起始坐标");
            if (endCol < startCol || endRow < startRow) return null;

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
            UnlockBits(rgbBytes);

            Byte[] resub, resug, resur;
            base.cutRectangle(ref tempb, singleWidth, startRow, startCol, endRow, endCol, out resub);
            base.cutRectangle(ref tempg, singleWidth, startRow, startCol, endRow, endCol, out resug);
            base.cutRectangle(ref tempr, singleWidth, startRow, startCol, endRow, endCol, out resur);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resug, ref resur, endCol - startCol, endRow - startRow, System.Drawing.Imaging.PixelFormat.Format24bppRgb, out result);

            return result;
        }

        public void UniformQuantization(System.Drawing.Bitmap srcImage, ColorBytes[] prototypeColor)
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

            Double[] temp = new Double[prototypeColor.Length];
            for (int i = 0; i < tempb.Length; i++)
            {
                for (int j = 0; j < prototypeColor.Length; j++)
                {
                    temp[j] = System.Math.Sqrt((tempr[i] - prototypeColor[j][2]) * (tempr[i] - prototypeColor[j][2]) +
                        (tempg[i] - prototypeColor[j][1]) * (tempg[i] - prototypeColor[j][1]) +
                        (tempb[i] - prototypeColor[j][0]) * (tempb[i] - prototypeColor[j][0]));
                }
                Int32 min = 0;
                for (int j = 1; j < temp.Length; j++)
                {
                    if (temp[min] > temp[j])
                        min = j;
                }
                tempb[i] = prototypeColor[min][0];
                tempg[i] = prototypeColor[min][1];
                tempr[i] = prototypeColor[min][2];
            }

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

        public void HilditchThinning(System.Drawing.Bitmap srcImage)
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
            Byte[] resub, resug, resur;
            base.hilditchThinning(ref tempb, singleWidth, Height, out resub);
            base.hilditchThinning(ref tempb, singleWidth, Height, out resug);
            base.hilditchThinning(ref tempb, singleWidth, Height, out resur);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resur[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resug[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void ZhangThinning(System.Drawing.Bitmap srcImage)
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
            Byte[] resub;
            base.zhangThinning(ref tempb, singleWidth, Height, out resub);

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

        public void ZhangExpandThinning(System.Drawing.Bitmap srcImage)
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
            Byte[] resub;
            base.zhangExpandThinning(ref tempb, singleWidth, Height, out resub);

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

        #endregion
    }
}
