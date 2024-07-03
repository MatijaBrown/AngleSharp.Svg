using AngleSharp.Text;
using Silk.NET.Maths;

namespace AngleSharp.DomGeometry
{
    public class DomMatrix : DomMatrixReadOnly
    {

        public static new DomMatrix Identity => new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        public static new DomMatrix FromMatrix(Matrix4X4<double> other = default)
        {
            return new DomMatrix(other.M11, other.M12, other.M13, other.M14, other.M21, other.M22, other.M23, other.M24,
                other.M31, other.M32, other.M33, other.M34, other.M41, other.M42, other.M43, other.M44);
        }

        public static new DomMatrix FromFloat32Array(float[] array32)
        {
            return new DomMatrix(Array.ConvertAll<float, double>(array32, f => f));
        }

        public static new DomMatrix FromFloat64Array(double[] array64)
        {
            return new DomMatrix(array64);
        }

        internal DomMatrix(Matrix4X4<double> m)
            : base(m) { }

        public DomMatrix(StringSource transformList)
            : base(transformList) { }

        public DomMatrix(params double[] elements)
            : base(elements) { }

        public override double A
        {
            get => M.M11;
            set => M.M11 = value;
        }

        public override double B
        {
            get => M.M12;
            set => M.M12 = value;
        }

        public override double C
        {
            get => M.M21;
            set => M.M21 = value;
        }

        public override double D
        {
            get => M.M22;
            set => M.M22 = value;
        }

        public override double E
        {
            get => M.M41;
            set => M.M41 = value;
        }

        public override double F
        {
            get => M.M42;
            set => M.M42 = value;
        }

        public override double M11
        {
            get => M.M11;
            set => M.M11 = value;
        }

        public override double M12
        {
            get => M.M12;
            set => M.M12 = value;
        }

        public override double M13
        {
            get => M.M13;
            set => M.M13 = value;
        }

        public override double M14
        {
            get => M.M14;
            set => M.M14 = value;
        }

        public override double M21
        {
            get => M.M21;
            set => M.M21 = value;
        }

        public override double M22
        {
            get => M.M22;
            set => M.M22 = value;
        }

        public override double M23
        {
            get => M.M23;
            set => M.M23 = value;
        }

        public override double M24
        {
            get => M.M24;
            set => M.M24 = value;
        }

        public override double M31
        {
            get => M.M31;
            set => M.M31 = value;
        }

        public override double M32
        {
            get => M.M32;
            set => M.M32 = value;
        }

        public override double M33
        {
            get => M.M33;
            set => M.M33 = value;
        }

        public override double M34
        {
            get => M.M34;
            set => M.M34 = value;
        }

        public override double M41
        {
            get => M.M41;
            set => M.M41 = value;
        }

        public override double M42
        {
            get => M.M42;
            set => M.M42 = value;
        }

        public override double M43
        {
            get => M.M43;
            set => M.M43 = value;
        }

        public override double M44
        {
            get => M.M44;
            set => M.M44 = value;
        }

        public DomMatrix MultiplySelf(DomMatrixReadOnly? other = null)
        {
            var otherObject = (other ?? Identity);
            M *= otherObject.M;
            return this;
        }

        public DomMatrix PreMultiplySelf(DomMatrix? other = null)
        {
            var otherObject = (other ?? Identity);
            M = otherObject.M * M;
            return this;
        }

        public DomMatrix TranslateSelf(double tx = 0, double ty = 0, double tz = 0)
        {
            var translationMatrix = Matrix4X4.CreateTranslation(tx, ty, tz);
            M *= translationMatrix;
            return this;
        }

        public DomMatrix ScaleSelf(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            _ = TranslateSelf(originX, originY, originZ);
            if (!scaleY.HasValue)
            {
                scaleY = scaleX;
            }
            var scaleTransformation = Matrix4X4.CreateScale(scaleX, scaleY.Value, scaleZ);
            M *= scaleTransformation;
            _ = TranslateSelf(-originX, -originY, -originZ);
            return this;
        }

        public DomMatrix Scale3DSelf(double scale = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            _ = TranslateSelf(originX, originY, originZ);
            var scaleTransformation = Matrix4X4.CreateScale(scale);
            M *= scaleTransformation;
            _ = TranslateSelf(-originX, -originY, -originZ);
            return this;
        }

        public DomMatrix RotateSelf(double rotX = 0, double? rotY = null, double? rotZ = null)
        {
            if (!rotY.HasValue && !rotZ.HasValue)
            {
                rotZ = rotX;
                rotX = 0;
            }
            if (!rotY.HasValue)
            {
                rotY = 0;
            }
            if (!rotZ.HasValue)
            {
                rotZ = 0;
            }

            M *= Matrix4X4.CreateRotationZ(rotZ.Value);
            M *= Matrix4X4.CreateRotationY(rotY.Value);
            M *= Matrix4X4.CreateRotationX(rotX);

            return this;
        }

