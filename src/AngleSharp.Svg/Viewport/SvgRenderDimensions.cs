using AngleSharp.Css;

namespace AngleSharp.Svg.Viewport
{
    internal readonly struct SvgRenderDimensions(double width, double height, double fontSize) : IRenderDimensions
    {

        public double RenderWidth => width;

        public double RenderHeight => height;

        public double FontSize => fontSize;

    }
}
