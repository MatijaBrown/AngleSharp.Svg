namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomPoint
    {

        static IDomPoint FromPoint(IDomPointReadOnly other)
        {
            return new DomPoint(other.X, other.Y, other.Z, other.W);
        }

        static IDomPoint FromPoint(IDomPoint other)
        {
            return new DomPoint(other.X, other.Y, other.Z, other.W);
        }

        double X { get; set; }

        double Y { get; set; }

        double Z { get; set; }

        double W { get; set; }

        IDomPoint MatrixTransform(IDomMatrixReadOnly matrix);

        IDomPoint MatrixTransform(IDomMatrix matrix);

    }
}
