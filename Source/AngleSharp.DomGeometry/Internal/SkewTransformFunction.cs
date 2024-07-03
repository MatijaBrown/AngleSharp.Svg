using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal class SkewTransformFunction(double alpha, double beta) : ITransformFunction
    {

        public TransformFunctionType FunctionType => TransformFunctionType.Skew;

        public bool Is2DTransform => true;

        public Matrix4X4<double> Abstract4x4Matrix => new(
            1, Math.Tan(alpha), 0, 0,
            Math.Tan(beta), 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );

    }
}
