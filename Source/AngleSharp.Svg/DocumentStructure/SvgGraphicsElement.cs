using AngleSharp.Dom;
using AngleSharp.Svg.Rendering;

namespace AngleSharp.Svg.DocumentStructure
{
    public abstract class SvgGraphicsElement : SvgElement
    {

        public SvgGraphicsElement(Document owner, string localName, string? prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, localName, prefix, flags) { }

        internal abstract void Draw(ISvgRenderer renderer);

    }
}
