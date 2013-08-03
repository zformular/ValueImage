using System;
using ValueImage.Interface;
using MathHelper.Infrastructure;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IFrequency
    {
        #region IFrequency 成员

        public void FFT(System.Drawing.Bitmap srcImage, bool inv)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.FFT(ref tempb, singleWidth, Height, inv);
            base.FFT(ref tempg, singleWidth, Height, inv);
            base.FFT(ref tempr, singleWidth, Height, inv);

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

        public void Amplitude(System.Drawing.Bitmap srcImage)
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
            base.amplitude(ref tempb, singleWidth, Height);
            base.amplitude(ref tempg, singleWidth, Height);
            base.amplitude(ref tempr, singleWidth, Height);

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

        public void Phase(System.Drawing.Bitmap srcImage)
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
            base.phase(ref tempb, singleWidth, Height);
            base.phase(ref tempg, singleWidth, Height);
            base.phase(ref tempr, singleWidth, Height);

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

        public void Gabor(System.Drawing.Bitmap srcImage, GaborParam gaborParam)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            Byte[] resub;
            base.gabor(ref tempb, singleWidth, Height, gaborParam, out resub);

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

        public int[] Projection(System.Drawing.Bitmap srcImage, ValueImage.Infrastructure.OrientationType orientType)
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
            UnlockBits(rgbBytes);

            Int32[] resub, resug, resur;
            base.projection(ref tempb, singleWidth, Height, orientType, out resub);
            base.projection(ref tempg, singleWidth, Height, orientType, out resug);
            base.projection(ref tempr, singleWidth, Height, orientType, out resur);

            Int32[] result = new Int32[resub.Length];
            for (int i = 0; i < resub.Length; i++)
            {
                Int32 temp = resub[i] + resug[i] + resur[i];
                result[i] = temp / 3;
            }

            return result;
        }

        public int[] Projection(System.Drawing.Bitmap srcImage, double angle, Infrastructure.OrientationType orientType)
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
            UnlockBits(rgbBytes);

            Int32[] resub, resug, resur;
            base.projection(ref tempb, singleWidth, Height, angle, orientType, out resub);
            base.projection(ref tempg, singleWidth, Height, angle, orientType, out resug);
            base.projection(ref tempr, singleWidth, Height, angle, orientType, out resur);

            Int32[] result = new Int32[resub.Length];
            for (int i = 0; i < resub.Length; i++)
            {
                Int32 temp = resub[i] + resug[i] + resur[i];
                result[i] = temp / 3;
            }

            return result;
        }

        public System.Drawing.Rectangle[] Density(System.Drawing.Bitmap srcImage, Int32 endureWidth)
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
            UnlockBits(rgbBytes);
            System.Drawing.Rectangle[] rectangles;
            base.density(ref tempb, singleWidth, Height, endureWidth, out rectangles);

            return rectangles;
        }

        #endregion
    }
}
