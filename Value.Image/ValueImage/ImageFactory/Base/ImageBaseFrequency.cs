using System;
using MathHelper.Infrastructure;
using System.Collections.Generic;
using ValueImage.Infrastructure;
using System.Drawing;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  快速傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        protected void FFT(ref Byte[] data, Int32 width, Int32 height, Boolean inv)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, inv, out tempComp);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (Byte)tempComp[i].Real;
            }
        }

        /// <summary>
        ///  快速傅里叶变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <param name="result">返回的结果</param>
        protected void FFT(ref Byte[] data, Int32 width, Int32 height, Boolean inv, out Complex[] result)
        {
            Int32 length = width * height;
            Byte[] tempBytes = (Byte[])data.Clone();
            Complex[] tempComp = new Complex[length];


            for (int i = 0; i < length; i++)
            {
                if (inv)
                {
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        tempComp[i] = new Complex(data[i], 0);
                    else
                        tempComp[i] = new Complex(-data[i], 0);
                }
                else
                    tempComp[i] = new Complex(data[i], 0);
            }

            // 水平方向变化
            Complex[] tempCompH = new Complex[width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = valueMath.FFT(tempCompH, width);
                for (int j = 0; j < width; j++)
                {
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            // 垂直方向变化
            Complex[] tempCompVe = new Complex[height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = valueMath.FFT(tempCompVe, height);

                for (int j = 0; j < height; j++)
                {
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }
            result = tempComp;
        }

        /// <summary>
        ///  快速傅里叶你变化
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <param name="result">返回的结果</param>
        protected void IFFT(ref Complex[] data, Int32 width, Int32 height, Boolean inv, out Byte[] result)
        {
            Int32 length = width * height;
            Complex[] tempComp = (Complex[])data.Clone();
            Complex[] tempCompH = new Complex[width];
            // 水平方向变化
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempCompH[j] = tempComp[i * width + j];
                }
                tempCompH = valueMath.IFFT(tempCompH, width);
                for (int j = 0; j < width; j++)
                {
                    tempComp[i * width + j] = tempCompH[j];
                }
            }

            Complex[] tempCompVe = new Complex[height];
            // 垂直方向变化
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempCompVe[j] = tempComp[j * width + i];
                }
                tempCompVe = valueMath.IFFT(tempCompVe, height);
                for (int j = 0; j < height; j++)
                {
                    tempComp[j * width + i] = tempCompVe[j];
                }
            }

            result = new Byte[length];
            Double temp = 0;
            // 赋值,保留实数部分
            for (int i = 0; i < length; i++)
            {
                if (inv)
                {
                    //坐标轴位移变化
                    if ((i / width + i % width) % 2 == 0)
                        temp = tempComp[i].Real;
                    else
                        temp = -tempComp[i].Real;
                }
                else
                    temp = tempComp[i].Real;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;

                result[i] = Convert.ToByte(temp);
            }
        }

        /// <summary>
        ///  幅度图像
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void amplitude(ref Byte[] data, Int32 width, Int32 height)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, true, out tempComp);
            Double[] tempData = new Double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                tempData[i] = System.Math.Log(1 + tempComp[i].Abs(), 2);
            }

            #region 灰度拉伸

            Double max =  MathHelper.ValueMath.Max(tempData);
            Double min =  MathHelper.ValueMath.Min(tempData);
            Double p = 255.0 / (max - min);
            Double temp;
            for (int i = 0; i < data.Length; i++)
            {
                temp = p * (tempData[i] - min) + 0.5;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }

            #endregion
        }

        /// <summary>
        ///  相位图像
        /// </summary>
        /// <param name="data">二维数据(必须是2的幂次方)</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据宽度</param>
        protected void phase(ref Byte[] data, Int32 width, Int32 height)
        {
            Complex[] tempComp;
            this.FFT(ref data, width, height, false, out tempComp);
            Double[] tempData = new Double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                tempData[i] = tempComp[i].Angle() + 2 * System.Math.PI;
            }

            #region 灰度拉伸

            Double max =  MathHelper.ValueMath.Max(tempData);
            Double min =  MathHelper.ValueMath.Min(tempData);
            Double p = 255.0 / (max - min);
            Double temp;
            for (int i = 0; i < data.Length; i++)
            {
                temp = p * (tempData[i] - min) + 0.5;
                temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                data[i] = Convert.ToByte(temp);
            }

            #endregion
        }

        /// <summary>
        ///  盖博滤波
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="gaborParam">盖博参数</param>
        /// <param name="result">返回的结果</param>
        protected void gabor(ref Byte[] data, Int32 width, Int32 height, GaborParam gaborParam, out Byte[] result)
        {
            result = new Byte[data.Length];
            Double temp;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Int32 index = j * width + i;
                    temp = data[index] * valueMath.Gabor(i, j, gaborParam).Real;
                    temp = temp > 255 ? 255 : temp < 0 ? 0 : temp;
                    result[index] = (Byte)temp;
                }
            }
        }

        /// <summary>
        ///  投影
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="orientType">投影的方向(水平或垂直)</param>
        /// <param name="result">投影的结果</param>
        protected void projection(ref Byte[] data, Int32 width, Int32 height, ValueImage.Infrastructure.OrientationType orientType, out Int32[] result)
        {
            this.projection(ref data, width, height, orientType, true, out result);
        }

        /// <summary>
        ///  投影
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="orientType">投影的方向(水平或垂直)</param>
        /// <param name="average">是否去平均值</param>
        /// <param name="result">投影的结果</param>
        protected void projection(ref Byte[] data, Int32 width, Int32 height, ValueImage.Infrastructure.OrientationType orientType, Boolean average, out Int32[] result)
        {
            if (orientType == Infrastructure.OrientationType.Horizontal)
            {
                result = new Int32[height];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        result[i] += data[i * width + j];
                    }
                    if (average)
                        result[i] /= width;
                }
            }
            else if (orientType == Infrastructure.OrientationType.Vertical)
            {
                result = new Int32[width];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        result[i] += data[j * width + i];
                    }
                    if (average)
                        result[i] /= height;
                }
            }
            else
            {
                result = new Int32[0];
            }
        }

        /// <summary>
        ///  投影
        /// </summary>
        protected void projection(ref Byte[] data, Int32 width, Int32 height, Double angle, ValueImage.Infrastructure.OrientationType orientType, out Int32[] result)
        {
            if (angle == System.Math.PI / 2)
            {
                projection(ref data, width, height, orientType, false, out result);
                return;
            }

            if (orientType == OrientationType.Horizontal)
            {
                result = new Int32[height];
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Int32 y = ((Int32)((Double)i / System.Math.Tan(angle)) + j);

                        if (y >= 0 && y < height)
                            result[y] += data[j * width + i];
                    }
                }
            }
            else if (orientType == OrientationType.Vertical)
            {
                result = new Int32[width];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Int32 x = ((Int32)((Double)j / System.Math.Tan(angle)) + i);
                        if (x >= 0 && x < width)
                            result[j * width + x] += data[j * width + i];
                    }
                }
            }
            else
                result = new Int32[0];
        }

        /// <summary>
        ///  确定图像密度矩形
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="endureWidth">容忍空白区域宽度</param>
        /// <returns>确定的密度矩形</returns>
        protected void density(ref Byte[] data, Int32 width, Int32 height, Int32 endureWidth, out Rectangle[] result)
        {
            List<LinePoint[]> lineList = new List<LinePoint[]>();
            List<LinePoint> lines = new List<LinePoint>();
            Int32 start = -1, whiteCount = 0, index = 0;

            #region 获取存在黑色像素水平线
            for (int i = 0; i < height; i++)
            {
                lines.Clear();
                whiteCount = 0;
                start = -1;
                for (int j = 0; j < width; j++)
                {
                    index = i * width + j;
                    if (data[index] == 0 && start == -1)
                        start = j;

                    if (data[index] == 255 && start != -1)
                        whiteCount++;
                    else
                        whiteCount = 0;

                    if (whiteCount > endureWidth)
                    {
                        lines.Add(new LinePoint
                        {
                            Start = new System.Drawing.Point(start, i),
                            End = new System.Drawing.Point(j - whiteCount, i)
                        });
                        whiteCount = 0;
                        start = -1;
                    }
                }
                if (start != -1)
                    lines.Add(new LinePoint
                    {
                        Start = new System.Drawing.Point(start, i),
                        End = new System.Drawing.Point(width - 1 - whiteCount, i)
                    });

                if (lines.Count > 0)
                    lineList.Add(lines.ToArray());
            }
            #endregion

            #region 确定构成矩阵的水平线组
            for (int i = 0; i < lineList.Count - 1; i++)
            {
                for (int j = 0; j < lineList[i].Length; j++)
                {
                    for (int x = 0; x < lineList[i + 1].Length; x++)
                    {
                        if ((lineList[i + 1][x].Start.Y - lineList[i][j].Start.Y) > endureWidth) continue;

                        if (((Double)(lineList[i + 1][x].End.X - lineList[i + 1][x].Start.X)) / (lineList[i][j].End.X - lineList[i][j].Start.X) < 0.3) continue;

                        if (((lineList[i + 1][x].Start.X + endureWidth > lineList[i][j].Start.X) && (lineList[i + 1][x].Start.X - endureWidth < lineList[i][j].End.X)) ||
                            (lineList[i + 1][x].Start.X < lineList[i][j].Start.X && lineList[i + 1][x].End.X > lineList[i][j].End.X))
                            lineList[i][j].NearLines.Add(lineList[i + 1][x]);
                    }
                }
            }
            #endregion

            #region 分析水平线组确定矩阵
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < lineList.Count; i++)
            {
                for (int j = 0; j < lineList[i].Length; j++)
                {
                    if (lineList[i][j].Sorted)
                        continue;

                    Rectangle rect = new Rectangle();
                    Int32 startRow = lineList[i][j].Start.Y, endRow = startRow;
                    Int32 startCol = lineList[i][j].Start.X, endCol = startCol;
                    LinePoint temp = lineList[i][j];
                    Queue<LinePoint> queue = new Queue<LinePoint>();
                    queue.Enqueue(temp);
                    temp.Sorted = true;
                    do
                    {
                        LinePoint line = queue.Dequeue();
                        endRow = line.Start.Y > endRow ? line.Start.Y : endRow;
                        startCol = line.Start.X < startCol ? line.Start.X : startCol;
                        endCol = line.End.X > endCol ? line.End.X : endCol;

                        List<LinePoint> nearLines = line.NearLines;
                        for (int x = 0; x < nearLines.Count; x++)
                        {
                            nearLines[x].Sorted = true;
                            queue.Enqueue(nearLines[x]);
                        }
                    } while (queue.Count > 0);

                    rect.X = startCol;
                    rect.Y = startRow;
                    rect.Width = endCol - startCol;
                    rect.Height = endRow - startRow;
                    rectangles.Add(rect);
                }
            }
            #endregion

            result = rectangles.ToArray();
        }

        /// <summary>
        ///  直线(两点,即附近关联直线)
        /// </summary>
        protected class LinePoint
        {
            public Point Start { get; set; }
            public Point End { get; set; }

            private List<LinePoint> nearLines;
            public List<LinePoint> NearLines
            {
                get
                {
                    if (nearLines == null)
                        nearLines = new List<LinePoint>();
                    return nearLines;
                }
                set
                {
                    nearLines = value;
                }
            }

            public Boolean Sorted { get; set; }
        }

        /// <summary>
        ///  确定噪声点
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="result">噪声点</param>
        protected void noisePoint(ref Byte[] data, Int32 width, Int32 height, out Point[] result)
        {
            Int32[] set = null;
            List<Point> points = new List<Point>();
            for (int i = 2; i < height - 2; i++)
            {
                for (int j = 2; j < width - 2; j++)
                {
                    Int32 index = i * width + j;
                    if (data[index] == 255) continue;
                    set = getFilterWindow5x5(index, width, height, DirectType.Clock);

                    Int32 start = 5;
                    Boolean connected = false;
                    for (int x = 2; x < 4; x += 2)
                    {
                        start += 4;

                        if (data[set[x]] == 225) continue;
                        for (int y = 0; y < 5; y++)
                        {
                            if (data[set[start + y]] == 0)
                            {
                                connected = true;
                                break;
                            }
                        }
                        if (connected) break;
                    }

                    if (connected) continue;
                    points.Add(new Point(j, i));
                }
            }

            result = points.ToArray();
        }
    }
}