using AngleSharp.Dom;

namespace AngleSharp.Svg.DOM
{
    public interface ISvgElement : IElement
    {

        ISvgElement? ViewportElement { get; }

    }
}
