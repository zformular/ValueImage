using System;
using ValueImage.Interface;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IEdge
    {
        #region IEdge 成员

        public void MaskEgde(System.Drawing.Bitmap srcImage, Infrastructure.MaskType type, int thresholding)
        {
            switch (type)
            {
                case MaskType.Roberts:
                    this.RobertsEgde(srcImage, thresholding);
                    break;
                case MaskType.Prewitt:
                    this.PrewittEgde(srcImage, thresholding);
                    break;
                case MaskType.Sobel:
                    this.SobelEgde(srcImage, thresholding);
                    break;
                case MaskType.Laplacian1:
                    this.LaplacianEgde(srcImage, thresholding, 1);
                    break;
                case MaskType.Laplacian2:
                    this.LaplacianEgde(srcImage, thresholding, 2);
                    break;
                case MaskType.Laplacian3:
                    this.LaplacianEgde(srcImage, thresholding, 3);
                    break;
                case MaskType.Kirsch:
                    this.KirschEgde(srcImage, thresholding);
                    break;
            }
        }

        public void RobertsEgde(System.Drawing.Bitmap srcImage, int thresholding)
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
            base.robertsEdge(ref tempb, singleWidth, Height, thresholding, out resub);
            base.robertsEdge(ref tempg, singleWidth, Height, thresholding, out resug);
            base.robertsEdge(ref tempr, singleWidth, Height, thresholding, out resur);

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

        public void PrewittEgde(System.Drawing.Bitmap srcImage, int thresholding)
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
            base.prewittEdge(ref tempb, singleWidth, Height, thresholding, out resub);
            base.prewittEdge(ref tempg, singleWidth, Height, thresholding, out resug);
            base.prewittEdge(ref tempr, singleWidth, Height, thresholding, out resur);

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

        public void SobelEgde(System.Drawing.Bitmap srcImage, int thresholding)
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
            base.sobelEdge(ref tempb, singleWidth, Height, thresholding, out resub);
            base.sobelEdge(ref tempg, singleWidth, Height, thresholding, out resug);
            base.sobelEdge(ref tempr, singleWidth, Height, thresholding, out resur);

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

        public void LaplacianEgde(System.Drawing.Bitmap srcImage, int thresholding, int number)
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
            base.laplacainEdge(ref tempb, singleWidth, Height, thresholding, number, out resub);
            base.laplacainEdge(ref tempg, singleWidth, Height, thresholding, number, out resug);
            base.laplacainEdge(ref tempr, singleWidth, Height, thresholding, number, out resur);

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

        public void KirschEgde(System.Drawing.Bitmap srcImage, int thresholding)
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
            base.kirschEdge(ref tempb, singleWidth, Height, thresholding, out resub);
            base.kirschEdge(ref tempg, singleWidth, Height, thresholding, out resug);
            base.kirschEdge(ref tempr, singleWidth, Height, thresholding, out resur);

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

        public void GaussEgde(System.Drawing.Bitmap srcImage, Infrastructure.GaussFilterType type, double sigma, double thresholding)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Double[] filt = null;
            if (type == GaussFilterType.LoG)
                filt = base.LogTemplate(sigma);
            else if (type == GaussFilterType.DoG)
                filt = base.DogTemplate(sigma);

            Int32 singleWidth = RealWidth / 3;
            Byte[] tempb = new Byte[singleWidth * Height];
            Byte[] tempg = new Byte[singleWidth * Height];
            Byte[] tempr = new Byte[singleWidth * Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            Double[] doub, doug, dour;
            this.gaussConv(ref tempb, singleWidth, Height, filt, out doub);
            this.gaussConv(ref tempg, singleWidth, Height, filt, out doug);
            this.gaussConv(ref tempr, singleWidth, Height, filt, out dour);

            base.zeroCross(ref doub, singleWidth, Height, thresholding, out tempb);
            base.zeroCross(ref doug, singleWidth, Height, thresholding, out tempg);
            base.zeroCross(ref dour, singleWidth, Height, thresholding, out tempr);

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

        public void CannyEgde(System.Drawing.Bitmap srcImage, double sigma, byte[] thresholding)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
