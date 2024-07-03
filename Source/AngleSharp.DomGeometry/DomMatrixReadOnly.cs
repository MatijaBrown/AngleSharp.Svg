using AngleSharp.DomGeometry.Internal;
using AngleSharp.Text;
using Silk.NET.Maths;
using System.Text;

namespace AngleSharp.DomGeometry
{
    public class DomMatrixReadOnly
    {

        public static DomMatrixReadOnly Identity => new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        public static DomMatrixReadOnly FromMatrix(Matrix4X4<double> other = default)
        {
            return new DomMatrixReadOnly(other.M11, other.M12, other.M13, other.M14, other.M21, other.M22, other.M23, other.M24,
                other.M31, other.M32, other.M33, other.M34, other.M41, other.M42, other.M43, other.M44);
        }

        public static DomMatrixReadOnly FromFloat32Array(float[] array32)
        {
            return new DomMatrixReadOnly(Array.ConvertAll<float, double>(array32, f => f));
        }

        public static DomMatrixReadOnly FromFloat64Array(double[] array64)
        {
            return new DomMatrixReadOnly(array64);
        }

        internal Matrix4X4<double> M;

        internal DomMatrixReadOnly(Matrix4X4<double> m)
        {
            M = m;
        }

        private static Matrix4X4<double> ParseStringToAbstractMatrix(StringSource transformList)
        {
            if (transformList.Content.Length == 0)
            {
                transformList = new StringSource("matrix(1, 0, 0, 1, 0, 0)");
            }

            IList<ITransformFunction?> transformFunctions = TransformParser.ParseTransform(transformList);

            Matrix4X4<double> matrix = Matrix4X4<double>.Identity;
            foreach (ITransformFunction? transformFunction in transformFunctions)
            {
                if (transformFunction != null)
                {
                    matrix = matrix * transformFunction.Abstract4x4Matrix;
                }
            }
            return matrix;
        }

        public DomMatrixReadOnly(StringSource transformList)
            : this(ParseStringToAbstractMatrix(transformList)) { }

