using System;
using ValueImage.Interface;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ValueImage.ImageFactory.Bit24
{
    sealed partial class ImageBit24 : IGeometry
    {
        #region IGeometry 成员

        public void FillRectangle(System.Drawing.Bitmap srcImage, int startRow, int startCol, int endRow, int endCol, System.Drawing.Color color)
        {
            Debug.Assert(endCol > startCol && endRow > startRow, "终止坐标必须大于起始坐标");
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

        /// <summary>
        ///  失败的作品
        ///  用算法旋转会出现噪声
        ///  双线性插值法
        /// </summary>
        public void Gyrate(System.Drawing.Bitmap srcImage, int degree)
        {
            Byte[] rgbBytes = LockBits(srcImage, ImageLockMode.ReadWrite);
            Double radian = degree * System.Math.PI / 180.0;
            Double sin = System.Math.Sin(radian);
            Double cos = System.Math.Cos(radian);

            Byte[] tempArray = new Byte[Length];
            for (int i = 0; i < Length; i++)
            {
                tempArray[i] = 255;
            }

            Int32 xz = 0, yz = 0;
            Int32 tempWidth = 0, tempHeight = 0;
            Double tempX, tempY, p, q;

            for (int i = 0; i < Length; i += 3)
            {
                if (i % RealWidth >= RealWidth)
                {
                    i = (i / Width + 1) * Width - 3;
                    continue;
                }

                Int32 row = i / Width;
                Int32 col = i % Width;

                tempHeight = row - HalfHeight;
                tempWidth = (col - HalfWidth) / 3;

                // 以图像的几何中心为坐标原点进行坐标变换
                // 按逆向映射法得到输入图像的坐标
                tempX = tempWidth * cos - tempHeight * sin;
                tempY = tempWidth * sin + tempHeight * cos;

                xz = tempWidth > 0 ? ((Int32)tempX) : ((Int32)(tempX - 1));
                yz = tempHeight > 0 ? ((Int32)tempY) : ((Int32)(tempY - 1));

                // 公式需要用到
                p = tempX - xz;
                q = tempY - yz;

                tempWidth = xz * 3 + HalfWidth;
                tempHeight = yz + HalfHeight;

                if (tempWidth < 0 || (tempWidth + 1) >= Width || tempHeight < 0 || (tempHeight + 1) >= Height)
                {
                    tempArray[i] = 255;
                    tempArray[i + 1] = 255;
                    tempArray[i + 2] = 255;
                }
                else
                {
                    tempArray[i] = (Byte)((1.0F - p) * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth] +
                        (1.0F - p) * q * rgbBytes[(tempHeight + 1) * Width + tempWidth] +
                        p * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth + 3] +
                        p * q * rgbBytes[(tempHeight + 1) * Width + tempWidth + 3]);

                    tempArray[i + 1] = (Byte)((1.0F - p) * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth + 1] +
                       (1.0F - p) * q * rgbBytes[(tempHeight + 1) * Width + tempWidth + 1] +
                       p * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth + 4] +
                       p * q * rgbBytes[(tempHeight + 1) * Width + tempWidth + 4]);

                    tempArray[i + 2] = (Byte)((1.0F - p) * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth + 2] +
                      (1.0F - p) * q * rgbBytes[(tempHeight + 1) * Width + tempWidth + 2] +
                      p * (1.0F - q) * rgbBytes[tempHeight * Width + tempWidth + 5] +
                      p * q * rgbBytes[(tempHeight + 1) * Width + tempWidth + 5]);
                }
            }
            rgbBytes = (Byte[])tempArray.Clone();
            UnlockBits(rgbBytes);
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

        #endregion
    }
}
