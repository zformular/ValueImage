using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    public interface IGray
    {
        /// <summary>
        ///  图形灰度化接口
        /// </summary>
        /// <param name="type">灰度化类型</param>
        void ConvertToGrayscale(Bitmap srcImage, GrayscaleType type);

        /// <summary>
        ///  加权灰度化
        /// </summary>
        /// <param name="weightR">像素R的权重</param>
        /// <param name="weightG">像素G的权重</param>
        /// <param name="weightB">像素B的权重</param>
        void ConvertToGrayscale(Bitmap srcImage, float weightR, float weightG, float weightB);

        /// <summary>
        ///  灰度拉伸
        /// </summary>
        void GrayscaleStretch(Bitmap srcImage);

        /// <summary>
        ///  灰度拉伸
        /// </summary>
        /// <param name="x1">拐点1横坐标</param>
        /// <param name="y1">拐点1纵坐标</param>
        /// <param name="x2">拐点2横坐标</param>
        /// <param name="y2">拐点2纵坐标</param>
        void GrayscaleStretch(Bitmap srcImage, Int32 x1, Int32 y1, Int32 x2, Int32 y2);

        /// <summary>
        ///  二值化
        /// </summary>
        /// <param name="throsholding">阈值</param>
        void Binarization(Bitmap srcImage, Int32 throsholding);

        /// <summary>
        ///  最优阈值化
        /// </summary>
        void OptimalThreshold(Bitmap srcImage);

        /// <summary>
        ///  Ostu阈值化
        /// </summary>
        /// <param name="srcImage"></param>
        void OstuThreshold(Bitmap srcImage);


    }
}
