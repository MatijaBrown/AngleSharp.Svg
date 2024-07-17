namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomPointReadOnly
    {

        static IDomPoint FromPoint(IDomPointReadOnly other)
        {
            return new DomPoint(other.X, other.Y, other.Z, other.W);
        }

        static IDomPoint FromPoint(IDomPoint other)
        {
            return new DomPoint(other.X, other.Y, other.Z, other.W);
        }

        double X { get; }

        double Y { get; }

        double Z { get; }

        double W { get; }

        IDomPoint MatrixTransform(IDomMatrixReadOnly matrix);

        IDomPoint MatrixTransform(IDomMatrix matrix);

    }
}
