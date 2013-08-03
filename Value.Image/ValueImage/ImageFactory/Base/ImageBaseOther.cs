using System;
using ValueImage.Infrastructure;
using System.Collections.Generic;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  确定一般情况下图片偏移角度
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="angle">偏移角度</param>
        protected void offsetAngle(ref Byte[] data, Int32 width, Int32 height, out int angle)
        {
            Double singleDegree = System.Math.PI / 180;
            System.Collections.Generic.List<Int32> list = new System.Collections.Generic.List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] == 0 ? (Byte)1 : (Byte)0;
            }
            for (int i = 30; i < 150; i++)
            {
                Int32[] result;
                this.projection(ref data, width, height, singleDegree * i, Infrastructure.OrientationType.Horizontal, out result);
                valueMath.BubbleDescending(result);
                list.Add(result[0]);
            }

            Int32 max = 0, maxIndex = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (max < list[i])
                {
                    max = list[i];
                    maxIndex = i;
                }
            }
            angle = maxIndex + 30;
        }

        /// <summary>
        ///  去除噪声
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="level">等级</param>
        protected void noiseKiller(ref Byte[] data, Int32 width, Int32 height, FilterLevelType level)
        {
            Int32[] set = null;
            Int32 replaceValue = 1;
            Int32[] tempData = new Int32[data.Length];
            for (int i = 0; i < tempData.Length; i++)
            {
                tempData[i] = data[i];
            }

            #region 用不同编号标记独立的物体快
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Int32 index = j * width + i;
                    if (data[index] == Byte.MinValue)
                    {
                        Byte untiData = (Byte)(Byte.MaxValue - data[index]);
                        if (level == FilterLevelType.Level01)
                        {
                            Int32 count = 0;
                            for (int x = 1; x < set.Length; x++)
                            {
                                if (set[x] == Byte.MaxValue)
                                    count++;
                            }
                            if (count == 8) data[index] = Byte.MaxValue;
                        }
                        else
                        {
                            set = this.getExistFilterWindow3x3(index, width, height, DirectType.Clock);
                            Int32 value = -1;
                            for (int x = 1; x < set.Length; x++)
                            {
                                if (tempData[set[x]] != Byte.MaxValue && tempData[set[x]] != Byte.MinValue)
                                    value = tempData[set[x]];
                            }

                            replaceValue = value == -1 ? ++replaceValue : value;

                            tempData[index] = replaceValue;

                            //this.markObjectRecursion(ref data, index, width, height, Byte.MinValue, replaceValue);
                            //replaceValue++;
                        }
                    }
                }
            }
            #endregion

            if (level == FilterLevelType.Level01) return;
            #region 遍历所有标记的块, 判断是否满足条件
            Int32 border = (Int32)level;
            for (Byte x = 2; x <= replaceValue; x++)
            {
                Int32 count = 0;
                Int32 matchRow = -1;

                #region 判断总个数是否超过边长平方
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Int32 index = j * width + i;
                        if (tempData[index].Equals(x))
                        {
                            if (matchRow == -1)
                                matchRow = j;
                            count++;
                        }
                    }
                }

                // 如果总数超过边长平方 则排除
                if (count > border * border) continue;
                #endregion

                Boolean match = true;
                #region 判断水平长度是否超过边长
                for (int j = matchRow; j < height; j++)
                {
                    count = 0;
                    for (int i = 0; i < width; i++)
                    {
                        Int32 index = j * width + i;
                        if (tempData[index].Equals(x))
                            count++;
                    }
                    if (count > border)
                    {
                        match = false;
                        break;
                    }
                }

                if (!match) continue;
                #endregion

                #region 判断垂直宽度是否超过边长
                for (int i = 0; i < width; i++)
                {
                    count = 0;
                    for (int j = 0; j < height; j++)
                    {
                        Int32 index = j * width + i;
                        if (data[index].Equals(x))
                            count++;
                    }
                    if (count > border)
                    {
                        match = false;
                        break;
                    }
                }
                if (!match) continue;
                #endregion

                for (int j = matchRow; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Int32 index = j * width + i;
                        if (tempData[index].Equals(x))
                            data[index] = Byte.MaxValue;
                    }
                }
            }
            #endregion
        }

        /// <summary>
        ///  填充断点
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="level">等级</param>
        protected void fileBreakpoint(ref Byte[] data, Int32 width, Int32 height, FilterLevelType level)
        {
            Byte[] set = null;
            Int16[][] indexSet = new Int16[][] {
                new Int16[] { 1, 4 }, new Int16[] { 1, 6 }, 
                new Int16[] { 3, 6 }, new Int16[] { 3, 8 }, 
                new Int16[] { 5, 2 }, new Int16[] { 5, 8 }, 
                new Int16[] { 7, 4 }, new Int16[] { 7, 2 } 
            };
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Int32 index = j * width + i;
                    if (data[index].Equals(Byte.MaxValue))
                    {
                        set = getFilterWindow3x3(ref data, i, j, width, height, Byte.MaxValue, DirectType.Clock);

                        if (level == FilterLevelType.Level01)
                        {
                            Int32 count = 0;
                            for (int x = 1; x < set.Length; x++)
                            {
                                if (set[x].Equals(Byte.MinValue)) count++;
                            }

                            //if (count == 2)
                            //{
                            //    if (((set[1] == set[5] && set[5] == Byte.MinValue) && (set[3] == set[7]) && (set[7] == Byte.MaxValue)) ||
                            //        ((set[1] == set[5] && set[5] == Byte.MaxValue) && ((set[3] == set[7]) && (set[7] == Byte.MinValue))))
                            //    {
                            //        data[index] = Byte.MinValue;
                            //        continue;
                            //    }

                            //    for (int x = 0; x < indexSet.Length; x++)
                            //    {
                            //        if ((set[indexSet[x][0]] == set[indexSet[x][1]]) && set[indexSet[x][1]] == Byte.MinValue)
                            //        {
                            //            data[index] = Byte.MinValue;
                            //            break;
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  像素投影 黑点为1, 白点为0
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="angle">角度</param>
        /// <param name="orientType">投影方向</param>
        protected void projectionV2(ref Byte[] data, Int32 width, Int32 height, Double angle, OrientationType orientType, out Int32[] result)
        {
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
                            result[x] += data[j * width + i];
                    }
                }
            }
            else
                result = new Int32[0];
        }

        /// <summary>
        ///  投影法 倾斜校正
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="angle">倾斜的角度</param>
        protected void shewCorrectProjection(ref Byte[] data, Int32 width, Int32 height, out Double angle)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Equals(Byte.MinValue) ? (Byte)1 : (Byte)0;
            }

            List<Double> deviation = new List<double>();
            for (int x = 65; x < 115; x++)
            {
                Double tempAngle = System.Math.PI / 180 * x;
                Int32[] tempList;
                this.projectionV2(ref data, width, height, tempAngle, OrientationType.Horizontal, out tempList);
                deviation.Add(MathHelper.ValueMath.StandardDeviation(tempList));
            }

            Int32 index = 0;
            Double max = 0;
            for (int i = 0; i < deviation.Count; i++)
            {
                if (deviation[i] >= max)
                {
                    max = deviation[i];
                    index = i;
                }
            }

            angle = index + 65;
        }

        /// <summary>
        ///  将指定矩形框内图片转化为数据
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="startRow">起始行</param>
        /// <param name="startCol">起始列</param>
        /// <param name="endRow">终止行</param>
        /// <param name="endCol">终止列</param>
        protected void convertToByte(ref Byte[] data, Int32 width, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol, out Byte[,] result)
        {
            result = new Byte[endRow - startRow, endCol - startCol];
            for (int j = 0; j < endRow - startRow; j++)
            {
                for (int i = 0; i < endCol - startCol; i++)
                {
                    Int32 defIndex = (j + startRow) * width + (i + startCol);
                    result[j, i] = data[defIndex];
                }
            }
        }
    }
}
