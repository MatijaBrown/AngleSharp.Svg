namespace AngleSharp.DomGeometry.Dom
{
    public class DomPoint(double x = 0, double y = 0, double z = 0, double w = 1) : IDomPoint, IDomPointReadOnly
    {

        public double X { get => x; set => x = value; }

        public double Y { get => y; set => y = value; }

        public double Z { get => z; set => z = value; }

        public double W { get => w; set => w = value; }

        public IDomPoint MatrixTransform(IDomMatrixReadOnly matrix)
        {
            double x = X;
            double y = Y;
            double z = Z;
            double w = W;

            return new DomPoint(
                x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41,
                x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42,
                x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43,
                x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44
            );
        }

        public IDomPoint MatrixTransform(IDomMatrix matrix)
        {
            double x = X;
            double y = Y;
            double z = Z;
            double w = W;

            return new DomPoint(
                x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41,
                x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42,
                x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43,
                x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44
            );
        }

    }
}
