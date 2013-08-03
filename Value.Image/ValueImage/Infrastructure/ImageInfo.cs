using System;
using System.Drawing;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  图像信息类
    /// </summary>
    public class ImageInfo
    {
        public Bitmap OrgImage { get; set; }

        /// <summary>
        ///  图像的位置
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        ///  图像的大小
        /// </summary>
        public Size Size { get; set; }
    }
}
