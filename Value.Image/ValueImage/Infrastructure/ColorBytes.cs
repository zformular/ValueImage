using System;
using System.Drawing;

namespace ValueImage.Infrastructure
{
    public class ColorBytes
    {
        public ColorBytes() { }
        public ColorBytes(params Byte[] bytes)
        {
            this.colorComponent = bytes;
        }

        /// <summary>
        ///  颜色分量
        /// </summary>
        private Byte[] colorComponent;

        /// <summary>
        ///  颜色分量,用内存法时的顺序
        /// </summary>
        public Byte[] ColorComponent
        {
            get
            {
                return colorComponent;
            }
            set
            {
                colorComponent = value;
            }
        }

        public Byte this[Int32 index]
        {
            get
            {
                if (index < this.colorComponent.Length)
                    return this.colorComponent[index];
                return 0;
            }
        }
    }
}
