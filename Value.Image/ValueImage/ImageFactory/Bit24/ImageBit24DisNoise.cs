using System;
using ValueImage.Interface;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IDisNoise
    {
        #region IDisNoise 成员

        public void Erode(System.Drawing.Bitmap srcImage, Infrastructure.FilterWindowType type)
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
            base.erode(ref tempb, singleWidth, Height, type, out resub);
            base.erode(ref tempg, singleWidth, Height, type, out resug);
            base.erode(ref tempr, singleWidth, Height, type, out resur);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void Delation(System.Drawing.Bitmap srcImage, Infrastructure.FilterWindowType type)
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
            base.delation(ref tempb, singleWidth, Height, type, out resub);
            base.delation(ref tempg, singleWidth, Height, type, out resug);
            base.delation(ref tempr, singleWidth, Height, type, out resur);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void Open(System.Drawing.Bitmap srcImage, Infrastructure.FilterWindowType type)
        {
            this.Erode(srcImage, type);
            this.Delation(srcImage, type);
        }

        public void Close(System.Drawing.Bitmap srcImage, Infrastructure.FilterWindowType type)
        {
            this.Delation(srcImage, type);
            this.Erode(srcImage, type);
        }

        public void GrayDelation(System.Drawing.Bitmap srcImage, Byte[] template)
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
            base.grayDelation(ref tempb, singleWidth, Height, template, out resub);
            base.grayDelation(ref tempg, singleWidth, Height, template, out resug);
            base.grayDelation(ref tempr, singleWidth, Height, template, out resur);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void GrayErode(System.Drawing.Bitmap srcImage, byte[] template)
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
            base.grayErode(ref tempb, singleWidth, Height, template, out resub);
            base.grayErode(ref tempg, singleWidth, Height, template, out resug);
            base.grayErode(ref tempr, singleWidth, Height, template, out resur);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        public void GrayOpen(System.Drawing.Bitmap srcImage, byte[] template)
        {
            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] != 1)
                    template[i] = 255;
            }

            this.GrayErode(srcImage, template);

            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] != 1)
                    template[i] = 0;
            }

            this.GrayDelation(srcImage, template);
        }

        public void GrayClose(System.Drawing.Bitmap srcImage, byte[] template)
        {
            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] != 1)
                    template[i] = 0;
            }
            this.GrayDelation(srcImage, template);
            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] != 1)
                    template[i] = 255;
            }
            this.GrayErode(srcImage, template);
        }

        public void GrayMorphologic(System.Drawing.Bitmap srcImage, byte[] template)
        {
            this.GrayClose(srcImage, template);
            this.GrayOpen(srcImage, template);
        }

        public void kFillFilter(System.Drawing.Bitmap srcImage)
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
            base.kfillFilter(ref tempb, singleWidth, Height);
            base.kfillFilter(ref tempg, singleWidth, Height);
            base.kfillFilter(ref tempr, singleWidth, Height);

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

        #endregion
    }
}
