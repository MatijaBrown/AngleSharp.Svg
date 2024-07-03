using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal interface ITransformFunction
    {

        public TransformFunctionType FunctionType { get; }

        public Matrix4X4<double> Abstract4x4Matrix { get; }

        public bool Is2DTransform { get; }

    }

    internal enum TransformFunctionType
    {
        Translate,
        Scale,
        Rotate,
        Skew,
        Matrix
    }
}
