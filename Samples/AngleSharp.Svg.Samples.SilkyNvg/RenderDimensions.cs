using AngleSharp.Css;

namespace AngleSharp.Svg.Samples.SilkyNvg
{
    internal struct RenderDimensions : IRenderDimensions
    {

        public double RenderWidth { get; set; }


        public double RenderHeight { get; set; }


        public double FontSize { get; set; }

        public void Update(double width, double height)
        {
            RenderWidth = width;
            RenderHeight = height;
            FontSize = 0;
        }

    }
}
