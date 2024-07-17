namespace AngleSharp.DomGeometry.Dom
{
    /**
     * NOTE: AbstractMatrix, conforming to the DomGeometry-spec (https://www.w3.org/TR/geometry-1/#DOMMatrix)
     *  is named in COLUMN MAJOR order! I.e. M[Column][Row] not M[Row][Column] as is usual.
     */
    public struct AbstractMatrix(double m11, double m21, double m31, double m41, double m12, double m22, double m32, double m42,
        double m13, double m23, double m33, double m43, double m14, double m24, double m34, double m44)
    {

        internal readonly static AbstractMatrix Identity = new(1, 0, 0, 0, 1, 0);

        internal double M11 = m11;
        internal double M21 = m21;
        internal double M31 = m31;
        internal double M41 = m41;
        internal double M12 = m12;
        internal double M22 = m22;
        internal double M32 = m32;
        internal double M42 = m42;
        internal double M13 = m13;
        internal double M23 = m23;
        internal double M33 = m33;
        internal double M43 = m43;
        internal double M14 = m14;
        internal double M24 = m24;
        internal double M34 = m34;
        internal double M44 = m44;

        internal AbstractMatrix(double m11, double m21, double m41, double m12, double m22, double m42)
            : this(m11, m21, 0, m41, m12, m22, 0, m42, 0, 0, 1, 0, 0, 0, 0, 1) { }

        internal void Premultiply(AbstractMatrix other)
        {
            AbstractMatrix @this = this;

            M11 = other.M11 * @this.M11 + other.M21 * @this.M12 + other.M31 * @this.M13 + other.M41 * @this.M14;
            M21 = other.M11 * @this.M21 + other.M21 * @this.M22 + other.M31 * @this.M23 + other.M41 * @this.M24;
            M31 = other.M11 * @this.M31 + other.M21 * @this.M32 + other.M31 * @this.M33 + other.M41 * @this.M34;
            M41 = other.M11 * @this.M41 + other.M21 * @this.M42 + other.M31 * @this.M43 + other.M41 * @this.M44;

            M12 = other.M12 * @this.M11 + other.M22 * @this.M12 + other.M32 * @this.M13 + other.M42 * @this.M14;
            M22 = other.M12 * @this.M21 + other.M22 * @this.M22 + other.M32 * @this.M23 + other.M42 * @this.M24;
            M32 = other.M12 * @this.M31 + other.M22 * @this.M32 + other.M32 * @this.M33 + other.M42 * @this.M34;
            M42 = other.M12 * @this.M41 + other.M22 * @this.M42 + other.M32 * @this.M43 + other.M42 * @this.M44;

            M13 = other.M13 * @this.M11 + other.M23 * @this.M12 + other.M33 * @this.M13 + other.M43 * @this.M14;
            M23 = other.M13 * @this.M21 + other.M23 * @this.M22 + other.M33 * @this.M23 + other.M43 * @this.M24;
            M33 = other.M13 * @this.M31 + other.M23 * @this.M32 + other.M33 * @this.M33 + other.M43 * @this.M34;
            M43 = other.M13 * @this.M41 + other.M23 * @this.M42 + other.M33 * @this.M43 + other.M43 * @this.M44;

            M14 = other.M14 * @this.M11 + other.M24 * @this.M12 + other.M34 * @this.M13 + other.M44 * @this.M14;
            M24 = other.M14 * @this.M21 + other.M24 * @this.M22 + other.M34 * @this.M23 + other.M44 * @this.M24;
            M34 = other.M14 * @this.M31 + other.M24 * @this.M32 + other.M34 * @this.M33 + other.M44 * @this.M34;
            M44 = other.M14 * @this.M41 + other.M24 * @this.M42 + other.M34 * @this.M43 + other.M44 * @this.M44;
        }

        internal void Postmultiply(AbstractMatrix other)
        {
            AbstractMatrix @this = this;

            M11 = @this.M11 * other.M11 + @this.M21 * other.M12 + @this.M31 * other.M13 + @this.M41 * other.M14;
            M21 = @this.M11 * other.M21 + @this.M21 * other.M22 + @this.M31 * other.M23 + @this.M41 * other.M24;
            M31 = @this.M11 * other.M31 + @this.M21 * other.M32 + @this.M31 * other.M33 + @this.M41 * other.M34;
            M41 = @this.M11 * other.M41 + @this.M21 * other.M42 + @this.M31 * other.M43 + @this.M41 * other.M44;

            M12 = @this.M12 * other.M11 + @this.M22 * other.M12 + @this.M32 * other.M13 + @this.M42 * other.M14;
            M22 = @this.M12 * other.M21 + @this.M22 * other.M22 + @this.M32 * other.M23 + @this.M42 * other.M24;
            M32 = @this.M12 * other.M31 + @this.M22 * other.M32 + @this.M32 * other.M33 + @this.M42 * other.M34;
            M42 = @this.M12 * other.M41 + @this.M22 * other.M42 + @this.M32 * other.M43 + @this.M42 * other.M44;

            M13 = @this.M13 * other.M11 + @this.M23 * other.M12 + @this.M33 * other.M13 + @this.M43 * other.M14;
            M23 = @this.M13 * other.M21 + @this.M23 * other.M22 + @this.M33 * other.M23 + @this.M43 * other.M24;
            M33 = @this.M13 * other.M31 + @this.M23 * other.M32 + @this.M33 * other.M33 + @this.M43 * other.M34;
            M43 = @this.M13 * other.M41 + @this.M23 * other.M42 + @this.M33 * other.M43 + @this.M43 * other.M44;

            M14 = @this.M14 * other.M11 + @this.M24 * other.M12 + @this.M34 * other.M13 + @this.M44 * other.M14;
            M24 = @this.M14 * other.M21 + @this.M24 * other.M22 + @this.M34 * other.M23 + @this.M44 * other.M24;
            M34 = @this.M14 * other.M31 + @this.M24 * other.M32 + @this.M34 * other.M33 + @this.M44 * other.M34;
            M44 = @this.M14 * other.M41 + @this.M24 * other.M42 + @this.M34 * other.M43 + @this.M44 * other.M44;
        }

        internal void Translate(double tx, double ty, double tz)
        {
            var translation = new AbstractMatrix(1, 0, 0, tx, 0, 1, 0, ty, 0, 0, 1, tz, 0, 0, 0, 1);
            Postmultiply(translation);
        }

        internal void Scale(double scaleX, double scaleY, double scaleZ, double originX, double originY, double originZ)
        {
            Translate(originX, originY, originZ);
            var scale = new AbstractMatrix(scaleX, 0, 0, 0, 0, scaleY, 0, 0, 0, 0, scaleZ, 0, 0, 0, 0, 1);
            Postmultiply(scale);
            Translate(-originX, -originY, -originZ);
        }

        internal void RotateAxisAngle(double x, double y, double z, double alpha)
        {
            double sc = Math.Sin(alpha / 2.0) * Math.Cos(alpha / 2.0);

            double sin = Math.Sin(alpha / 2.0);
            double sq = sin * sin;

            AbstractMatrix rotation = Identity;

            rotation.M11 = 1.0 - 2.0 * (y * y + z * z) * sq;
            rotation.M21 = 2.0 * (x * y * sq - z * sc);
            rotation.M31 = 2.0 * (x * z * sq + y * sc);
            rotation.M41 = 0.0;

            rotation.M12 = 2.0 * (x * y * sq + z * sc);
            rotation.M22 = 1.0 - 2.0 * (x * x + z * z) * sq;
            rotation.M32 = 2.0 * (y * z * sq - x * sc);
            rotation.M42 = 0.0;

            rotation.M13 = 2.0 * (x * z * sq - y * sc);
            rotation.M23 = 2.0 * (y * z * sq + x * sc);
            rotation.M33 = 1.0 - 2.0 * (x * x + y * y) * sq;
            rotation.M43 = 0.0;

            rotation.M14 = 0.0;
            rotation.M24 = 0.0;
            rotation.M34 = 0.0;
            rotation.M44 = 1.0;

            Postmultiply(rotation);
        }

        internal void SkewX(double alpha)
        {
            var skew = new AbstractMatrix(1, Math.Tan(alpha), 0, 0, 1, 0);
            Postmultiply(skew);
        }

        internal void SkewY(double alpha)
        {
            var skew = new AbstractMatrix(1, 0, 0, Math.Tan(alpha), 1, 0);
            Postmultiply(skew);
        }

        internal readonly double GetDeterminant()
        {
            return M11 * (M22 * M33 * M44 - M42 * M33 * M24)
                + M21 * (M32 * M43 * M14 - M12 * M43 * M34)
                + M31 * (M42 * M13 * M24 - M22 * M13 * M44)
                + M41 * (M12 * M23 * M34 - M32 * M23 * M14);
        }

        internal bool Invert()
        {
            double[] r1 = [M11, M21, M31, M41, 1, 0, 0, 0];
            double[] r2 = [M12, M22, M32, M42, 0, 1, 0, 0];
            double[] r3 = [M13, M23, M33, M43, 0, 0, 1, 0];
            double[] r4 = [M14, M24, M34, M44, 0, 0, 0, 1];

            double[][] matrix = [r1, r2, r3, r4];

            double v;
            for (int r = 0; r < 4; r++)
            {
                v = matrix[r][r];
                if (MathsUtils.IsZero(v))
                {
                    goto non_invertible;
                }

                for (int l = 0; l < 8; l++)
                {
                    matrix[r][l] /= v;
                }

                for (int s = 0; s < 4; s++)
                {
                    if (s == r)
                    {
                        continue;
                    }

                    v = matrix[s][r];
                    for (int l = 0; l < 8; l++)
                    {
                        matrix[s][l] -= v * matrix[r][l];
                    }
                }
            }

            M11 = r1[4];
            M21 = r1[5];
            M31 = r1[6];
            M41 = r1[7];

            M12 = r2[4];
            M22 = r2[5];
            M32 = r2[6];
            M42 = r2[7];

            M13 = r3[4];
            M23 = r3[5];
            M33 = r3[6];
            M43 = r3[7];

            M14 = r4[4];
            M24 = r4[5];
            M34 = r4[6];
            M44 = r4[7];

            return true;
        non_invertible:
            M11 = M21 = M31 = M41 = double.NaN;
            M12 = M22 = M32 = M42 = double.NaN;
            M13 = M23 = M33 = M43 = double.NaN;
            M14 = M24 = M34 = M44 = double.NaN;
            return false;
        }

    }
}