using AngleSharp.Dom;
using AngleSharp.DomGeometry;
using AngleSharp.Svg.Rendering;

namespace AngleSharp.Svg.DocumentStructure
{
    public class SvgSvgElement : SvgGraphicsElement
    {

        private readonly DomPointReadonly _translate;

        private double _width;
        private double _height;

        public SvgSvgElement(Document owner, string? prefix = null, NodeFlags flags = NodeFlags.None,
            double x = 0, double y = 0, double width = 0, double height = 0)
            : base(owner, TagNames.Svg, prefix, flags)
        {
            _translate = IsOutermostSvgElement ? new DomPoint(x, y) : new DomPointReadonly(x, y);
            _width = width;
            _height = height;
        }

        internal DomMatrixReadOnly Transform => new(_width, 0, 0, _height, _translate.X, _translate.Y);

        public double CurrentScale
        {
            get => Transform.A;
            set
            {
                if (!IsOutermostSvgElement)
                {
                    return;
                }
                _width = _height = value;
            }
        }

        public DomPointReadonly CurrentTranslate => _translate;

        internal override void Draw(ISvgRenderer renderer)
        {
            foreach (var child in Children)
            {
                if (child is SvgGraphicsElement graphicsElement)
                {
                    graphicsElement.Draw(renderer);
                }
            }
        }

    }
}
