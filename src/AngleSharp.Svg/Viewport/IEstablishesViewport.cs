using AngleSharp.Css;
using AngleSharp.Svg.Accessors.Viewport;

namespace AngleSharp.Svg.Viewport
{
    internal interface IEstablishesViewport
    {

        double Width { get; }

        double Height { get; }

        double FontSize { get; }

        IRenderDimensions RenderDimensions => new SvgRenderDimensions(Width, Height, FontSize);

    }
}
