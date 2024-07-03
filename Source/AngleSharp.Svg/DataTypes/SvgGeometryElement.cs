using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Svg.DocumentStructure;
using AngleSharp.Svg.Rendering;
using AngleSharp.Svg.Rendering.Paths;

namespace AngleSharp.Svg.DataTypes
{
    public abstract class SvgGeometryElement : SvgGraphicsElement
    {

        public SvgGeometryElement(Document owner, string localName, string? prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, localName, prefix, flags)
        {
        }

        internal abstract SvgPathCommand BuildPath(IRenderDimensions dim);

        internal override void Draw(ISvgRenderer renderer)
        {
            renderer.Path(BuildPath(renderer.Dim), SvgFillRule.Nonzero, Fill, Stroke);
        }

    }
}
