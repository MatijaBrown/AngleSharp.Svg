namespace AngleSharp.DomGeometry
{
    public class DomRect : DomRectReadOnly
    {

        public DomRect(double x = 0, double y = 0, double width = 0, double height = 0)
            : base(x, y, width, height) { }

        public override double X
        {
            get => _x;
            set => _x = value;
        }

        public override double Y
        {
            get => _y;
            set => _y = value;
        }

        public override double Width
        {
            get => _width;
            set => _width = value;
        }

        public override double Height
        {
            get => _height;
            set => _height = value;
        }

    }
}
