using System;
using ValueImage.Interface;
using System.Drawing.Imaging;
using ValueImage.ImageFactory.Bit24;
using System.Diagnostics;

namespace ValueImage
{
    /// <summary>
    ///  ValueImage管理者
    /// </summary>
    public class ValueImageManager
    {
        /// <summary>
        ///  获得ValueImage实例
        /// </summary>
        /// <param name="format">图像像素类型</param>
        public static IValueImage GetValueImage(PixelFormat format)
        {
            MathHelper.ValueMath helper = MathHelper.ValueMath.GetInstance();

            switch (format)
            {
                case PixelFormat.Format24bppRgb:
                    return ImageBit24.Instance;
                default:
                    Debug.Fail(String.Concat("暂时不支持", format, "格式位图的图像处理"));
                    break;
            }

            return null;
        }
    }
}
