using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Svg.Rendering;
using AngleSharp.Svg.Rendering.Paths;
using SilkyNvg;
using SilkyNvg.Graphics;
using SilkyNvg.Paths;

namespace AngleSharp.Svg.Samples.SilkyNvg
{
    internal class SvgRenderer : ISvgRenderer
    {

        private readonly Nvg _nvg;

        public IRenderDimensions Dim { get; }

        public SvgRenderer(Nvg nvg, IRenderDimensions dim)
        {
            _nvg = nvg;
            Dim = dim;
        }

        public void EnterIsolatedGroup()
        {
            throw new NotImplementedException();
        }

        public void ExitAndRenderCurrentIsolatedGroup()
        {
            throw new NotImplementedException();
        }

        private void NvgStrokePaint(Color? svgPaint)
        {
            if (svgPaint == null)
            {
                _nvg.StrokeColour(Colour.Black);
            }
            else
            {
                Color c = svgPaint.Value;
                _nvg.StrokeColour(new Colour(c.R, c.G, c.B));
            }
        }

        private void NvgFillPaint(Color? svgPaint)
        {
            if (svgPaint == null)
            {
                _nvg.FillColour(Colour.Black);
            }
            else
            {
                Color c = svgPaint.Value;
                _nvg.FillColour(new Colour(c.R, c.G, c.B));
            }
        }

        public void Path(SvgPathCommand c, SvgFillRule fillRule, Color? fillPaint, Color? strokePaint)
        {
            _nvg.BeginPath();
            while (c.Next != null)
            {
                switch (c.Type)
                {
                    case SvgPathCommandType.MoveTo:
                        _nvg.MoveTo((float)c.X, (float)c.Y);
                        break;
                    case SvgPathCommandType.LineTo:
                        _nvg.LineTo((float)c.X, (float)c.Y);
                        break;
                    case SvgPathCommandType.Close:
                        _nvg.ClosePath();
                        break;
                }
                c = c.Next;
            }
            if ((fillPaint != null) || (strokePaint == null && fillPaint == null))
            {
                NvgFillPaint(fillPaint);
                _nvg.Fill();
            }
            if (strokePaint != null)
            {
                NvgStrokePaint(strokePaint);
                _nvg.Stroke();
            }
        }
    }
}
