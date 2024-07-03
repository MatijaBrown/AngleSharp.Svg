using AngleSharp.Dom;

namespace AngleSharp.Svg.DocumentStructure
{
    public class SvgDescElement : SvgElement
    {

        public SvgDescElement(string content, Document owner, string? prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, TagNames.Desc, prefix, flags)
        {
            TextContent = content;
        }

    }
}
