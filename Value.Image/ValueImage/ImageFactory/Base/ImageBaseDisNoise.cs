using System;
using System.Linq;
using ValueImage.Infrastructure;
using System.Diagnostics;
using System.Collections.Generic;

namespace ValueImage.ImageFactory.Base
{
    abstract partial class ImageBase
    {
        /// <summary>
        ///  腐蚀
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="type">腐蚀模板的类型</param>
        protected void erode(ref Byte[] data, Int32 width, Int32 height, FilterWindowType type, out Byte[] result)
        {
            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = 255;
            }

            Int32[] set;
            Boolean hasZero;
            for (int i = 0; i < data.Length; i++)
            {
                hasZero = false;
                set = getFilterWindow(i, width, height, type);
                for (int j = 0; j < set.Length; j++)
                {
                    if (data[set[j]] == 0)
                    {
                        hasZero = true;
                        break;
                    }
                }
                if (!hasZero)
                    result[i] = 0;
            }
        }

        /// <summary>
        ///  膨胀
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="type">膨胀模板的类型</param>
        protected void delation(ref Byte[] data, Int32 width, Int32 height, FilterWindowType type, out Byte[] result)
        {
            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = 255;
            }

            Int32[] set;
            Boolean hasZero;
            for (int i = 0; i < data.Length; i++)
            {
                hasZero = false;
                set = getFilterWindow(i, width, height, type);
                for (int j = 0; j < set.Length; j++)
                {
                    if (data[set[j]] == 0)
                    {
                        hasZero = true;
                        break;
                    }
                }
                if (hasZero)
                    result[i] = 0;
            }
        }

        /// <summary>
        ///  灰度形态膨胀
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="template">模板赋值</param>
        protected void grayDelation(ref Byte[] data, Int32 width, Int32 height, Byte[] template, out Byte[] result)
        {
            Debug.Assert(template.Where(x => x == 1 || x == 0).Count() == template.Length, "模板只能为0或1");
            if (template.Where(x => x == 1 || x == 0).Count() != template.Length)
            {
                result = new Byte[0];
                return;
            }

            Int32[] set = null;
            List<Int32> list = new List<int>();
            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (template.Length == 9)
                    set = getFilterWindow(i, width, height, TemplateType.T3x3);
                else if (template.Length == 25)
                    set = getFilterWindow(i, width, height, TemplateType.T5x5);
                else if (template.Length == 49)
                    set = getFilterWindow(i, width, height, TemplateType.T7x7);

                for (int j = 0; j < set.Length; j++)
                {
                    list.Add(data[set[j]] * template[j]);
                }
                Int32 max = MathHelper.ValueMath.Max(list.ToArray());
                result[i] = Convert.ToByte(max);
                list.Clear();
            }
        }

        /// <summary>
        ///  灰度形态腐蚀
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="template">模板赋值</param>
        protected void grayErode(ref Byte[] data, Int32 width, Int32 height, Byte[] template, out Byte[] result)
        {
            Debug.Assert(template.Where(x => x == 1 || x == 255).Count() == template.Length, "模板只能为255或1");
            if (template.Where(x => x == 1 || x == 255).Count() != template.Length)
            {
                result = new Byte[0];
                return;
            }

            Int32[] set = null;
            List<Int32> list = new List<int>();
            result = new Byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (template.Length == 9)
                    set = getFilterWindow(i, width, height, TemplateType.T3x3);
                else if (template.Length == 25)
                    set = getFilterWindow(i, width, height, TemplateType.T5x5);
                else if (template.Length == 49)
                    set = getFilterWindow(i, width, height, TemplateType.T7x7);

                for (int j = 0; j < set.Length; j++)
                {
                    list.Add(data[set[j]] * template[j]);
                }
                Int32 min = MathHelper.ValueMath.Min(list.ToArray());
                result[i] = Convert.ToByte(min);
                list.Clear();
            }
        }

        /// <summary>
        ///  kFill滤波器(不完全)
        /// </summary>
        /// <param name="data">二维数据</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        protected void kfillFilter(ref Byte[] data, Int32 width, Int32 height)
        {
            Int32[] set = null;
            Boolean loop = true;
            do
            {
                loop = false;
                #region 填充黑点

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Int32 index = j * width + i;
                        if (data[index] == 0)
                        {
                            set = getFilterWindow3x3(index, width, height, DirectType.Clock);

                            Int32 n = 0;
                            for (int x = 1; x < set.Length; x++)
                            {
                                if (data[set[x]] == 255) n++;
                            }
                            Int32 r = 0;
                            for (int x = 2; x < set.Length; x += 2)
                            {
                                if (data[set[x]] == 255) r++;
                            }
                            if (n > (6) || ((n == 6) && r == 2))
                            {
                                data[index] = 255;
                                loop = true;
                            }
                        }
                    }
                }

                #endregion

                #region 填充白点

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Int32 index = j * width + i;
                        if (data[index] == 255)
                        {
                            set = getFilterWindow3x3(index, width, height, DirectType.Clock);
                            Int32 n = 0;
                            for (int x = 1; x < set.Length; x++)
                            {
                                if (data[set[x]] == 0)
                                    n++;
                            }
                            Int32 r = 0;
                            for (int x = 2; x < set.Length; x += 2)
                            {
                                if (data[set[x]] == 0)
                                    r++;
                            }
                            if (n > (6) || ((n == 6) && r == 2))
                            {
                                data[index] = 0;
                                loop = true;
                            }
                        }
                    }
                }

                #endregion
            } while (loop);
        }
    }
}
