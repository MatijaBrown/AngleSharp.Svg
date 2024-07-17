namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomQuad
    {

        static IDomQuad FromRect(IDomRect other)
        {
            var point1 = new DomPoint(other.X, other.Y, 0, 1);
            var point2 = new DomPoint(other.X + other.Width, other.Y, 0, 1);
            var point3 = new DomPoint(other.X + other.Width, other.Y + other.Height, 0, 1);
            var point4 = new DomPoint(other.X, other.Y + other.Height, 0, 1);
            return new DomQuad(point1, point2, point3, point4);
        }

        static IDomQuad FromRect(IDomRectReadOnly other)
        {
            var point1 = new DomPoint(other.X, other.Y, 0, 1);
            var point2 = new DomPoint(other.X + other.Width, other.Y, 0, 1);
            var point3 = new DomPoint(other.X + other.Width, other.Y + other.Height, 0, 1);
            var point4 = new DomPoint(other.X, other.Y + other.Height, 0, 1);
            return new DomQuad(point1, point2, point3, point4);
        }

        static IDomQuad FromQuad(IDomQuad other)
        {
            return new DomQuad(other.P1, other.P2, other.P3, other.P4);
        }

        IDomPoint P1 { get; }

        IDomPoint P2 { get; }

        IDomPoint P3 { get; }

        IDomPoint P4 { get; }

        IDomRect GetBounds();

    }
}
