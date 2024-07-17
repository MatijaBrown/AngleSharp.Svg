namespace AngleSharp.DomGeometry.Dom
{
    public class DomRect(double x = 0, double y = 0, double width = 0, double height = 0) : IDomRect, IDomRectReadOnly
    {

        public double X { get => x; set => x = value; }

        public double Y { get => y; set => y = value; }

        public double Width { get => width; set => width = value; }

        public double Height { get => height; set => height = value; }

        public double Top => Math.Min(Y, Y + Height);

        public double Right => Math.Max(X, X + Width);

        public double Bottom => Math.Max(Y, Y + Height);

        public double Left => Math.Min(X, X + Width);

    }
}
