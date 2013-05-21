using System;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  模型算子锐化类型
    /// </summary>
    public enum MaskType
    {
        Roberts,
        Prewitt,
        Sobel,
        Laplacian1,
        Laplacian2,
        Laplacian3,
        Kirsch
    }
}
