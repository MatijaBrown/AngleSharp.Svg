using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal sealed class ScaleFunction(double scaleX, double scaleY) : ITransformFunction
    {

        public bool Is2D => true;

        public AbstractMatrix ToMatrix()
        {
            return new AbstractMatrix(scaleX, 0, 0, 0, scaleY, 0);
        }

    }
}
