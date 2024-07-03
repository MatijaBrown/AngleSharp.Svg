using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal class MatrixTransformFunction(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24,
        double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44) : ITransformFunction
    {

        public TransformFunctionType FunctionType => TransformFunctionType.Matrix;

        public bool Is2DTransform => (m13 == 0.0) && (m14 == 0.0) && (m23 == 0.0) && (m24 == 0.0) &&
            (m31 == 0.0) && (m32 == 0.0) && (m34 == 0.0) && (m43 == 0.0) && (m33 == 1.0) && (m44 == 1.0);

        public Matrix4X4<double> Abstract4x4Matrix => new(
            m11, m12, m13, m14,
            m21, m22, m23, m24,
            m31, m32, m33, m34,
            m41, m42, m43, m44
        );

    }
}
