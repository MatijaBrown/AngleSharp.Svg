using AngleSharp.Css;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.DataTypes;
using AngleSharp.Svg.Rendering.Paths;

namespace AngleSharp.Svg.BasicShapes
{
    public class SvgRectElement : SvgGeometryElement
    {

        public ICssValue X { get; }

        public ICssValue Y { get; }

        public ICssValue Width { get; }

        public ICssValue Height { get; }

        public ICssValue RX { get; }

        public ICssValue RY { get; }

        public SvgRectElement(string? sx, string? sy, string? swidth, string? sheight, string? srx, string? sry, 
            Document owner, string? prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, "rect", prefix, flags)
        {
            X = UnitParser.ParseLength(new Text.StringSource(sx ?? "0px")) ?? Length.Zero;
            Y = UnitParser.ParseLength(new Text.StringSource(sy ?? "0px")) ?? Length.Zero;
            Width = UnitParser.ParseLength(new Text.StringSource(swidth ?? "0px")) ?? Length.Zero;
            Height = UnitParser.ParseLength(new Text.StringSource(sheight ?? "0px")) ?? Length.Zero;
            RX = UnitParser.ParseLength(new Text.StringSource(srx ?? "0px")) ?? Length.Zero;
            RY = UnitParser.ParseLength(new Text.StringSource(sry ?? "0px")) ?? Length.Zero;
        }

        internal override SvgPathCommand BuildPath(IRenderDimensions dim)
        {
            double x = X.AsPx(dim, RenderMode.Undefined);
            double y = Y.AsPx(dim, RenderMode.Undefined);
            double w = Width.AsPx(dim, RenderMode.Undefined);
            double h = Height.AsPx(dim, RenderMode.Undefined);
            double rx = RX.AsPx(dim, RenderMode.Undefined);
            double ry = RY.AsPx(dim, RenderMode.Undefined);

            var path = new SvgPathCommand(SvgPathCommandType.MoveTo, x, y);
            path.Append(new SvgPathCommand(SvgPathCommandType.LineTo, x + w, y));
            path.Append(new SvgPathCommand(SvgPathCommandType.LineTo, x + w, y + h));
            path.Append(new SvgPathCommand(SvgPathCommandType.LineTo, x, y + h));
            path.Append(new SvgPathCommand(SvgPathCommandType.LineTo, x, y));
            path.Append(new SvgPathCommand());

            return path;
        }

    }
}
