using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class SkewXFunction(double angle) : ITransformFunction
    {

        public bool Is2D => true;

        public AbstractMatrix ToMatrix()
        {
            double radians = angle / 180.0 * Math.PI;
            return new AbstractMatrix(1, Math.Tan(radians), 0, 0, 1, 0);
        }

    }
}
