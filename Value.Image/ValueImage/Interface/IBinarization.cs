using System;
using System.Drawing;
using ValueImage.Infrastructure;
namespace ValueImage.Interface
{
    /// <summary>
    ///  对图像的二值处理接口
    /// </summary>
    public interface IBinarization
    {
        /// <summary>
        ///  映射
        ///  投影取值未平均
        /// </summary>
        /// <param name="orientType">映射方向</param>
        Int32[] Projection(Bitmap srcImage, OrientationType orientType);

        /// <summary>
        ///  按一定角度映射
        ///  投影取值未平均
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="orientType">映射方向</param>
        Int32[] Projection(Bitmap srcImage, Double angle, OrientationType orientType);

        /// <summary>
        ///  Roborts算子边缘锐化
        /// </summary>
        void RobortsEdge(Bitmap srcImage, Int32 threshold);

        /// <summary>
        ///  kFill滤波器
        /// </summary>
        void kFillFilter(Bitmap srcImage);

        /// <summary>
        ///  切割矩形
        /// </summary>
        /// <param name="startRow">起始行索引</param>
        /// <param name="startCol">起始列索引</param>
        /// <param name="endRow">终止行索引</param>
        /// <param name="endCol">终止列索引</param>
        Bitmap CutRectangle(Bitmap srcImage, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol);

        /// <summary>
        ///  双线性插值缩放
        /// </summary>
        /// <param name="width">缩放后宽度</param>
        /// <param name="height">缩放后高度</param>
        Bitmap BileanerZoom(Bitmap srcImage, Int32 width, Int32 height);

        /// <summary>
        ///  双线性插值法
        /// </summary>
        /// <param name="angle">旋转的角度</param>
        /// <returns></returns>
        Bitmap BileanerRotate(Bitmap srcImage, Double angle);

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
        ///  获得图片的偏移角度
        /// </summary>
        /// <returns>偏移的角度</returns>
        Int32 OffsetAngle(Bitmap srcImage);

        /// <summary>
        ///  将图片数据转化为字节型数组
        /// </summary>
        /// <param name="data">转化为字节型的数组</param>
        void ConvertToBytes(Bitmap srcImage, out Byte[,] data);

        /// <summary>
        ///  将矩形内的图像转化会字节型数组
        /// </summary>
        Byte[,] ConvertToBytes(Bitmap srcImage, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol);

        /// <summary>
        ///  剔除指定矩形大小噪声
        /// </summary>
        /// <param name="level">操作最大大小</param>
        void NosieKiller(Bitmap srcImage, FilterLevelType level);

        /// <summary>
        ///  填充断点
        /// </summary>
        /// <param name="level">填充的等级</param>
        void FillBreakpoint(Bitmap srcImage, FilterLevelType level);

        /// <summary>
        ///  倾斜检测
        /// </summary>
        /// <param name="type">检测类型</param>
        Double ShewDetection(Bitmap srcImage, ShewDetectionType type);

        /// <summary>
        ///  倾斜矫正
        /// </summary>
        /// <param name="type">倾斜检测的方法</param>
        Bitmap ShewCorrection(Bitmap srcImage, ShewDetectionType type);

        /// <summary>
        ///  中值滤波
        /// </summary>
        void MedianFilter(Bitmap srcImage, TemplateType type);
    }
}