        public DomMatrixReadOnly(params double[] elements)
        {
            if (elements.Length == 6)
            {
                M = new Matrix4X4<double>(elements[0], elements[1], 0, elements[4], elements[2], elements[3], 0, elements[5], 0, 0, 1, 0, 0, 0, 0, 1);
            }
            else if (elements.Length == 16)
            {
                M = new Matrix4X4<double>(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5], elements[6], elements[7], elements[8],
                    elements[9], elements[10], elements[11], elements[12], elements[13], elements[14], elements[15]);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public virtual double A
        {
            get => M.M11;
            set => throw new InvalidOperationException("Cannot set A (M11) property of DomReadOnlyMatrix");
        }

        public virtual double B
        {
            get => M.M12;
            set => throw new InvalidOperationException("Cannot set B (M12) property of DomReadOnlyMatrix");
        }

        public virtual double C
        {
            get => M.M21;
            set => throw new InvalidOperationException("Cannot set C (M21) property of DomReadOnlyMatrix");
        }

        public virtual double D
        {
            get => M.M22;
            set => throw new InvalidOperationException("Cannot set D (M11) property of DomReadOnlyMatrix");
        }

        public virtual double E
        {
            get => M.M41;
            set => throw new InvalidOperationException("Cannot set E (M41) property of DomReadOnlyMatrix");
        }

        public virtual double F
        {
            get => M.M42;
            set => throw new InvalidOperationException("Cannot set F (M42) property of DomReadOnlyMatrix");
        }

        public virtual double M11
        {
            get => M.M11;
            set => throw new InvalidOperationException("Cannot set M11 property of DomReadOnlyMatrix");
        }

        public virtual double M12
        {
            get => M.M12;
            set => throw new InvalidOperationException("Cannot set M12 property of DomReadOnlyMatrix");
        }

        public virtual double M13
        {
            get => M.M13;
            set => throw new InvalidOperationException("Cannot set M13 property of DomReadOnlyMatrix");
        }

        public virtual double M14
        {
            get => M.M14;
            set => throw new InvalidOperationException("Cannot set M14 property of DomReadOnlyMatrix");
        }

        public virtual double M21
        {
            get => M.M21;
            set => throw new InvalidOperationException("Cannot set M21 property of DomReadOnlyMatrix");
        }

        public virtual double M22
        {
            get => M.M22;
            set => throw new InvalidOperationException("Cannot set M22 property of DomReadOnlyMatrix");
        }

        public virtual double M23
        {
            get => M.M23;
            set => throw new InvalidOperationException("Cannot set M23 property of DomReadOnlyMatrix");
        }

        public virtual double M24
        {
            get => M.M24;
            set => throw new InvalidOperationException("Cannot set M24 property of DomReadOnlyMatrix");
        }

        public virtual double M31
        {
            get => M.M31;
            set => throw new InvalidOperationException("Cannot set M31 property of DomReadOnlyMatrix");
        }

        public virtual double M32
        {
            get => M.M32;
            set => throw new InvalidOperationException("Cannot set M32 property of DomReadOnlyMatrix");
        }

        public virtual double M33
        {
            get => M.M33;
            set => throw new InvalidOperationException("Cannot set M33 property of DomReadOnlyMatrix");
        }

        public virtual double M34
        {
            get => M.M34;
            set => throw new InvalidOperationException("Cannot set M34 property of DomReadOnlyMatrix");
        }

        public virtual double M41
        {
            get => M.M41;
            set => throw new InvalidOperationException("Cannot set M41 property of DomReadOnlyMatrix");
        }

        public virtual double M42
        {
            get => M.M42;
            set => throw new InvalidOperationException("Cannot set M42 property of DomReadOnlyMatrix");
        }

        public virtual double M43
        {
            get => M.M43;
            set => throw new InvalidOperationException("Cannot set M43 property of DomReadOnlyMatrix");
        }

        public virtual double M44
        {
            get => M.M44;
            set => throw new InvalidOperationException("Cannot set M44 property of DomReadOnlyMatrix");
        }

        public bool Is2D => (M13 == 0.0) && (M23 == 0.0) && (M31 == 0.0) && (M32 == 0.0) && (M34 == 0.0)
            && (M41 == 0.0) && (M42 == 0.0) && (M43 == 0.0) && (M33 == 1.0) && (M44 == 1.0);

        public bool IsIdentity => M.IsIdentity;

        public DomMatrix Translate(double tx = 0, double ty = 0, double tz = 0)
        {
            var result = new DomMatrix(M);
            return result.TranslateSelf(tx, ty, tz);
        }

        public DomMatrix Scale(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            var result = new DomMatrix(M);
            return result.ScaleSelf(scaleX, scaleY, scaleZ, originX, originY, originZ);
        }

        public DomMatrix ScaleNonUniform(double scaleX = 1, double scaleY = 1)
        {
            var result = new DomMatrix(M);
            return result.ScaleSelf(scaleX, scaleY, 1, 0, 0, 0);
        }

        public DomMatrix Scale3D(double scale = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            var result = new DomMatrix(M);
            return result.Scale3DSelf(scale, originX, originY, originZ);
        }

        public DomMatrix Rotate(double rotX = 0, double? rotY = null, double? rotZ = null)
        {
            var result = new DomMatrix(M);
            return result.RotateSelf(rotX, rotY, rotZ);
        }

        public DomMatrix RotateFromVector(double x = 0, double y = 0)
        {
            var result = new DomMatrix(M);
            return result.RotateFromVectorSelf(x, y);
        }

        public DomMatrix RotateAxisAngle(double x = 0, double y = 0, double z = 0, double angle = 0)
        {
            var result = new DomMatrix(M);
            return result.RotateAxisAngleSelf(x, y, z, angle);
        }

        public DomMatrix SkewX(double sx = 0)
        {
            var result = new DomMatrix(M);
            return result.SkewXSelf(sx);
        }

        public DomMatrix SkewY(double sy = 0)
        {
            var result = new DomMatrix(M);
            return result.SkewYSelf(sy);
        }

        public DomMatrix Multiply(DomMatrixReadOnly? other = null)
        {
            var result = new DomMatrix(M);
            return result.MultiplySelf(other);
        }

        public DomMatrix FlipX()
        {
            var result = new DomMatrix(M);
            return result.Multiply(new DomMatrixReadOnly(-1, 0, 0, 1, 0, 0));
        }

        public DomMatrix FlipY()
        {
            var result = new DomMatrix(M);
            return result.Multiply(new DomMatrixReadOnly(1, 0, 0, -1, 0, 0));
        }

        public DomMatrix Inverse()
        {
            var result = new DomMatrix(M);
            return result.InvertSelf();
        }

        public DomPoint TransformPoint(DomPointReadonly point)
        {
            return point.MatrixTransform(this);
        }

        public float[] ToFloat32Array()
        {
            return Array.ConvertAll(ToFloat64Array(), d => (float)d);
        }

        public double[] ToFloat64Array()
        {
            return [
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44
            ];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Is2D)
            {
                _ = sb.Append("matrix(")
                    .Append(M11).Append(", ")
                    .Append(M12).Append(", ")
                    .Append(M21).Append(", ")
                    .Append(M22).Append(", ")
                    .Append(M14).Append(", ")
                    .Append(M24).Append(')');
            }
            else
            {
                _ = sb.Append("matrix3d(")
                    .Append(M11).Append(", ")
                    .Append(M12).Append(", ")
                    .Append(M13).Append(", ")
                    .Append(M14).Append(", ")
                    .Append(M21).Append(", ")
                    .Append(M22).Append(", ")
                    .Append(M23).Append(", ")
                    .Append(M24).Append(", ")
                    .Append(M31).Append(", ")
                    .Append(M32).Append(", ")
                    .Append(M33).Append(", ")
                    .Append(M34).Append(", ")
                    .Append(M41).Append(", ")
                    .Append(M42).Append(", ")
                    .Append(M43).Append(", ")
                    .Append(M44).Append(')');
            }
            return sb.ToString();
        }

    }
}
