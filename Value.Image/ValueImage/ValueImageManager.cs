using System;
using ValueImage.Interface;
using System.Drawing.Imaging;
using ValueImage.ImageFactory.Bit24;
using System.Diagnostics;

namespace ValueImage
{
    public class ValueImageManager
    {
        public static IValueImage GetValueImage(PixelFormat format)
        {
            ValueMathHelper.ValueMath helper = ValueMathHelper.ValueMath.GetInstance();

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
