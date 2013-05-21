using System;
using System.Drawing;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图像其它接口
    /// </summary>
    public interface IOther
    {
        /// <summary>
        ///  反色
        /// </summary>
        void InvertColor(Bitmap srcImage);


        /// <summary>
        ///  直方图均衡化
        /// </summary>
        void HistEqualization(Bitmap srcImage);

        /// <summary>
        ///  直方图匹配
        /// </summary>
        /// <param name="histogram">要匹配的直方图模板</param>
        void HistMatch(Bitmap srcImage, Int32[] histogram);
    }
}
