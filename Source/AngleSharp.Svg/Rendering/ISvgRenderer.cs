using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Svg.Rendering.Paths;

namespace AngleSharp.Svg.Rendering
{
    public interface ISvgRenderer
    {

        IRenderDimensions Dim { get; }

        void EnterIsolatedGroup();

        void ExitAndRenderCurrentIsolatedGroup();

        void Path(SvgPathCommand pathCommands, SvgFillRule fillRule, Color? fillPaint, Color? strokePaint);

    }
}
