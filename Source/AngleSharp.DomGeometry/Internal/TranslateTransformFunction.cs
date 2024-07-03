using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal class TranslateTransformFunction(double x, double y, double z) : ITransformFunction
    {

        public TransformFunctionType FunctionType => TransformFunctionType.Translate;

        public bool Is2DTransform => z == 0.0;

        public Matrix4X4<double> Abstract4x4Matrix => Matrix4X4.CreateTranslation(x, y, z);

    }
}
