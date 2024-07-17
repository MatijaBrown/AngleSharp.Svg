using AngleSharp.DomGeometry.Parser.Functions;
using AngleSharp.DomGeometry.Parser;
using System.Text;

namespace AngleSharp.DomGeometry.Dom
{
    public class DomMatrix : IDomMatrix, IDomMatrixReadOnly
    {

        private AbstractMatrix _mat;

        public double A { get => M11; set => M11 = value; }

        public double B { get => M12; set => M12 = value; }

        public double C { get => M21; set => M21 = value; }

        public double D { get => M22; set => M22 = value; }

        public double E { get => M41; set => M41 = value; }

        public double F { get => M42; set => M42 = value; }

        public double M11 { get => _mat.M11; set => _mat.M11 = value; }

        public double M12 { get => _mat.M12; set => _mat.M12 = value; }

        public double M13
        {
            get => _mat.M13;
            set
            {
                _mat.M13 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M14
        {
            get => _mat.M14;
            set
            {
                _mat.M14 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M21 { get => _mat.M21; set => _mat.M21 = value; }

        public double M22 { get => _mat.M22; set => _mat.M22 = value; }

        public double M23
        {
            get => _mat.M23;
            set
            {
                _mat.M23 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M24
        {
            get => _mat.M24;
            set
            {
                _mat.M24 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M31
        {
            get => _mat.M31;
            set
            {
                _mat.M31 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M32
        {
            get => _mat.M32;
            set
            {
                _mat.M32 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M33
        {
            get => _mat.M33;
            set
            {
                _mat.M33 = value;
                Is2D &= (value == 1D);
            }
        }

        public double M34
        {
            get => _mat.M34;
            set
            {
                _mat.M34 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M41 { get => _mat.M41; set => _mat.M41 = value; }

        public double M42 { get => _mat.M42; set => _mat.M42 = value; }

        public double M43
        {
            get => _mat.M43;
            set
            {
                _mat.M43 = value;
                Is2D &= MathsUtils.IsZero(value);
            }
        }

        public double M44
        {
            get => _mat.M44;
            set
            {
                _mat.M44 = value;
                Is2D &= (value == 1D);
            }
        }

        public bool Is2D { get; private set; }

        public bool IsIdentity
            => MathsUtils.Is(_mat.M11, 1) && MathsUtils.Is(_mat.M12, 0) && MathsUtils.Is(_mat.M13, 0) && MathsUtils.Is(_mat.M14, 0)
            && MathsUtils.Is(_mat.M21, 0) && MathsUtils.Is(_mat.M22, 1) && MathsUtils.Is(_mat.M23, 0) && MathsUtils.Is(_mat.M24, 0)
            && MathsUtils.Is(_mat.M31, 0) && MathsUtils.Is(_mat.M32, 0) && MathsUtils.Is(_mat.M33, 1) && MathsUtils.Is(_mat.M34, 0)
            && MathsUtils.Is(_mat.M41, 0) && MathsUtils.Is(_mat.M42, 0) && MathsUtils.Is(_mat.M43, 0) && MathsUtils.Is(_mat.M44, 1);

        public DomMatrix(string init)
        {
            if (init.Length == 0)
            {
                init = "matrix(1, 0, 0, 1, 0, 0)";
            }
            IList<ITransformFunction> transformFunctions = TransformParser.ParseSvgTransform(init);
            if (transformFunctions.Count == 0)
            {
                transformFunctions.Add(new MatrixFunction(1, 0, 0, 1, 0, 0));
            }

            _mat = AbstractMatrix.Identity;
            Is2D = true;
            foreach (ITransformFunction t in transformFunctions)
            {
                Is2D &= t.Is2D;
                _mat.Postmultiply(t.ToMatrix());
            }
        }

        public DomMatrix(params double[]? init)
        {
            if ((init == null) || (init.Length == 0))
            {
                _mat = AbstractMatrix.Identity;
                Is2D = true;
            }
            else if (init.Length == 6)
            {
                _mat = new AbstractMatrix(init[0], init[2], init[4], init[1], init[3], init[5]);
                Is2D = true;
            }
            else if (init.Length == 16)
            {
                _mat = new AbstractMatrix(
                    init[0], init[4], init[8], init[12],
                    init[1], init[5], init[9], init[13],
                    init[2], init[6], init[10], init[14],
                    init[3], init[7], init[11], init[15]
                );
                Is2D = false;
            }
            else
            {
                throw new ArgumentException("DomMatrix init array must be either 'null' or of length '6' or '16'");
            }
        }

        public DomMatrix()
            : this(1, 0, 0, 1, 0, 0) { }

        private DomMatrix(AbstractMatrix mat, bool is2D)
        {
            _mat = mat;
            Is2D = is2D;
        }

        public IDomMatrix Translate(double tx = 0, double ty = 0, double tz = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.TranslateSelf(tx, ty, tz);
        }

        public IDomMatrix Scale(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            scaleY ??= scaleX;
            var result = new DomMatrix(_mat, Is2D);
            return result.ScaleSelf(scaleX, scaleY, scaleZ, originX, originY, originZ);
        }

        public IDomMatrix ScaleNonUniform(double scaleX = 1, double scaleY = 1)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.ScaleSelf(scaleX, scaleY, 1, 0, 0, 0);
        }

        public IDomMatrix Scale3D(double scale = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.Scale3DSelf(scale, originX, originY, originZ);
        }

        public IDomMatrix Rotate(double rotX = 0, double? rotY = null, double? rotZ = null)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.RotateSelf(rotX, rotY, rotZ);
        }

        public IDomMatrix RotateFromVector(double x = 0, double y = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.RotateFromVectorSelf(x, y);
        }

        public IDomMatrix RotateAxisAngle(double x = 0, double y = 0, double z = 0, double angle = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.RotateAxisAngleSelf(x, y, z, angle);
        }

        public IDomMatrix SkewX(double sx = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.SkewXSelf(sx);
        }

        public IDomMatrix SkewY(double sy = 0)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.SkewYSelf(sy);
        }

        public IDomMatrix Multiply(IDomMatrixReadOnly other)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.MultiplySelf(other);
        }

        public IDomMatrix Multiply(IDomMatrix other)
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.MultiplySelf(other);
        }

        public IDomMatrix FlipX()
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.MultiplySelf((IDomMatrixReadOnly)(new DomMatrix(-1, 0, 0, 1, 0, 0)));
        }

        public IDomMatrix FlipY()
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.MultiplySelf((IDomMatrixReadOnly)(new DomMatrix(1, 0, 0, -1, 0, 0)));
        }

        public IDomMatrix Inverse()
        {
            var result = new DomMatrix(_mat, Is2D);
            return result.InvertSelf();
        }

        public IDomPoint TransformPoint(IDomPointReadOnly point)
        {
            return point.MatrixTransform((IDomMatrixReadOnly)this);
        }

        public IDomPoint TransformPoint(IDomPoint point)
        {
            return point.MatrixTransform((IDomMatrixReadOnly)this);
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
            if (!(double.IsFinite(M11) && double.IsFinite(M21) && double.IsFinite(M31) && double.IsFinite(M41) &&
                double.IsFinite(M12) && double.IsFinite(M22) && double.IsFinite(M32) && double.IsFinite(M41) &&
                double.IsFinite(M13) && double.IsFinite(M23) && double.IsFinite(M33) && double.IsFinite(M43) &&
                double.IsFinite(M14) && double.IsFinite(M24) && double.IsFinite(M34) && double.IsFinite(M44)))
            {
                throw new InvalidOperationException("The CSS syntax cannot represent 'NaN' or 'Infinity' values.");
            }

            var sb = new StringBuilder();
            if (Is2D)
            {
                return sb.Append("matrix(")
                    .Append(_mat.M11).Append(", ")
                    .Append(_mat.M12).Append(", ")
                    .Append(_mat.M21).Append(", ")
                    .Append(_mat.M22).Append(", ")
                    .Append(_mat.M41).Append(", ")
                    .Append(_mat.M42).Append(')').ToString();
            }
            else
            {
                return sb.Append("matrix3d(")
                    .Append(_mat.M11).Append(", ")
                    .Append(_mat.M12).Append(", ")
                    .Append(_mat.M13).Append(", ")
                    .Append(_mat.M14).Append(", ")

                    .Append(_mat.M21).Append(", ")
                    .Append(_mat.M22).Append(", ")
                    .Append(_mat.M23).Append(", ")
                    .Append(_mat.M24).Append(", ")

                    .Append(_mat.M31).Append(", ")
                    .Append(_mat.M32).Append(", ")
                    .Append(_mat.M33).Append(", ")
                    .Append(_mat.M34).Append(", ")

                    .Append(_mat.M41).Append(", ")
                    .Append(_mat.M42).Append(", ")
                    .Append(_mat.M43).Append(", ")
                    .Append(_mat.M44).Append(')').ToString();
            }
        }

        public IDomMatrix MultiplySelf(IDomMatrixReadOnly other)
        {
            _mat.Postmultiply(((DomMatrix)other)._mat);
            Is2D &= other.Is2D;
            return this;
        }

        public IDomMatrix MultiplySelf(IDomMatrix other)
        {
            _mat.Postmultiply(((DomMatrix)other)._mat);
            Is2D &= other.Is2D;
            return this;
        }

        public IDomMatrix PreMultiplySelf(IDomMatrixReadOnly other)
        {
            _mat.Premultiply(((DomMatrix)other)._mat);
            Is2D &= other.Is2D;
            return this;
        }

        public IDomMatrix PreMultiplySelf(IDomMatrix other)
        {
            _mat.Premultiply(((DomMatrix)other)._mat);
            Is2D &= other.Is2D;
            return this;
        }

        public IDomMatrix TranslateSelf(double tx = 0, double ty = 0, double tz = 0)
        {
            _mat.Translate(tx, ty, tz);
            Is2D &= MathsUtils.IsZero(tz);
            return this;
        }

        public IDomMatrix ScaleSelf(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            scaleY ??= scaleX;
            _mat.Scale(scaleX, scaleY.Value, scaleZ, originX, originY, originZ);
            Is2D &= ((scaleZ == 1D) & MathsUtils.IsZero(originZ));
            return this;
        }

        public IDomMatrix Scale3DSelf(double scale = 1, double originX = 0, double originY = 0, double originZ = 0)
        {
            _mat.Scale(scale, scale, scale, originX, originY, originZ);
            Is2D = (scale == 1D);
            return this;
        }

        public IDomMatrix RotateSelf(double rotX = 0, double? rotY = null, double? rotZ = null)
        {
            if (!rotY.HasValue && !rotZ.HasValue)
            {
                rotZ = rotX;
                rotX = 0;
                rotY = 0;
            }
            if (!rotY.HasValue)
            {
                rotY = 0;
            }
            if (!rotZ.HasValue)
            {
                rotZ = 0;
            }
            Is2D &= (MathsUtils.IsZero(rotX) & MathsUtils.IsZero(rotY.Value));
            _mat.RotateAxisAngle(0, 0, 1, MathsUtils.ToRadians(rotZ.Value));
            _mat.RotateAxisAngle(0, 1, 0, MathsUtils.ToRadians(rotY.Value));
            _mat.RotateAxisAngle(1, 0, 0, MathsUtils.ToRadians(rotX));
            return this;
        }

        public IDomMatrix RotateFromVectorSelf(double x = 0, double y = 0)
        {
            MathsUtils.Normalise(ref x, ref y);
            double angle = MathsUtils.AngleOnUnitCircle(x, y);
            _mat.RotateAxisAngle(0, 0, 1, MathsUtils.ToRadians(angle));
            return this;
        }

        public IDomMatrix RotateAxisAngleSelf(double x = 0, double y = 0, double z = 0, double angle = 0)
        {
            _mat.RotateAxisAngle(x, y, z, MathsUtils.ToRadians(angle));
            Is2D &= (MathsUtils.IsZero(x) & MathsUtils.IsZero(y));
            return this;
        }

        public IDomMatrix SkewXSelf(double sx = 0)
        {
            _mat.SkewX(MathsUtils.ToRadians(sx));
            return this;
        }

        public IDomMatrix SkewYSelf(double sy = 0)
        {
            _mat.SkewY(MathsUtils.ToRadians(sy));
            return this;
        }

        public IDomMatrix InvertSelf()
        {
            Is2D &= _mat.Invert();
            return this;
        }

    }
}
