using System;
using System.Drawing;
using ValueImage.Infrastructure;
using ValueMathHelper.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图形频率变化接口
    /// </summary>
    public interface IFrequency
    {
        /// <summary>
        ///  快速傅里叶变化
        /// </summary>
        /// <param name="inv">是否进行坐标位移变换</param>
        /// <returns></returns>
        void FFT(Bitmap srcImage, Boolean inv);

        /// <summary>
        ///  幅度图像
        /// </summary>
        void Amplitude(Bitmap srcImage);

        /// <summary>
        ///  相位图像
        /// </summary>
        void Phase(Bitmap srcImage);

        /// <summary>
        ///  Gabor滤波
        /// </summary>
        /// <param name="srcImage"></param>
        void Gabor(Bitmap srcImage, Double sigma, Double theta, Double lambda, Double psi, Double gamma);

        /// <summary>
        ///  映射
        /// </summary>
        /// <param name="orientType">映射方向</param>
        Int32[] Projection(Bitmap srcImage, OrientationType orientType);
    }
}
