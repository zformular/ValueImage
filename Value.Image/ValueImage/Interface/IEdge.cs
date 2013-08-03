using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图形边缘锐化接口
    /// </summary>
    public interface IEdge
    {
        /// <summary>
        ///  模板算子计算
        /// </summary>
        /// <param name="thresholding">阈值</param>
        void MaskEgde(Bitmap srcImage, MaskType type, Int32 thresholding);

        /// <summary>
        ///  Roberts算子边缘锐化
        /// </summary>
        /// <param name="thresholding">阈值</param>
        void RobertsEgde(Bitmap srcImage, Int32 thresholding);

        /// <summary>
        ///  Prewitt算子锐化
        /// </summary>
        /// <param name="thresholding">阈值(为零的话不进行二值化)</param>
        void PrewittEgde(Bitmap srcImage, Int32 thresholding);

        /// <summary>
        ///  Sobel算子锐化
        /// </summary>
        /// <param name="thresholding">阈值(为零的话不进行二值化)</param>
        void SobelEgde(Bitmap srcImage, Int32 thresholding);

        /// <summary>
        ///  拉普拉斯算子
        /// </summary>
        /// <param name="thresholding">阈值(为零的话不进行二值化)</param>
        /// <param name="number">拉普拉斯算子序号</param>
        void LaplacianEgde(Bitmap srcImage, Int32 thresholding, Int32 number);

        /// <summary>
        ///  Kirsch算子锐化
        /// </summary>
        /// <param name="thresholding">阈值(为零的话不进行二值化)</param>
        void KirschEgde(Bitmap srcImage, Int32 thresholding);

        /// <summary>
        ///  高斯算子锐化
        /// </summary>
        /// <param name="type">高斯过滤类型</param>
        /// <param name="sigma">方差</param>
        /// <param name="thresholding">阈值</param>
        void GaussEgde(Bitmap srcImage, GaussFilterType type, Double sigma, Double thresholding);

        /// <summary>
        ///  Canny算子
        /// </summary>
        /// <param name="sigma">均方差</param>
        /// <param name="thresholding">阈值</param>
        void CannyEgde(Bitmap srcImage, Double sigma, Byte[] thresholding);
    }
}
