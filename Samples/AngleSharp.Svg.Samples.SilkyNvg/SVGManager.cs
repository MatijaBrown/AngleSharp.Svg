using AngleSharp.Dom;
using AngleSharp.Svg.DocumentStructure;
using AngleSharp.Svg.Rendering;
using AngleSharp.Svg.Tools;
using AngleSharp.Xml.Dom;
using AngleSharp.Xml.Parser;
using SilkyNvg;
using SilkyNvg.Graphics;

namespace AngleSharp.Svg.Samples.SilkyNvg
{
    internal class SVGManager
    {

        private static RenderDimensions renderDimensions;
        private static SvgRenderer svgRenderer;

        private static SvgTreeRenderer svgTreeRenderer;

        private static SvgSvgElement? rootElement = null;

        public static void Init(Nvg nvg, RenderDimensions initialDimensions)
        {
            renderDimensions = initialDimensions;
            svgRenderer = new SvgRenderer(nvg, renderDimensions);
            LoadSvg("rectangles.svg");

            svgTreeRenderer = new SvgTreeRenderer(svgRenderer);
        }

        public static void LoadSvg(string name)
        {
            var parser = new XmlParser();
            IXmlDocument document = parser.ParseDocument(File.ReadAllText("./svgs/" + name));

            IElement root = document.DocumentElement;

            var renderDimensions = new RenderDimensions();

            rootElement = SvgLoader.Load(root, renderDimensions);
        }

        public static void UpdateDimensions(double newWidth, double newHeight)
        {
            renderDimensions.Update(newWidth, newHeight);
        }

        public static void Render(Nvg nvg)
        {
            if (rootElement == null)
            {
                return;
            }

            svgTreeRenderer.Render(rootElement);
        }

    }
}
