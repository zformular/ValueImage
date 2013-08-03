using System;
using ValueImage.Interface;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IBinarization
    {
        #region IBinarization 成员

        int[] IBinarization.Projection(System.Drawing.Bitmap srcImage, Infrastructure.OrientationType orientType)
        {
            return ((IBinarization)this).Projection(srcImage, System.Math.PI / 2, orientType);
        }

        int[] IBinarization.Projection(System.Drawing.Bitmap srcImage, double angle, Infrastructure.OrientationType orientType)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3].Equals(Byte.MinValue) ? (Byte)1 : (Byte)0;
                }
            }
            UnlockBits(rgbBytes);

            Int32[] result;
            base.projectionV2(ref data, singleWidth, Height, angle, orientType, out result);
            return result;
        }

        void IBinarization.RobortsEdge(System.Drawing.Bitmap srcImage, int threshold)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            Byte[] resub;
            base.robertsEdge(ref data, singleWidth, Height, threshold, out resub);

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

        void IBinarization.kFillFilter(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            base.kfillFilter(ref data, singleWidth, Height);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = data[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        System.Drawing.Bitmap IBinarization.CutRectangle(System.Drawing.Bitmap srcImage, int startRow, int startCol, int endRow, int endCol)
        {
            if (endCol <= startCol || endRow <= startRow) return srcImage;

            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);

            Byte[] resub;
            base.cutRectangle(ref data, singleWidth, startRow, startCol, endRow, endCol, out resub);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resub, ref resub, endCol - startCol, endRow - startRow, System.Drawing.Imaging.PixelFormat.Format24bppRgb, out result);
            return result;
        }

        System.Drawing.Bitmap IBinarization.BileanerZoom(System.Drawing.Bitmap srcImage, int width, int height)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);

            Byte[] resub;
            bileanerZoom(ref data, singleWidth, Height, width, height, out resub);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resub, ref resub, width, height, srcImage.PixelFormat, out result);
            return result;
        }

        System.Drawing.Bitmap IBinarization.BileanerRotate(System.Drawing.Bitmap srcImage, double angle)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);

            #region 根据旋转的角度获取目标图片大小
            angle = (360 - angle % 360) % 360;
            Double degree = System.Math.PI / 180 * angle;

            Double sin = System.Math.Sin(degree);
            Double cos = System.Math.Cos(degree);
            Int32 medianX = (singleWidth + 1) / 2;
            Int32 medianY = (Height + 1) / 2;

            Int32[] setX = new Int32[] { 0, singleWidth };
            Int32[] setY = new Int32[] { 0, Height };
            Int32 xx = 0, yy = 0, tempX, tempY;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    tempX = System.Math.Abs((Int32)System.Math.Ceiling((setX[i] - medianX) * cos - (setY[j] - medianY) * sin + medianX));
                    tempY = System.Math.Abs((Int32)System.Math.Ceiling((setX[i] - medianX) * sin + (setY[j] - medianY) * cos + medianY));

                    xx = xx < tempX ? tempX : xx;
                    yy = yy < tempY ? tempY : yy;
                }
            }

            Int32 width = System.Math.Abs(xx - singleWidth) * 2 + singleWidth;
            Int32 height = System.Math.Abs(yy - Height) * 2 + Height;
            #endregion

            Byte[] resub;
            bilinearRotate(ref data, singleWidth, Height, degree, width, height, out resub);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resub, ref resub, width, height, srcImage.PixelFormat, out result);
            return result;
        }

        void IBinarization.HilditchThinning(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            Byte[] result;
            base.hilditchThinning(ref data, singleWidth, Height, out result);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = data[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        void IBinarization.ZhangThinning(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            Byte[] result;
            base.zhangThinning(ref data, singleWidth, Height, out result);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = result[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        int IBinarization.OffsetAngle(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);

            Int32 angle;
            base.offsetAngle(ref data, singleWidth, Height, out angle);
            return angle;
        }

        void IBinarization.ConvertToBytes(System.Drawing.Bitmap srcImage, out byte[,] data)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;

            data = new Byte[Height, singleWidth];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i, j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);
        }

        void IBinarization.NosieKiller(System.Drawing.Bitmap srcImage, Infrastructure.FilterLevelType level)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            base.noiseKiller(ref data, singleWidth, Height, level);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = data[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        void IBinarization.FillBreakpoint(System.Drawing.Bitmap srcImage, Infrastructure.FilterLevelType level)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            base.fileBreakpoint(ref data, singleWidth, Height, level);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    rgbBytes[i * Width + j * 3] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 1] = data[i * singleWidth + j];
                    rgbBytes[i * Width + j * 3 + 2] = data[i * singleWidth + j];
                }
            }
            UnlockBits(rgbBytes);
        }

        System.Drawing.Bitmap IBinarization.ShewCorrection(System.Drawing.Bitmap srcImage, Infrastructure.ShewDetectionType type)
        {
            Double angle = ((IBinarization)this).ShewDetection(srcImage, type);
            return ((IBinarization)this).BileanerRotate(srcImage, 90 - angle);
        }

        double IBinarization.ShewDetection(System.Drawing.Bitmap srcImage, Infrastructure.ShewDetectionType type)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                }
            }
            UnlockBits(rgbBytes);

            Double angle = 0.0;
            if (type == Infrastructure.ShewDetectionType.Projection)
                base.shewCorrectProjection(ref data, singleWidth, Height, out angle);
            return angle;
        }

        byte[,] IBinarization.ConvertToBytes(System.Drawing.Bitmap srcImage, int startRow, int startCol, int endRow, int endCol)
        {
            Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] data = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {
                    data[i * singleWidth + j] = rgbBytes[i * Width + j * 3].Equals(0) ? (Byte)1 : (Byte)0;
                }
            }
            UnlockBits(rgbBytes);
            Byte[,] result;
            base.convertToByte(ref data, singleWidth, startRow, startCol, endRow, endCol, out result);
            return result;
        }

        void IBinarization.MedianFilter(System.Drawing.Bitmap srcImage, Infrastructure.TemplateType type)
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
            base.medianFilter(ref tempb, singleWidth, Height, type, out resub);

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
