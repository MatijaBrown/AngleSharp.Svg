namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomRectReadOnly
    {

        static IDomRectReadOnly FromRect(IDomRectReadOnly other)
        {
            return new DomRect(other.X, other.Y, other.Width, other.Height);
        }

        static IDomRectReadOnly FromRect(IDomRect other)
        {
            return new DomRect(other.X, other.Y, other.Width, other.Height);
        }

        public double X { get; }

        public double Y { get; }

        public double Width { get; }

        public double Height { get; }

        public double Top { get; }

        public double Right { get; }

        public double Bottom { get; }

        public double Left { get; }

    }
}
