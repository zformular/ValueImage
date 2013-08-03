using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图形噪声接口
    /// </summary>
    public interface INoise
    {
        /// <summary>
        ///  噪声
        /// </summary>
        void Noise(Bitmap srcImage, NoiseType type);

        /// <summary>
        ///  高斯噪声
        /// </summary>
        /// <param name="mean">均值</param>
        /// <param name="meanDeviation">均方差</param>
        void GaussNoise(Bitmap srcImage, Double mean, Double meanDeviation);

        /// <summary>
        ///  瑞利噪声
        /// </summary>
        /// <param name="paramA">参数A</param>
        /// <param name="paramB">参数B</param>
        void RayleighNoise(Bitmap srcImage, Double paramA, Double paramB);

        /// <summary>
        ///  指数噪声
        /// </summary>
        /// <param name="param">参数a(a>0)</param>
        void IndexNoise(Bitmap srcImage, Double param);
    }
}
