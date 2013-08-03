using System;
using System.Drawing;
using ValueImage.Infrastructure;

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

        /// <summary>
        ///  模糊噪声监测
        /// </summary>
        Point[] BlurryNoiseDetect(Bitmap srcImage);

        /// <summary>
        ///  将图片数据转化为字节型数组
        /// </summary>
        /// <param name="data">转化为字节型的数组</param>
        void ConvertToBytes(Bitmap srcImage, out Byte[,] data);

        /// <summary>
        ///  将图片数据转化为字节型数组
        /// </summary>
        /// <param name="datab">字节型数组B分量</param>
        /// <param name="datar">字节型数组R分量</param>
        /// <param name="datag">字节型数组G分量</param>
        void ConvertToBytes(Bitmap srcImage, out Byte[,] datab, out Byte[,] datar, out Byte[,] datag);

        /// <summary>
        ///  获得图片的偏移角度
        /// </summary>
        /// <returns>偏移的角度</returns>
        Int32 OffsetAngle(Bitmap srcImage);
    }
}
