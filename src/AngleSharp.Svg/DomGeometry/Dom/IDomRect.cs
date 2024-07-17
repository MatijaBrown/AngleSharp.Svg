namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomRect
    {

        static IDomRectReadOnly FromRect(IDomRectReadOnly other)
        {
            return new DomRect(other.X, other.Y, other.Width, other.Height);
        }

        static IDomRectReadOnly FromRect(IDomRect other)
        {
            return new DomRect(other.X, other.Y, other.Width, other.Height);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Top { get; }

        public double Right { get; }

        public double Bottom { get; }

        public double Left { get; }

    }
}
