using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal class ScaleTransformFunction(double sx, double sy, double sz) : ITransformFunction
    {

        public TransformFunctionType FunctionType => TransformFunctionType.Scale;

        public bool Is2DTransform => sz == 0.0;

        public Matrix4X4<double> Abstract4x4Matrix => Matrix4X4.CreateScale(sx, sy, sz);

    }
}
