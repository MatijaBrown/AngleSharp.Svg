using AngleSharp.Svg.DocumentStructure;

namespace AngleSharp.Svg.Rendering
{
    public class SvgTreeRenderer
    {

        private readonly ISvgRenderer _renderer;

        public SvgTreeRenderer(ISvgRenderer renderer)
        {
            _renderer = renderer;
        }

        public void Render(SvgSvgElement root)
        {
            root.Draw(_renderer);
        }

    }
}
