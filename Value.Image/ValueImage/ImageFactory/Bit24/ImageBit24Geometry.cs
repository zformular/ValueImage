using System;
using ValueImage.Interface;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IGeometry
    {
        #region IGeometry 成员

        public void FillRectangle(System.Drawing.Bitmap srcImage, int startRow, int startCol, int endRow, int endCol, System.Drawing.Color color)
        {
            if (endCol < startCol || endRow < startRow) return;

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
            base.fillRectangle(ref tempb, singleWidth, startRow, startCol, endRow, endCol, color.B);
            base.fillRectangle(ref tempg, singleWidth, startRow, startCol, endRow, endCol, color.G);
            base.fillRectangle(ref tempr, singleWidth, startRow, startCol, endRow, endCol, color.R);

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

        public void LinearChange(System.Drawing.Bitmap srcImage, float slope, float displacements)
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
            base.linearChange(ref tempb, slope, displacements);
            base.linearChange(ref tempg, slope, displacements);
            base.linearChange(ref tempr, slope, displacements);

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

        public void Move(System.Drawing.Bitmap srcImage, Int32 x, Int32 y)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Byte[] tempArray = new Byte[rgbBytes.Length];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = 255;
            }

            Int32 extend = Width - RealWidth;
            for (int i = 0; i < Length; i += 3)
            {
                if (i % RealWidth >= RealWidth)
                {
                    i = (i / Width + 1) * Width - 3;
                    continue;
                }

                // 行
                Int32 row = i / Width;
                // 列
                Int32 col = i % Width;

                Int32 nx = col + 3 + x * 3;
                if (nx > RealWidth) continue;
                if (nx < 0) continue;

                Int32 ny = (row + y) * Width;
                if (row + y > Height) break;

                Int32 newPos = nx + ny;
                if (newPos < 0) continue;
                if (newPos >= Length) break;

                tempArray[newPos] = rgbBytes[i];
                tempArray[newPos + 1] = rgbBytes[i + 1];
                tempArray[newPos + 2] = rgbBytes[i + 2];
            }

            rgbBytes = (Byte[])tempArray.Clone();

            UnlockBits(rgbBytes);
        }

        public void HoriMirror(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Int32 splitPoint = 0;

            // 奇偶取中值的区别
            if (RealWidth / 3 % 2 == 0)
                splitPoint = RealWidth / 6 * 3;
            else
                splitPoint = ((RealWidth / 3 - 1) / 2) * 3;

            Byte[] tempArray = new Byte[Length];
            for (int i = 0; i < Length; i++)
            {
                tempArray[i] = 255;
            }

            for (int i = 0; i < Length; i += 3)
            {
                if (i % RealWidth >= RealWidth)
                {
                    i = (i / Width + 1) * Width - 3;
                    continue;
                }

                Int32 row = i / Width;
                Int32 col = i % Width;

                Int32 nx = 2 * splitPoint - col;
                Int32 newPos = row * Width + nx;
                tempArray[newPos] = rgbBytes[i];
                tempArray[newPos + 1] = rgbBytes[i + 1];
                tempArray[newPos + 2] = rgbBytes[i + 2];
                tempArray[i] = rgbBytes[newPos];
                tempArray[i + 1] = rgbBytes[newPos + 1];
                tempArray[i + 2] = rgbBytes[newPos + 2];

                if (col == splitPoint)
                {
                    i = (row + 1) * Width;
                    continue;
                }
            }
            rgbBytes = (Byte[])tempArray.Clone();

            UnlockBits(rgbBytes);
        }

        public void VertMirror(System.Drawing.Bitmap srcImage)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Int32 splitPoint = 0;

            // 奇偶取中值的区别
            if (Height % 2 == 0)
                splitPoint = Height / 2;
            else
                splitPoint = (Height - 1) / 2;

            Byte[] tempArray = new Byte[Length];
            for (int i = 0; i < Length; i++)
            {
                tempArray[i] = 255;
            }

            for (int i = 0; i < Length; i += 3)
            {
                if (i % RealWidth >= RealWidth)
                {
                    i = (i / Width + 1) * Width - 3;
                    continue;
                }

                Int32 row = i / Width;
                Int32 col = i % Width;

                Int32 ny = 2 * splitPoint - row - 1;
                Int32 newPos = ny * Width + col;
                tempArray[newPos] = rgbBytes[i];
                tempArray[newPos + 1] = rgbBytes[i + 1];
                tempArray[newPos + 2] = rgbBytes[i + 2];
                tempArray[i] = rgbBytes[newPos];
                tempArray[i + 1] = rgbBytes[newPos + 1];
                tempArray[i + 2] = rgbBytes[newPos + 2];

                if (ny == splitPoint && col == RealWidth)
                    break;
            }
            rgbBytes = (Byte[])tempArray.Clone();
            UnlockBits(rgbBytes);
        }

        public void Rotate(System.Drawing.Bitmap srcImage, int degree)
        {
            //Byte[] rgbBytes = LockBits(srcImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            //Int32 singleWidth = RealWidth / 3;
            //Int32 length = singleWidth * Height;
            //Byte[] tempb = new Byte[length];
            //Byte[] tempg = new Byte[length];
            //Byte[] tempr = new Byte[length];
            //for (int i = 0; i < Height; i++)
            //{
            //    for (int j = 0; j < singleWidth; j++)
            //    {
            //        tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
            //        tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
            //        tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
            //    }
            //}
            //Byte[] resub, resug, resur;
            //base.amphilinearityRotate(ref tempb, singleWidth, Height, degree, out resub);
            //base.amphilinearityRotate(ref tempg, singleWidth, Height, degree, out resug);
            //base.amphilinearityRotate(ref tempr, singleWidth, Height, degree, out resur);

            //for (int i = 0; i < Height; i++)
            //{
            //    for (int j = 0; j < singleWidth; j++)
            //    {
            //        rgbBytes[i * Width + j * 3] = resub[i * singleWidth + j];
            //        rgbBytes[i * Width + j * 3 + 1] = resug[i * singleWidth + j];
            //        rgbBytes[i * Width + j * 3 + 2] = resur[i * singleWidth + j];
            //    }
            //}
            //UnlockBits(rgbBytes);

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

            Double sin = System.Math.Sin(degree);
            Double cos = System.Math.Cos(degree);

            Int32 medianX = singleWidth / 2;
            Int32 medianY = Height / 2;

            Int32 growX = (Int32)System.Math.Abs(System.Math.Ceiling(medianX * cos - medianY * sin) - medianX);
            Int32 growY = (Int32)System.Math.Ceiling(medianX * sin + medianY * cos);

            Int32 newWidth = singleWidth + growX * 2;
            Int32 newHeight = Height + growY * 2;
            Byte[] resub, resug, resur;
            base.bilinearRotate(ref tempb, singleWidth, Height, degree, newWidth, newHeight, out resub);
            base.bilinearRotate(ref tempg, singleWidth, Height, degree, newWidth, newHeight, out resug);
            base.bilinearRotate(ref tempr, singleWidth, Height, degree, newWidth, newHeight, out resur);

            System.Drawing.Bitmap rstImage;
            this.createImage(ref resub, ref resug, ref resur, newWidth, newHeight, srcImage.PixelFormat, out rstImage);
            rstImage.Save("D:\\test.jpg");
        }

        public System.Drawing.Bitmap BileanerRotate(System.Drawing.Bitmap srcImage, double angle)
        {
            return this.BileanerRotate(srcImage, angle, Color.White);
        }

        public System.Drawing.Bitmap BileanerRotate(System.Drawing.Bitmap srcImage, double angle, System.Drawing.Color backColor)
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

            Byte[] resub, resug, resur;
            bilinearRotate(ref tempb, singleWidth, Height, degree, width, height, backColor.B, out resub);
            bilinearRotate(ref tempg, singleWidth, Height, degree, width, height, backColor.G, out resug);
            bilinearRotate(ref tempr, singleWidth, Height, degree, width, height, backColor.R, out resur);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resug, ref resur, width, height, srcImage.PixelFormat, out result);
            return result;
        }

        public void Zoom(System.Drawing.Bitmap srcImage, Double zoomingX, Double zoomingY, Infrastructure.ZoomType type)
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
            if (type == Infrastructure.ZoomType.NearestNeighbor)
            {
                base.nearestNeighborZoom(ref tempb, singleWidth, Height, zoomingX, zoomingY, out resub);
                base.nearestNeighborZoom(ref tempg, singleWidth, Height, zoomingX, zoomingY, out resug);
                base.nearestNeighborZoom(ref tempr, singleWidth, Height, zoomingX, zoomingY, out resur);
            }
            else
            {
                base.amphilinearityZoom(ref tempb, singleWidth, Height, zoomingX, zoomingY, out resub);
                base.amphilinearityZoom(ref tempg, singleWidth, Height, zoomingX, zoomingY, out resug);
                base.amphilinearityZoom(ref tempr, singleWidth, Height, zoomingX, zoomingY, out resur);
            }

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

        public System.Drawing.Bitmap BileanerZoom(System.Drawing.Bitmap srcImage, int width, int height)
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

            Byte[] resub, resug, resur;
            bileanerZoom(ref tempb, singleWidth, Height, width, height, out resub);
            bileanerZoom(ref tempg, singleWidth, Height, width, height, out resug);
            bileanerZoom(ref tempr, singleWidth, Height, width, height, out resur);

            System.Drawing.Bitmap result;
            this.createImage(ref resub, ref resug, ref resur, width, height, srcImage.PixelFormat, out result);
            return result;
        }

        public System.Drawing.Bitmap SpliceImage(Infrastructure.ImageInfo[] infos)
        {
            Int32 width = 0, height = 0, temp;
            for (int i = 0; i < infos.Length; i++)
            {
                temp = infos[i].Location.X + infos[i].Size.Width;
                if (temp > width)
                    width = temp;

                temp = infos[i].Location.Y + infos[i].Size.Height;
                if (temp > height)
                    height = temp;
            }

            System.Drawing.Bitmap orgImage = new System.Drawing.Bitmap(width, height, PixelFormat.Format24bppRgb);
            Byte[] rgbBytes = LockBits(orgImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            Int32 singleWidth = RealWidth / 3;
            Int32 length = singleWidth * Height;
            Byte[] tempb = new Byte[length];
            Byte[] tempg = new Byte[length];
            Byte[] tempr = new Byte[length];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < singleWidth; j++)
                {

                    rgbBytes[i * Width + j * 3] = Byte.MaxValue;
                    rgbBytes[i * Width + j * 3 + 1] = Byte.MaxValue;
                    rgbBytes[i * Width + j * 3 + 2] = Byte.MaxValue;

                    tempb[i * singleWidth + j] = rgbBytes[i * Width + j * 3];
                    tempg[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 1];
                    tempr[i * singleWidth + j] = rgbBytes[i * Width + j * 3 + 2];
                }
            }
            UnlockBits(rgbBytes);

            for (int i = 0; i < infos.Length; i++)
            {
                base.spliceImage(singleWidth, ref tempb, ref tempg, ref tempr, ref infos[i]);
            }

            rgbBytes = LockBits(orgImage, System.Drawing.Imaging.ImageLockMode.ReadWrite);
            singleWidth = RealWidth / 3;
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

            return orgImage;
        }

        #endregion
    }
}
