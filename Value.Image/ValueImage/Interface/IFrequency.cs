using System;
using System.Drawing;
using ValueImage.Infrastructure;
using MathHelper.Infrastructure;

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
        void Gabor(Bitmap srcImage, GaborParam gaborParam);

        /// <summary>
        ///  映射
        /// </summary>
        /// <param name="orientType">映射方向</param>
        Int32[] Projection(Bitmap srcImage, OrientationType orientType);

        /// <summary>
        ///  按一定角度映射
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="orientType">映射方向</param>
        Int32[] Projection(Bitmap srcImage, Double angle, OrientationType orientType);

        /// <summary>
        ///  图像密度区域(二值)
        ///  自创, 效率效果有待提高
        /// </summary>
        /// <param name="endureWidth">容忍白点宽度</param>
        Rectangle[] Density(Bitmap srcImage, Int32 endureWidth);
    }
}
