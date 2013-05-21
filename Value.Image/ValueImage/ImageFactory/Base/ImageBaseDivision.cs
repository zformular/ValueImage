using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValueImage.Infrastructure;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  Hough变化累加器
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        /// <param name="maxHough">最大累计器值</param>
        private void houghAccumulate(ref Byte[] data, Int32 width, Int32 height, out Int32[,] houghMap, out Int32 maxHough)
        {
            houghMap = new Int32[width, height];
            // 累计器清零
            Array.Clear(houghMap, 0, width * height);
            maxHough = 1;
            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 0; j < width - 1; j++)
                {
                    // 判断白色像素点
                    if (data[i * width + j] == 255)
                    {
                        for (int thet = 0; thet < 180; thet++)
                        {
                            Double arc = thet * System.Math.PI / 180;
                            Int32 rho = Convert.ToInt16((j * System.Math.Cos(arc) + i * System.Math.Sin(arc)) / 8) + 90;
                            // 计数器加1
                            houghMap[thet, rho]++;
                            // 计算最大累计器值
                            if (maxHough < houghMap[thet, rho])
                                maxHough = houghMap[thet, rho];
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  切割图片
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="startRow">起始行索引</param>
        /// <param name="startCol">起始列索引</param>
        /// <param name="endRow">终止行索引</param>
        /// <param name="endCol">终止列索引</param>
        /// <param name="result">切割后的数据</param>
        protected void cutRectangle(ref Byte[] data, Int32 width, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol, out Byte[] result)
        {
            result = new Byte[(endCol - startCol) * (endRow - startRow)];
            Int32 index = 0;
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startCol; j < endCol; j++)
                {
                    result[index] = data[i * width + j];
                    index++;
                }
            }
        }

        /// <summary>
        ///  Hilditch细化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void hilditchThinning(ref Byte[] data, Int32 width, Int32 height, out Byte[] result)
        {
            Int32[] tempData = new Int32[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(0))
                    tempData[i] = 1;
            }

            Int32 index;
            Int32[] set;
            Boolean loop = true;
            Int32[] marked = new Int32[data.Length];
            Int32 test = 0;
            while (loop)
            {
                test++;
                //loop = false;
                for (int i = 1; i < width - 1; i++)
                {
                    for (int j = 1; j < height - 1; j++)
                    {
                        index = j * width + i;
                        // 条件1 当前点为1
                        if (tempData[index] == 0) continue;
                        // 条件2 当前点8邻接白点个数>=2,<=6
                        set = getFilterWindow3x3(index, width, height, DirectType.UntiClock);
                        Int32 blackCount = 0;
                        for (int x = 1; x < set.Length; x++)
                            blackCount += (tempData[set[x]]);
                        if (blackCount < 2 || blackCount > 6) continue;
                        // 条件3 当前点8邻接逆时针考虑前一点为0,后一点为1, sp+1 一周后sp仍为1
                        Int32[] curList = new Int32[set.Length];
                        for (int x = 0; x < set.Length; x++)
                        {
                            curList[x] = tempData[set[x]];
                        }
                        Int32 sT = sumT(curList);
                        if (sT != 1) continue;
                        // 条件4 p1,p2,p7值均为0 或者 以p1为中心点的sT值不为1
                        Int32[] tempSet = getFilterWindow3x3(set[1], width, height, DirectType.UntiClock);
                        Int32[] tempList = new Int32[tempSet.Length];
                        for (int x = 0; x < tempSet.Length; x++)
                        {
                            tempList[x] = tempData[tempSet[x]];
                        }
                        sT = sumT(tempList);
                        if ((tempData[set[1]] * tempData[set[3]] * tempData[set[7]] != 0) && (sT == 1)) continue;
                        // 条件5 p1,p3,p5值均为0 或者 以p3为中心点的sT值不为1
                        tempSet = getFilterWindow3x3(set[3], width, height, DirectType.UntiClock);
                        for (int x = 0; x < set.Length; x++)
                        {
                            tempList[x] = tempData[tempSet[x]];
                        }
                        sT = sumT(tempList);
                        if ((tempData[set[1]] * tempData[set[3]] * tempData[set[5]] != 0) && (sT == 1)) continue;
                        marked[index] = 1;
                        loop = true;
                    }
                }

                for (int i = 0; i < data.Length; i++)
                {
                    if (marked[i] == 1)
                    {
                        marked[i] = 0;
                        tempData[i] = 0;
                    }
                }

                if (test == 40) break;
            }

            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (tempData[i].Equals(0))
                    result[i] = 255;
                else
                    result[i] = 0;
            }
        }

        // 计算8邻接0-1 跳转的次数
        private Int32 sumT(Int32[] list)
        {
            Int32 sp = 0;
            for (int i = 1; i < list.Length - 1; i++)
            {
                if (list[i + 1] - list[i] == 1)
                    sp++;
            }
            if (list[1] - list[list.Length - 1] == 1)
                sp++;
            return sp;
        }

        /// <summary>
        ///  Zhange细化
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void zhangThinning(ref Byte[] data, Int32 width, Int32 height, out Byte[] result)
        {
            Int32[] tempData = new Int32[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(0))
                    tempData[i] = 1;
            }

            Int32[] set;
            Int32[] marked = new Int32[data.Length];
            Boolean loop = true;
            while (loop)
            {
                #region 判断1
                loop = false;
                for (int i = 0; i < width - 1; i++)
                {
                    for (int j = 0; j < height - 1; j++)
                    {
                        Int32 index = j * width + i;

                        // 条件1 当前点为1
                        if (tempData[index] == 0) continue;
                        // 条件2 当前点8邻接为黑个数>=2,<=6
                        Int32 blackCount = 0;
                        set = getFilterWindow3x3(index, width, height, DirectType.Clock);
                        for (int x = 1; x < set.Length; x++)
                            blackCount += tempData[set[x]];
                        if (blackCount < 2 || blackCount > 6) continue;
                        // 条件3 当前点8邻接0-1跳转次数
                        Int32[] curList = new Int32[set.Length];
                        for (int x = 0; x < set.Length; x++)
                            curList[x] = tempData[set[x]];
                        Int32 sT = sumT(curList);
                        if (sT != 1) continue;
                        // 条件4 p1,p3,p5 至少有一个为0
                        if (tempData[set[1]] * tempData[set[3]] * tempData[set[5]] != 0) continue;
                        // 条件5 p3,p5,p7 至少有一个为0
                        if (tempData[set[3]] * tempData[set[5]] * tempData[set[7]] != 0) continue;
                        marked[index] = 1;
                        loop = true;
                    }
                }
                for (int i = 0; i < data.Length; i++)
                {
                    if (marked[i] == 1)
                    {
                        tempData[i] = 0;
                        marked[i] = 0;
                    }
                }
                #endregion
                #region 判断2
                for (int i = 1; i < width - 1; i++)
                {
                    for (int j = 1; j < height - 1; j++)
                    {
                        Int32 index = j * width + i;

                        // 条件1 当前点为1
                        if (tempData[index] == 0) continue;
                        // 条件2 当前点8邻接为黑个数>=2,<=6
                        Int32 blackCount = 0;
                        set = getFilterWindow3x3(index, width, height, DirectType.Clock);
                        for (int x = 1; x < set.Length; x++)
                            blackCount += tempData[set[x]];
                        if (blackCount < 2 || blackCount > 6) continue;
                        // 条件3 当前点8邻接0-1跳转次数
                        Int32[] curList = new Int32[set.Length];
                        for (int x = 0; x < set.Length; x++)
                            curList[x] = tempData[set[x]];
                        Int32 sT = sumT(curList);
                        if (sT != 1) continue;
                        // 条件4 p1,p3,p7至少一个为0
                        if (tempData[set[1]] * tempData[set[3]] * tempData[set[7]] != 0) continue;
                        // 条件5 p1,p5,p7至少一个为0
                        if (tempData[set[1]] * tempData[set[5]] * tempData[set[7]] != 0) continue;
                        marked[index] = 1;
                        loop = true;
                    }
                }
                for (int i = 0; i < data.Length; i++)
                {
                    if (marked[i] == 1)
                    {
                        tempData[i] = 0;
                        marked[i] = 0;
                    }
                }
                #endregion
            }
            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (tempData[i].Equals(0))
                    result[i] = 255;
                else
                    result[i] = 0;
            }
        }

        /// <summary>
        ///  Zhang扩展细化
        ///  貌似还不如不扩展.. 有点问题
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">数据宽度</param>
        /// <param name="height">数据高度</param>
        protected void zhangExpandThinning(ref Byte[] data, Int32 width, Int32 height, out Byte[] result)
        {
            this.zhangThinning(ref data, width, height, out result);
            Int32[] tempData = new Int32[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Equals(0))
                    tempData[i] = 1;
            }

            Int32[] set;
            Queue<Int32> Dm = new Queue<int>();
            Int32[] medianData = (Int32[])tempData.Clone();
            #region 单像素细化
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    Int32 index = j * width + i;

                    // 条件1 当前点为1 初始化标记flag = 0;
                    if (medianData[index] == 0) continue;
                    Int32 flag = 0;
                    Queue<Int32> Sq = new Queue<int>();
                    set = getFilterWindow3x3(index, width, height, DirectType.Clock);
                    do
                    {
                        // 条件a 搜索当前点4个斜对角如果有1则压入种子队列
                        for (int x = 2; x < set.Length; x += 2)
                        {
                            if (medianData[set[x]] == 1)
                                Sq.Enqueue(set[x]);
                        }
                        if (Sq.Count > 0)
                        {
                            flag = 1;
                            medianData[index] = 0;
                        }

                        // 条件b 如果flag为1将当前点十字方向为1点划入Dm同时置反
                        if (flag == 1)
                        {
                            for (int x = 1; x < set.Length; x += 2)
                            {
                                if (medianData[set[x]] == 1)
                                {
                                    Dm.Enqueue(set[x]);
                                    medianData[set[x]] = (1 - medianData[set[x]]);
                                }
                            }
                        }
                        // 如果flag为0将当前点十字方向为1点划入种子队列
                        if (flag == 0)
                        {
                            for (int x = 1; x < set.Length; x += 2)
                            {
                                if (medianData[set[x]] == 1)
                                    Sq.Enqueue(set[x]);
                            }
                        }
                        // 条件c flag置为0 删除当前种子点, 若种子队列非空, 则转条件a 否则继续下移连通点
                        flag = 0;
                        if (Sq.Count > 0)
                        {
                            medianData[Sq.Dequeue()] = 0;
                        }
                    } while (Sq.Count > 0);
                }
            }
            while (Dm.Count > 0)
            {
                tempData[Dm.Dequeue()] = 0;
            }
            #endregion
            medianData = (Int32[])tempData.Clone();
            #region 端点连接
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    Int32 index = j * width + i;
                    // 条件1 当前点为1
                    if (medianData[index] == 0) continue;
                    // 条件2
                    // 条件a 当前点8邻域求和
                    Int32 sum = 0;
                    set = getFilterWindow3x3(index, width, height, DirectType.Clock);
                    for (int x = 1; x < set.Length; x++)
                        sum += medianData[set[x]];
                    if (sum != 2) continue;
                    // 如果值为2 可能为断裂点或端点
                    if (sum == 2)
                    {
                        Int32[] tempSet;
                        for (int x = 1; x < set.Length; x++)
                        {
                            if (medianData[set[x]] == 1)
                            {
                                tempSet = getFilterWindow3x3(set[x], width, height, DirectType.Clock);
                                for (int y = 0; y < tempSet.Length; y++)
                                    medianData[tempSet[y]] = 0;
                            }
                        }
                        Int32[] set5 = getFilterWindow(index, width, height, FilterWindowType.Rect5);
                        sum = 0;
                        for (int x = 1; x < set5.Length; x++)
                        {
                            sum += medianData[set5[x]];
                        }
                        // 当前点为端点
                        if (sum == 0)
                            continue;
                        // 当前点为断裂短
                        if (sum > 0)
                        {
                            Int32 count = 1, sumx = i, sumy = j;
                            for (int x = 1; x < set5.Length; x++)
                            {
                                if (medianData[set5[x]] == 0)
                                {
                                    count++;
                                    sumy += set5[x] / width;
                                    sumx += set5[x] % width;
                                }
                            }
                            tempData[(sumy / count) * width + (sumx / count)] = 1;
                        }
                    }
                }
            }
            #endregion
            for (int i = 0; i < result.Length; i++)
            {
                if (tempData[i] == 0)
                    result[i] = 255;
                else
                    result[i] = 0;
            }
        }
    }
}
