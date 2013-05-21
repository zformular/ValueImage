using System;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  缩放的算法选择
    /// </summary>
    public enum ZoomType
    {
        /// <summary>
        ///  最近邻插值法
        /// </summary>
        NearestNeighbor,
        /// <summary>
        ///  双线性插值法
        /// </summary>
        Amphilinearity
    }
}
