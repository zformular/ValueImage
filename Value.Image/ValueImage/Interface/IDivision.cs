using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图形分割接口
    /// </summary>
    public interface IDivision
    {
        /// <summary>
        ///  切割矩形
        /// </summary>
        /// <param name="startRow">起始行索引</param>
        /// <param name="startCol">起始列索引</param>
        /// <param name="endRow">终止行索引</param>
        /// <param name="endCol">终止列索引</param>
        Bitmap CutRectangle(Bitmap srcImage, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol);

        /// <summary>
        ///  均匀量化
        /// </summary>
        /// <param name="prototypeColor">原型色</param>
        void UniformQuantization(Bitmap srcImage, ColorBytes[] prototypeColor);

        /// <summary>
        ///  Hilditch细化
        /// </summary>
        void HilditchThinning(Bitmap srcImage);

        /// <summary>
        ///  Zhang细化
        /// </summary>
        /// <param name="srcImage"></param>
        void ZhangThinning(Bitmap srcImage);

        /// <summary>
        ///  Zhang扩展细化
        /// </summary>
        /// <param name="srcImage"></param>
        void ZhangExpandThinning(Bitmap srcImage);
    }
}
