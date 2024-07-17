using AngleSharp.DomGeometry.Dom;

namespace AngleSharp.DomGeometry.Parser.Functions
{
    internal interface ITransformFunction
    {

        bool Is2D { get; }

        AbstractMatrix ToMatrix();

    }
}