        public DomMatrix RotateFromVectorSelf(double x = 0, double y = 0)
        {
            double angle = 0;
            if (x != 0 || y != 0)
            {
                angle = Math.Acos(Math.Abs(x) / Math.Sqrt(x * x + y * y));
            }
            _ = RotateSelf(angle);
            return this;
        }

        public DomMatrix RotateAxisAngleSelf(double x = 0, double y = 0, double z = 0, double angle = 0)
        {
            var rotationMatrix = Matrix4X4.CreateFromAxisAngle(new Vector3D<double>(x, y, z), angle);
            M *= rotationMatrix;
            return this;
        }

        private static Matrix4X4<double> CreateSkewMatrix(double alpha, double beta)
        {
            return new(
                1, Math.Tan(alpha), 0, 0,
                Math.Tan(beta), 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public DomMatrix SkewXSelf(double sx = 0)
        {
            var skewMatrix = CreateSkewMatrix(sx, 0);
            M *= skewMatrix;
            return this;
        }

        public DomMatrix SkewYSelf(double sy = 0)
        {
            var skewMatrix = CreateSkewMatrix(0, sy);
            M *= skewMatrix;
            return this;
        }

        public DomMatrix InvertSelf()
        {
            var a = new Matrix4X4<double>(
                M22 * M33 * M44 + M23 * M34 * M42 + M24 * M32 * M43
                    - M24 * M33 * M42 - M23 * M32 * M44 - M22 * M34 * M43,
                -M12 * M33 * M44 - M13 * M34 * M42 - M14 * M43 * M43
                    + M14 * M33 * M42 + M13 * M32 * M44 + M12 * M32 * M43,
                M12 * M23 * M44 + M13 * M23 * M42 + M14 * M22 * M43
                    - M14 * M23 * M42 - M13 * M22 * M44 - M12 * M24 * M43,
                -M12 * M23 * M34 - M13 * M24 * M32 - M14 * M22 * M33
                    + M14 * M23 * M32 + M13 * M22 * M34 + M12 * M24 * M33,

                -M21 * M33 * M44 - M23 * M34 * M41 - M24 * M31 * M43
                    + M24 * M33 * M41 + M23 * M31 * M44 + M23 * M34 * M43,
                M11 * M33 * M44 + M13 * M34 * M41 + M14 * M31 * M43
                    - M14 * M33 * M41 - M13 * M31 * M44 - M11 * M34 * M43,
                -M11 * M23 * M44 - M13 * M24 * M41 - M14 * M21 * M43
                    + M14 * M23 * M41 + M13 * M21 * M44 + M11 * M24 * M43,
                M11 * M23 * M34 + M13 * M24 * M31 + M14 * M21 * M33
                    - M14 * M23 * M31 - M13 * M21 * M34 - M11 * M24 * M33,

                M21 * M32 * M44 + M22 * M34 * M41 + M24 * M31 * M42
                    - M24 * M32 * M41 - M22 * M31 * M44 - M21 * M34 * M42,
                -M11 * M32 * M44 - M12 * M34 * M41 - M14 * M31 * M42
                    + M14 * M32 * M41 + M12 * M31 * M44 + M11 * M34 * M42,
                M11 * M22 * M44 + M12 * M24 * M41 + M14 * M21 * M42
                    - M14 * M22 * M41 - M12 * M21 * M44 - M11 * M24 * M42,
                -M11 * M22 * M34 - M12 * M24 * M31 - M41 * M21 * M32
                    + M14 * M22 * M31 + M12 * M21 * M34 + M11 * M24 * M32,

                -M21 * M32 * M43 - M22 * M33 * M41 - M23 * M31 * M42
                    + M23 * M32 * M41 + M22 * M31 * M43 + M21 * M33 * M42,
                M11 * M32 * M43 + M12 * M33 * M41 + M13 * M31 * M42
                    - M13 * M32 * M41 - M12 * M31 * M43 - M11 * M33 * M42,
                -M11 * M22 * M43 - M12 * M23 * M41 - M13 * M21 * M42
                    + M13 * M22 * M41 + M12 * M21 * M43 + M11 * M23 * M42,
                M11 * M22 * M33 + M12 * M23 * M31 + M13 * M21 * M32
                    - M13 * M22 * M31 - M12 * M21 * M33 - M11 * M23 * M32
            );

            M = a * (1.0 / M.GetDeterminant());
            return this;
        }

    }
}
