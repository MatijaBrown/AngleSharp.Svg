namespace AngleSharp.DomGeometry
{
    public class DomRectReadOnly
    {

        protected double _x, _y;
        protected double _width, _height;

        public DomRectReadOnly(double x = 0, double y = 0, double width = 0, double height = 0)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public virtual double X
        {
            get => _x;
            set => throw new InvalidOperationException("Cannot set X property of DomRectReadOnly");
        }

        public virtual double Y
        {
            get => _y;
            set => throw new InvalidOperationException("Cannot set Y property of DomRectReadOnly");
        }

        public virtual double Width
        {
            get => _width;
            set => throw new InvalidOperationException("Cannot set Width property of DomRectReadOnly");
        }

        public virtual double Height
        {
            get => _height;
            set => throw new InvalidOperationException("Cannot set Height property of DomRectReadOnly");
        }

        public double Top => Math.Min(Y, Y + Height);

        public double Right => Math.Max(X, X + Width);

        public double Bottom => Math.Max(Y, Y + Height);

        public double Left => Math.Min(X, X + Width);

    }
}
