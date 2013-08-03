using System;
using System.Drawing;
using ValueImage.Infrastructure;

namespace ValueImage.Interface
{
    /// <summary>
    ///  ValueImage实现接口
    /// </summary>
    public interface IValueImage : IGray, IFrequency, IDisNoise, IFilter, IEdge, INoise, IDivision, IGeometry, IBinarization, IOther { }
}
