using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图形过滤接口
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        ///  成分滤波
        /// </summary>
        void ComponentFilter(Bitmap srcImage, RateFilterType type);

        /// <summary>
        ///  低通滤波
        /// </summary>
        /// <param name="radius">滤波边界(大于该边界都不可通过)</param>
        void LowpassFilter(Bitmap srcImage, Double radius);

        /// <summary>
        ///  高通滤波
        /// </summary>
        /// <param name="radius">滤波边界(小于该边界都不可通过)</param>
        void HighpassFilter(Bitmap srcImage, Double radius);

        /// <summary>
        ///  带阻滤波
        /// </summary>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        void BandstopFilter(Bitmap srcImage, Double innerRadius, Double outerRadius);

        /// <summary>
        ///  带通滤波
        /// </summary>
        /// <param name="innerRadius">滤波圆周内边界</param>
        /// <param name="outerRadius">滤波圆周外边界</param>
        void BandpassFilter(Bitmap srcImage, Double innerRadius, Double outerRadius);

        /// <summary>
        ///  方位滤波
        /// </summary>
        /// <param name="startOrient">起始方位</param>
        /// <param name="endOrient">终止方位</param>
        void OrientationFilter(Bitmap srcImage, Int32 startOrient, Int32 endOrient);

        /// <summary>
        ///  均值滤波
        /// </summary>
        /// <param name="type">模板大小类型</param>
        void MeanFilter(Bitmap srcImage, TemplateType type);

        /// <summary>
        ///  中值滤波
        /// </summary>
        /// <param name="type">模板大小类型</param>
        void MedianFilter(Bitmap srcImage, TemplateType type);

        /// <summary>
        ///  二维小波变化
        /// </summary>
        /// <param name="lowpassType">低通滤波类型</param>
        /// <param name="hardThreshold">是否为硬阀值</param>
        /// <param name="thresholding">阀值</param>
        /// <param name="series">关于小波变换级数的参数</param>
        void Wavelet(Bitmap srcImage, WaveletLowpassType lowpassType, Boolean hardThreshold, Byte thresholding, Int32 series);

        /// <summary>
        ///  高斯滤波
        /// </summary>
        /// <param name="sigma">方差</param>
        void GaussFilter(Bitmap srcImage, Double sigma);

        /// <summary>
        ///  统计方法滤波
        /// </summary>
        /// <param name="thresholding">阀值</param>
        void StatisticFilter(Bitmap srcImage, TemplateType type, Double thresholding);
    }
}
