using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  图像集合变换接口
    /// </summary>
    public interface IGeometry
    {
        /// <summary>
        ///  填充矩形
        /// </summary>
        /// <param name="startRow">起始行索引</param>
        /// <param name="startCol">起始列索引</param>
        /// <param name="endRow">终止行索引</param>
        /// <param name="endCol">终止列索引</param>
        /// <param name="color">颜色</param>
        void FillRectangle(Bitmap srcImage, Int32 startRow, Int32 startCol, Int32 endRow, Int32 endCol, Color color);

        /// <summary>
        ///  对图像已指定像素按 kx+b 线性变换
        /// </summary>
        /// <param name="slope">斜率</param>
        /// <param name="displacements">平移</param>
        void LinearChange(Bitmap srcImage, float slope, float displacements);

        /// <summary>
        ///  平移
        /// </summary>
        /// <param name="x">横向位移</param>
        /// <param name="y">纵向位移</param>
        void Move(Bitmap srcImage, Int32 x, Int32 y);

        /// <summary>
        ///  水平镜像
        /// </summary>
        void HoriMirror(Bitmap srcImage);

        /// <summary>
        ///  垂直镜像
        /// </summary>
        void VertMirror(Bitmap srcImage);

        /// <summary>
        ///  图像缩放
        /// </summary>
        /// <param name="type">缩放算法</param>
        void Zoom(Bitmap srcImage, Double zoomingX, Double zoomingY, ZoomType type);

        /// <summary>
        ///  双线性插值缩放
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="zoomingX"></param>
        /// <param name="zoomingY"></param>
        /// <returns></returns>
        Bitmap BileanerZoom(Bitmap srcImage, Int32 width, Int32 height);

        /// <summary>
        ///  旋转
        /// </summary>
        /// <param name="degree">角度</param>
        void Rotate(Bitmap srcImage, Int32 degree);

        /// <summary>
        ///  双线性插值法
        /// </summary>
        Bitmap BileanerRotate(Bitmap srcImage, Double angle);

        /// <summary>
        ///  双线性插值法
        /// </summary>
        /// <param name="backColor">背景颜色</param>
        Bitmap BileanerRotate(Bitmap srcImage, Double angle, Color backColor);

        /// <summary>
        ///  拼接图片
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        Bitmap SpliceImage(ImageInfo[] infos);
    }
}
