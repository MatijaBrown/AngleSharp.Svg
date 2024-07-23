using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class SkewYFunction(double angle) : ITransformFunction
    {

        public AbstractMatrix ToMatrix()
        {
            double radians = angle / 180.0 * Math.PI;
            return new AbstractMatrix(1, 0, 0, Math.Tan(radians), 1, 0);
        }

    }
}
