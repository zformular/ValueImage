using System;
using ValueImage.Interface;
using ValueImage.Infrastructure;
using System.Diagnostics;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IFilter
    {
        #region IFilter 成员

        public void ComponentFilter(System.Drawing.Bitmap srcImage, Infrastructure.RateFilterType type)
        {
            switch (type)
            {
                case ValueImage.Infrastructure.RateFilterType.LowPass:
                    this.LowpassFilter(srcImage, RateFilterRadius.LowPass);
                    break;
                case ValueImage.Infrastructure.RateFilterType.BandStop:
                    this.BandstopFilter(srcImage, RateFilterRadius.BandStopInner, RateFilterRadius.BandStopOuter);
                    break;
                case ValueImage.Infrastructure.RateFilterType.BandPass:
                    this.BandpassFilter(srcImage, RateFilterRadius.BandPassInner, RateFilterRadius.BandPassOuter);
                    break;
                case ValueImage.Infrastructure.RateFilterType.HighPass:
                    this.HighpassFilter(srcImage, RateFilterRadius.HighPass);
                    break;
                default:
                    break;
            }
        }

        public void LowpassFilter(System.Drawing.Bitmap srcImage, double radius)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.lowpassFilter(ref tempb, singleWidth, Height, radius, out resub);
            base.lowpassFilter(ref tempg, singleWidth, Height, radius, out resug);
            base.lowpassFilter(ref tempr, singleWidth, Height, radius, out resur);

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

        public void HighpassFilter(System.Drawing.Bitmap srcImage, double radius)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.highpassFilter(ref tempb, singleWidth, Height, radius, out resub);
            base.highpassFilter(ref tempg, singleWidth, Height, radius, out resug);
            base.highpassFilter(ref tempr, singleWidth, Height, radius, out resur);

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

        public void BandstopFilter(System.Drawing.Bitmap srcImage, double innerRadius, double outerRadius)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.bandstopFilter(ref tempb, singleWidth, Height, innerRadius, outerRadius, out resub);
            base.bandstopFilter(ref tempg, singleWidth, Height, innerRadius, outerRadius, out resug);
            base.bandstopFilter(ref tempr, singleWidth, Height, innerRadius, outerRadius, out resur);

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

        public void BandpassFilter(System.Drawing.Bitmap srcImage, double innerRadius, double outerRadius)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.bandpassFilter(ref tempb, singleWidth, Height, innerRadius, outerRadius, out resub);
            base.bandpassFilter(ref tempg, singleWidth, Height, innerRadius, outerRadius, out resug);
            base.bandpassFilter(ref tempr, singleWidth, Height, innerRadius, outerRadius, out resur);

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

        public void OrientationFilter(System.Drawing.Bitmap srcImage, int startOrient, int endOrient)
        {
            Debug.Assert(endOrient > startOrient, "终止角度必须大于起始角度");
            if (endOrient < startOrient) return;

            Debug.Assert(endOrient - startOrient < 90, "起始角度与终止角度之间不能大于90");
            if (endOrient - startOrient > 90) return;

            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Debug.Assert(valueMath.IsPow2(singleWidth), "图片宽度必须为2的幂次方,才能调用该方法");
            if (!valueMath.IsPow2(singleWidth)) { base.UnlockBits(); return; }

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
            base.orientationFilter(ref tempb, singleWidth, Height, startOrient, endOrient, out resub);
            base.orientationFilter(ref tempg, singleWidth, Height, startOrient, endOrient, out resug);
            base.orientationFilter(ref tempr, singleWidth, Height, startOrient, endOrient, out resur);

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

        public void MeanFilter(System.Drawing.Bitmap srcImage, Infrastructure.TemplateType type)
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
            base.meanFilter(ref tempb, singleWidth, Height, type, out resub);
            base.meanFilter(ref tempg, singleWidth, Height, type, out resug);
            base.meanFilter(ref tempr, singleWidth, Height, type, out resur);

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

        public void MedianFilter(System.Drawing.Bitmap srcImage, Infrastructure.TemplateType type)
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
            base.medianFilter(ref tempb, singleWidth, Height, type, out resub);
            base.medianFilter(ref tempg, singleWidth, Height, type, out resug);
            base.medianFilter(ref tempr, singleWidth, Height, type, out resur);

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

        public void Wavelet(System.Drawing.Bitmap srcImage, Infrastructure.WaveletLowpassType lowpassType, bool hardThreshold, byte thresholding, int series)
        {
            throw new NotImplementedException();
        }

        public void GaussFilter(System.Drawing.Bitmap srcImage, double sigma)
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
            base.gaussSmooth(ref tempb, singleWidth, sigma, out resub);
            base.gaussSmooth(ref tempg, singleWidth, sigma, out resug);
            base.gaussSmooth(ref tempr, singleWidth, sigma, out resur);

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

        public void StatisticFilter(System.Drawing.Bitmap srcImage, Infrastructure.TemplateType type, double thresholding)
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
            base.statisticFilter(ref tempb, singleWidth, Height, thresholding, type, out resub);
            base.statisticFilter(ref tempg, singleWidth, Height, thresholding, type, out resug);
            base.statisticFilter(ref tempr, singleWidth, Height, thresholding, type, out resur);

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

        #endregion
    }
}
