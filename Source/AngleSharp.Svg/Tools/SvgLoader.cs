using AngleSharp.Css;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Dom;
using AngleSharp.Svg.BasicShapes;
using AngleSharp.Svg.DocumentStructure;

namespace AngleSharp.Svg.Tools
{
    public sealed class SvgLoader
    {

        private static T CopyAttributes<T>(IElement mirror, T destinationElement)
            where T : Element
        {
            foreach (Node node in mirror.ChildNodes)
            {
                if (node.NodeType == NodeType.Element)
                {
                    continue;
                }
                destinationElement.AddNode(node);
            }
            foreach (Attr attr in mirror.Attributes)
            {
                destinationElement.AddAttribute(attr);
            }
            return destinationElement;
        }

        private static SvgElement Load(IElement domElement, SvgElement thisElement, IRenderDimensions renderDimension)
        {
            foreach (IElement child in domElement.Children)
            {
                if (child.NamespaceUri is null || child.NamespaceUri != NamespaceNames.SvgUri)
                {
                    continue;
                }
                switch (child.LocalName)
                {
                    case "svg":
                        thisElement.AddNode(LoadSvgElement(child, renderDimension));
                        break;
                    case "desc":
                        thisElement.AddNode(LoadDescElement(child));
                        break;
                    case "rect":
                        thisElement.AddNode(LoadRectElement(child));
                        break;
                }
            }
            return thisElement;
        }

        private static SvgRectElement LoadRectElement(IElement domElement)
        {
            return CopyAttributes(domElement, new SvgRectElement(domElement.GetAttribute("x"), domElement.GetAttribute("y"), domElement.GetAttribute("width"), domElement.GetAttribute("height"),
                domElement.GetAttribute("rx"), domElement.GetAttribute("ry"), (Document)domElement.Owner!, domElement.Prefix, domElement.Flags));
        }

        private static SvgDescElement LoadDescElement(IElement domElement)
        {
            return CopyAttributes(domElement, new SvgDescElement(domElement.TextContent, (Document)domElement.Owner!, domElement.Prefix, domElement.Flags));
        }

        private static SvgSvgElement LoadSvgElement(IElement domElement, IRenderDimensions renderDimensions, bool isTopLevel = false)
        {
            double x = 0, y = 0, width = 0, height = 0;
            if (isTopLevel)
            {
                string? sx = domElement.GetAttribute("x");
                string? sy = domElement.GetAttribute("y");
                string? swidth = domElement.GetAttribute("width");
                string? sheight = domElement.GetAttribute("height");

                x = UnitParser.ParseLengthOrCalc(new Text.StringSource(sx ?? "0")).AsPx(renderDimensions, RenderMode.Undefined);
                y = UnitParser.ParseLengthOrCalc(new Text.StringSource(sy ?? "0")).AsPx(renderDimensions, RenderMode.Undefined);
                width = UnitParser.ParseLengthOrCalc(new Text.StringSource(swidth ?? "0")).AsPx(renderDimensions, RenderMode.Undefined);
                height = UnitParser.ParseLengthOrCalc(new Text.StringSource(sheight ?? "0")).AsPx(renderDimensions, RenderMode.Undefined);
            }

            var element = new SvgSvgElement((Document)domElement.Owner!, domElement.Prefix, domElement.Flags, x, y, width, height);
            _ = CopyAttributes(domElement, element);

            return (SvgSvgElement)Load(domElement, element, renderDimensions);
        }

        public static SvgSvgElement Load(IElement rootElement, IRenderDimensions renderDimensions)
        {
            return LoadSvgElement(rootElement, renderDimensions, isTopLevel: true);
        }

    }
}
