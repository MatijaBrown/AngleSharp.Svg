using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class RotateFunction(double rx, double ry, double rz) : ITransformFunction
    {

        public bool Is2D => MathsUtils.IsZero(rx) && MathsUtils.IsZero(ry);

        public AbstractMatrix ToMatrix()
        {
            var mat = AbstractMatrix.Identity;
            mat.RotateAxisAngle(0, 0, 1, rz);
            mat.RotateAxisAngle(0, 1, 0, ry);
            mat.RotateAxisAngle(1, 0, 0, rx);
            return mat;
        }

    }
}
