using System;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  滤波窗口格子数
    /// </summary>
    public enum FilterLevelType
    {
        /// <summary>
        ///  1x1点
        /// </summary>
        Level01 = 0x01,
        /// <summary>
        ///  2x2区域
        /// </summary>
        Level02 = 0x02,
        /// <summary>
        ///  3x3区域
        /// </summary>
        Level03 = 0x03,
        /// <summary>
        ///  4x4区域
        /// </summary>
        Level04 = 0x04,
        /// <summary>
        ///  5x5区域
        /// </summary>
        Level05 = 0x05,
        /// <summary>
        ///  6x6区域
        /// </summary>
        Level06 = 0x06,
        /// <summary>
        ///  7x7区域
        /// </summary>
        Level07 = 0x07,
        /// <summary>
        ///  8x8区域
        /// </summary>
        Level08 = 0x08,
        /// <summary>
        ///  9x9区域
        /// </summary>
        Level09 = 0x09,
        /// <summary>
        ///  10x10区域
        /// </summary>
        Level10 = 0x0A,
        /// <summary>
        ///  15x15
        /// </summary>
        Level15 = 0x0E,
        /// <summary>
        ///  16x16
        /// </summary>
        Level16 = 0x0F
    }
}
