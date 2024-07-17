using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.DomGeometry.Dom
{
    public interface IDomMatrix
    {

        static IDomMatrix FromMatrix(double m11, double m21, double m31, double m41, double m12, double m22, double m32, double m42,
            double m13, double m23, double m33, double m43, double m14, double m24, double m34, double m44)
        {
            return null;
        }

        static IDomMatrix FromFloat32Array(float[] array32)
        {
            return null;
        }

        static IDomMatrix FromFloat64Array(double[] array64)
        {
            return null;
        }

        public double A { get; set; }

        public double B { get; set; }

        public double C { get; set; }

        public double D { get; set; }

        public double E { get; set; }

        public double F { get; set; }

        public double M11 { get; set; }

        public double M12 { get; set; }

        public double M13 { get; set; }

        public double M14 { get; set; }

        public double M21 { get; set; }

        public double M22 { get; set; }

        public double M23 { get; set; }

        public double M24 { get; set; }

        public double M31 { get; set; }

        public double M32 { get; set; }

        public double M33 { get; set; }

        public double M34 { get; set; }

        public double M41 { get; set; }

        public double M42 { get; set; }

        public double M43 { get; set; }

        public double M44 { get; set; }

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

        string ToJson();

        IDomMatrix MultiplySelf(IDomMatrixReadOnly other);

        IDomMatrix MultiplySelf(IDomMatrix other);

        IDomMatrix PreMultiplySelf(IDomMatrixReadOnly other);

        IDomMatrix PreMultiplySelf(IDomMatrix other);

        IDomMatrix TranslateSelf(double tx = 0, double ty = 0, double tz = 0);

        IDomMatrix ScaleSef(double scaleX = 1, double? scaleY = null, double scaleZ = 1, double originX = 0, double originY = 0, double originZ = 0);

        IDomMatrix Scale3DSelf(double scale = 1, double originX = 0, double originY = 0, double originZ = 0);

        IDomMatrix RotateSelf(double rotX = 0, double? rotY = null, double? rotZ = null);

        IDomMatrix RotateFromVectorSelf(double x = 0, double y = 0);

        IDomMatrix RotateAxisAngleSelf(double x = 0, double y = 0, double z = 0, double angle = 0);

        IDomMatrix SkewXSelf(double sx = 0);

        IDomMatrix SkewYSelf(double sy = 0);

        IDomMatrix InvertSelf();

    }
}
