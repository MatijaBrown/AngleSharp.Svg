using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class TranslateFunction(double x, double y) : ITransformFunction
    {

        public bool Is2D => true;

        public AbstractMatrix ToMatrix()
        {
            return new AbstractMatrix(1, 0, x, 0, 1, y);
        }

    }
}
