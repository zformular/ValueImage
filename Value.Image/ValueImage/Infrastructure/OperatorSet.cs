using System;

namespace ValueImage.Infrastructure
{
    /// <summary>
    ///  算子集合
    /// </summary>
    class OperatorSet
    {
        #region 拉普拉斯算子

        public static Int32[] laplacianOperator1 = new Int32[] { 0, 1, 0, 1, -4, 1, 0, 1, 0 };
        public static Int32[] laplacianOperator2 = new Int32[] { 1, 1, 1, 1, -8, 1, 1, 1, 1 };
        public static Int32[] laplacianOperator3 = new Int32[] { -1, 2, -1, 2, -4, 2, -1, 2, -1 };

        #endregion

        #region Prewitt算子

        public static Int32[] prewittOperatorX = new Int32[] { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        public static Int32[] prewittOperatorY = new Int32[] { 1, 1, 1, 0, 0, 0, -1, -1, -1 };

        #endregion


        #region Sobel算子

        public static Int32[] sobelOperatorX = new Int32[] { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
        public static Int32[] sobelOperatorY = new Int32[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };

        #endregion

        #region Roberts算子

        public static Int32[] robertOperatorX = { 0, 1, -1, 0 };
        public static Int32[] robertOperatorY = { 1, 0, 0, -1 };

        #endregion

        #region Kirsch算子

        public static Int32[] kirschOperator1 = new Int32[] { 3, 3, 3, 3, 0, 3, -5, -5, -5 };
        public static Int32[] kirschOperator2 = new Int32[] { 3, 3, 3, -5, 0, 3, -5, -5, 3 };
        public static Int32[] kirschOperator3 = new Int32[] { -5, 3, 3, -5, 0, 3, -5, 3, 3 };
        public static Int32[] kirschOperator4 = new Int32[] { 3, 3, 3, 3, 0, -5, 3, -5, -5 };
        public static Int32[] kirschOperator5 = new Int32[] { 3, 3, -5, 3, 0, -5, 3, 3, -5 };
        public static Int32[] kirschOperator6 = new Int32[] { 3, -5, -5, 3, 0, -5, 3, 3, 3 };

        #endregion
    }
}
