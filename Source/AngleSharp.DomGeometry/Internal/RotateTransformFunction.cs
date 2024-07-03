using Silk.NET.Maths;

namespace AngleSharp.DomGeometry.Internal
{
    internal class RotateTransformFunction(double x, double y, double z, double alpha) : ITransformFunction
    {

        public TransformFunctionType FunctionType => TransformFunctionType.Rotate;

        public bool Is2DTransform => (x == 0.0) && (y == 0.0) && (z == 0.0);

        public Matrix4X4<double> Abstract4x4Matrix
        {
            get
            {
                if (Is2DTransform)
                {
                    return Matrix4X4.CreateRotationZ(alpha);
                }
                else
                {
                    return Matrix4X4.CreateFromAxisAngle(new Vector3D<double>(x, y, z), alpha);
                }
            }
        }

    }
}
