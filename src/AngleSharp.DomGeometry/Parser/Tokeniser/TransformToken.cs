using AngleSharp.Text;

namespace AngleSharp.DomGeometry.Parser.Tokeniser
{
    internal class TransformToken(TransformTokenType type, string data, TextPosition position)
    {

        internal TransformTokenType Type { get; } = type;

        internal string Data { get; } = data;

        internal TextPosition Position { get; } = position;

    }
}
