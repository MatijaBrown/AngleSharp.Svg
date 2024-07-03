using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.Rendering.Paths;

namespace AngleSharp.Svg.DocumentStructure
{
    public class SvgElement : Element
    {

        // a little hack for now because the Parse... methods are internal for some godforsaken reason.
        private readonly Dom.SvgElement _inner;

        internal bool IsOutermostSvgElement => (ParentElement == null) || (ParentElement.NamespaceUri != NamespaceNames.SvgUri);

        internal Color? Fill
        {
            get
            {
                Console.WriteLine(this.FillRule);
                string? s = GetAttribute("fill");
                if (s != null)
                {
                    return s == "none" ? null : Color.FromName(s);
                }
                else
                {
                    return Color.Black;
                }
            }
        }

        internal SvgFillRule FillRule
        {
            get
            {
                string? s = GetAttribute("fill");
                return s switch
                {
                    "nonzero" => SvgFillRule.Nonzero,
                    "evenodd" => SvgFillRule.Evenodd,
                    _ => SvgFillRule.Nonzero
                };
            }
        }

        internal Color? Stroke
        {
            get
            {
                string? s = GetAttribute("stroke");
                if (s != null)
                {
                    return s == "none" ? null : Color.FromName(s);
                }
                else
                {
                    return null;
                }
            }
        }

        internal Length StrokeWidth
        {
            get
            {
                string? s = GetAttribute("stroke-width");
                if (s != null)
                {
                    return Length.TryParse(s, out Length r) ? r : Length.Zero;
                }
                return Length.Normal;
            }
        }

        public SvgElement(Document owner, string localName, string? prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, localName, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember)
        {
            _inner = new Dom.SvgElement(owner, localName, prefix, flags);
        }

        public SvgSvgElement? OwnerSvgElement
        {
            get
            {
                IElement? owner = ParentElement;
                while ((owner is not SvgSvgElement) && (owner is not null))
                {
                    if ((owner.ParentElement == null) || (owner.ParentElement.NamespaceUri == NamespaceNames.SvgUri))
                    {
                        owner = owner.ParentElement;
                    }
                    else
                    {
                        owner = null;
                        break;
                    }
                }
                if ((owner is SvgSvgElement) || (owner is null))
                {
                    return (SvgSvgElement?)owner;
                }
                throw new Exception();
            }
        }

        public override IElement ParseSubtree(string source) => _inner.ParseSubtree(source);

    }
}
