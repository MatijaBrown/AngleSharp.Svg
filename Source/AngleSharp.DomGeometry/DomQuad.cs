namespace AngleSharp.DomGeometry
{
    public class DomQuad
    {

        public static DomQuad FromRect(double x, double y, double width, double height)
        {
            var point1 = new DomPoint(x, y, 0, 1);
            var point2 = new DomPoint(x + width, y, 0, 1);
            var point3 = new DomPoint(x + width, y + height, 0, 1);
            var point4 = new DomPoint(x, y + height, 0, 1);

            return new DomQuad(point1, point2, point3, point4);
        }

        public static DomQuad FromQuad(DomPointReadonly p1, DomPointReadonly p2, DomPointReadonly p3, DomPointReadonly p4)
        {
            return new DomQuad(p1, p2, p3, p4);
        }

        public DomQuad(DomPointReadonly? p1 = null, DomPointReadonly? p2 = null, DomPointReadonly? p3 = null, DomPointReadonly? p4 = null)
        {
            p1 ??= new();
            p2 ??= new();
            p3 ??= new();
            p4 ??= new();

            P1 = new DomPoint(p1.X, p1.Y, p1.Z, p1.W);
            P2 = new DomPoint(p2.X, p2.Y, p2.Z, p2.W);
            P3 = new DomPoint(p3.X, p3.Y, p3.Z, p3.W);
            P4 = new DomPoint(p4.X, p4.Y, p4.Z, p4.W);
        }

        public DomPoint P1 { get; }

        public DomPoint P2 { get; }

        public DomPoint P3 { get; }

        public DomPoint P4 { get; }

    }
}
