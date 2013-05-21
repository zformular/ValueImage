﻿using System;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  灰度转化的类型
    /// </summary>
    public enum GrayscaleType
    {
        /// <summary>
        ///  最大灰度化
        /// </summary>
        Maximum,
        /// <summary>
        ///  最小灰度化
        /// </summary>
        Minimal,
        /// <summary>
        ///  中值灰度化
        /// </summary>
        Average
    }
}
