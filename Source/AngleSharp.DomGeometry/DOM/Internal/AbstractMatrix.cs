namespace AngleSharp.DomGeometry.DOM.Internal
{
    /**
     * NOTE: AbstractMatrix, conforming to the DomGeometry-spec (https://www.w3.org/TR/geometry-1/#DOMMatrix)
     *  is named in COLUMN MAJOR order! I.e. M[Column][Row] not M[Row][Column] as is usual.
     */
    internal struct AbstractMatrix(double m11, double m21, double m31, double m41, double m12, double m22, double m32, double m42,
        double m13, double m23, double m33, double m43, double m14, double m24, double m34, double m44)
    {

        internal readonly static AbstractMatrix Identity = new(1, 0, 0, 1, 0, 0);

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
            M11 = other.M11 * M11 + other.M21 * M12 + other.M31 * M13 + other.M41 * M14;
            M21 = other.M11 * M21 + other.M21 * M22 + other.M31 * M23 + other.M41 * M24;
            M31 = other.M11 * M31 + other.M21 * M32 + other.M31 * M33 + other.M41 * M34;
            M41 = other.M11 * M41 + other.M21 * M42 + other.M31 * M43 + other.M41 * M44;

            M12 = other.M12 * M11 + other.M22 * M12 + other.M32 * M13 + other.M42 * M14;
            M22 = other.M12 * M21 + other.M22 * M22 + other.M32 * M23 + other.M42 * M24;
            M32 = other.M12 * M31 + other.M22 * M32 + other.M32 * M33 + other.M42 * M34;
            M42 = other.M12 * M41 + other.M22 * M42 + other.M32 * M43 + other.M42 * M44;
            
            M13 = other.M13 * M11 + other.M23 * M12 + other.M33 * M13 + other.M43 * M14;
            M23 = other.M13 * M21 + other.M23 * M22 + other.M33 * M23 + other.M43 * M24;
            M33 = other.M13 * M31 + other.M23 * M32 + other.M33 * M33 + other.M43 * M34;
            M43 = other.M13 * M41 + other.M23 * M42 + other.M33 * M43 + other.M43 * M44;

            M14 = other.M14 * M11 + other.M24 * M12 + other.M34 * M13 + other.M44 * M14;
            M24 = other.M14 * M21 + other.M24 * M22 + other.M34 * M23 + other.M44 * M24;
            M34 = other.M14 * M31 + other.M24 * M32 + other.M34 * M33 + other.M44 * M34;
            M44 = other.M14 * M41 + other.M24 * M42 + other.M34 * M43 + other.M44 * M44;
        }

        internal void Postmultiply(AbstractMatrix other)
        {
            M11 = M11 * other.M11 + M21 * other.M12 + M31 * other.M13 + M41 * other.M14;
            M21 = M11 * other.M21 + M21 * other.M22 + M31 * other.M23 + M41 * other.M24;
            M31 = M11 * other.M31 + M21 * other.M32 + M31 * other.M33 + M41 * other.M34;
            M41 = M11 * other.M41 + M21 * other.M42 + M31 * other.M43 + M41 * other.M44;

            M12 = M12 * other.M11 + M22 * other.M12 + M32 * other.M13 + M42 * other.M14;
            M22 = M12 * other.M21 + M22 * other.M22 + M32 * other.M23 + M42 * other.M24;
            M32 = M12 * other.M31 + M22 * other.M32 + M32 * other.M33 + M42 * other.M34;
            M42 = M12 * other.M41 + M22 * other.M42 + M32 * other.M43 + M42 * other.M44;

            M13 = M13 * other.M11 + M23 * other.M12 + M33 * other.M13 + M43 * other.M14;
            M23 = M13 * other.M21 + M23 * other.M22 + M33 * other.M23 + M43 * other.M24;
            M33 = M13 * other.M31 + M23 * other.M32 + M33 * other.M33 + M43 * other.M34;
            M43 = M13 * other.M41 + M23 * other.M42 + M33 * other.M43 + M43 * other.M44;

            M14 = M14 * other.M11 + M24 * other.M12 + M34 * other.M13 + M44 * other.M14;
            M24 = M14 * other.M21 + M24 * other.M22 + M34 * other.M23 + M44 * other.M24;
            M34 = M14 * other.M31 + M24 * other.M32 + M34 * other.M33 + M44 * other.M34;
            M44 = M14 * other.M41 + M24 * other.M42 + M34 * other.M43 + M44 * other.M44;
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

        internal void RotateAxisAngle(double x, double y, double z, double angle)
        {
            double sa = Math.Sin(angle), ca = Math.Cos(angle);
            double xx = x * x, yy = y * y, zz = z * z;
            double xy = x * y, xz = x * z, yz = y * z;

            AbstractMatrix rotation = Identity;

            rotation.M11 = xx + (ca * (1 - xx));
            rotation.M21 = xy - (ca * xy) + (sa * z);
            rotation.M31 = xz - (ca * xz) - (sa * y);

            rotation.M12 = xy - (ca * xy) - (sa * z);
            rotation.M22 = yy + (ca * (1 - yy));
            rotation.M32 = yz - (ca * yz) + (sa * x);

            rotation.M13 = xz - (ca * xz) + (sa * y);
            rotation.M23 = yz - (ca * yz) - (sa * x);
            rotation.M33 = zz + (ca * (1 - zz));

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

        internal void Invert()
        {
            double[] r1 = [ M11, M21, M31, M41, 1, 0, 0, 0 ];
            double[] r2 = [ M12, M22, M32, M42, 0, 1, 0, 0 ];
            double[] r3 = [ M13, M23, M33, M43, 0, 0, 1, 0 ];
            double[] r4 = [ M14, M24, M34, M44, 0, 0, 0, 1 ];

            double[][] matrix = [ r1, r2, r3, r4 ];

            double v;
            for (int r = 0; r < 4; r++)
            {
                v = matrix[r][r];
                if (Maths.IsZero(v))
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

            return;
        non_invertible:
            M11 = M21 = M31 = M41 = double.NaN;
            M12 = M22 = M32 = M42 = double.NaN;
            M13 = M23 = M33 = M43 = double.NaN;
            M14 = M24 = M34 = M44 = double.NaN;
            return;
        }

    }
}
