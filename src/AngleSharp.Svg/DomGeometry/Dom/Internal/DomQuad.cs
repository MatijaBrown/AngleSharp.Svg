namespace AngleSharp.DomGeometry.Dom
{
    public class DomQuad(IDomPoint p1, IDomPoint p2, IDomPoint p3, IDomPoint p4) : IDomQuad
    {

        public IDomPoint P1 { get; } = p1;

        public IDomPoint P2 { get; } = p2;

        public IDomPoint P3 { get; } = p3;

        public IDomPoint P4 { get; } = p4;

        public IDomRect GetBounds()
        {
            double left = MathsUtils.Min(P1.X, P2.X, P3.X, P4.X);
            double top = MathsUtils.Min(P1.Y, P2.Y, P3.Y, P4.Y);
            double right = MathsUtils.Max(P1.X, P2.X, P3.X, P4.X);
            double bottom = MathsUtils.Max(P1.Y, P2.Y, P3.Y, P4.Y);

            return new DomRect()
            {
                X = left,
                Y = top,
                Width = right - left,
                Height = bottom - top
            };
        }
    }
}
