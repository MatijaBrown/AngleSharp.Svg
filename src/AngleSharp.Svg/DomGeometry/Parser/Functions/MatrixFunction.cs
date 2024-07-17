using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class MatrixFunction(double a, double b, double c, double d, double e, double f) : ITransformFunction
    {

        public bool Is2D => true;

        public AbstractMatrix ToMatrix()
        {
            return new AbstractMatrix(a, c, e, b, d, f);
        }

    }
}
