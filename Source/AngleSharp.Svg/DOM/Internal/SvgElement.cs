using AngleSharp.Dom;

namespace AngleSharp.Svg.DOM.Internal
{
    public abstract class SvgElement : Element, ISvgElement
    {

        protected SvgElement(Document owner, string localName, string? prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, localName, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember) { }

    }
}
