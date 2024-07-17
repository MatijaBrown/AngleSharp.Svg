namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomMatrixReadOnly
    {

        static IDomMatrix FromMatrix(IDomMatrixReadOnly other)
        {
            return new DomMatrix(
                other.M11, other.M12, other.M13, other.M14,
                other.M21, other.M22, other.M23, other.M24,
                other.M31, other.M32, other.M33, other.M34,
                other.M41, other.M42, other.M43, other.M44
            );
        }

        static IDomMatrix FromMatrix(IDomMatrix other)
        {
            return new DomMatrix(
                other.M11, other.M12, other.M13, other.M14,
                other.M21, other.M22, other.M23, other.M24,
                other.M31, other.M32, other.M33, other.M34,
                other.M41, other.M42, other.M43, other.M44
            );
        }

        static IDomMatrixReadOnly FromFloat32Array(float[] array32)
        {
            return new DomMatrix(Array.ConvertAll<float, double>(array32, f => f));
        }

        static IDomMatrixReadOnly FromFloat64Array(double[] array64)
        {
            return new DomMatrix(array64);
        }

        public double A { get; }

        public double B { get; }

        public double C { get; }

        public double D { get; }

        public double E { get; }

        public double F { get; }

        public double M11 { get; }

        public double M12 { get; }

        public double M13 { get; }

        public double M14 { get; }

        public double M21 { get; }

        public double M22 { get; }

        public double M23 { get; }

        public double M24 { get; }

        public double M31 { get; }

        public double M32 { get; }

        public double M33 { get; }

        public double M34 { get; }

        public double M41 { get; }

        public double M42 { get; }

        public double M43 { get; }

        public double M44 { get; }

        public bool Is2D { get; }

        public bool IsIdentity { get; }

        IDomMatrix Translate(double tx = 0, double ty = 0, double tz = 0);

        IDomMatrix Scale(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0);

        IDomMatrix ScaleNonUniform(double scaleX = 1, double scaleY = 1);

        IDomMatrix Scale3D(double scale = 1, double originX = 0, double originY = 0, double originZ = 0);

        IDomMatrix Rotate(double rotX = 0, double? rotY = null, double? rotZ = null);

        IDomMatrix RotateFromVector(double x = 0, double y = 0);

        IDomMatrix RotateAxisAngle(double x = 0, double y = 0, double z = 0, double angle = 0);

        IDomMatrix SkewX(double sx = 0);

        IDomMatrix SkewY(double sy = 0);

        IDomMatrix Multiply(IDomMatrixReadOnly other);

        IDomMatrix Multiply(IDomMatrix other);

        IDomMatrix FlipX();

        IDomMatrix FlipY();

        IDomMatrix Inverse();

        IDomPoint TransformPoint(IDomPointReadOnly point);

        IDomPoint TransformPoint(IDomPoint point);

        float[] ToFloat32Array();

        double[] ToFloat64Array();

    }
}
