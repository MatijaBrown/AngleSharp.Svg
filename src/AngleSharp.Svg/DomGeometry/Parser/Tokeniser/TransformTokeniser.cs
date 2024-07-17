using AngleSharp.Common;
using AngleSharp.Text;

namespace AngleSharp.DomGeometry.Parser.Tokeniser
{
    internal class TransformTokeniser(TextSource source) : BaseTokenizer(source)
    {

        private readonly TextSource _source = source;

        private TextPosition _currentPosition;

        internal TransformToken? CurrentToken { get; private set; } = null;

        internal TransformToken Next()
        {
            if (GetCurrentPosition().Position >= _source.Length)
            {
                return EOFToken();
            }
            char current = GetNext();
            _currentPosition = GetCurrentPosition();
            return (CurrentToken = Tokenise(current));
        }

        private TransformToken WhitespaceToken(char c)
        {
            return new TransformToken(TransformTokenType.Whitespace, c.ToString(), _currentPosition);
        }

        private TransformToken OpenParenthesesToken()
        {
            return new TransformToken(TransformTokenType.OpenParentheses, "(", _currentPosition);
        }

        private TransformToken CloseParenthesesToken()
        {
            return new TransformToken(TransformTokenType.CloseParentheses, ")", _currentPosition);
        }

        private TransformToken CommaToken()
        {
            return new TransformToken(TransformTokenType.Comma, ",", _currentPosition);
        }

        private TransformToken NumberToken(string data)
        {
            return new TransformToken(TransformTokenType.Number, data, _currentPosition);
        }

        private TransformToken PercentageToken(string data)
        {
            return new TransformToken(TransformTokenType.Percentage, data, _currentPosition);
        }

        private TransformToken DimensionToken(string data)
        {
            return new TransformToken(TransformTokenType.Dimension, data, _currentPosition);
        }

        private TransformToken FunctionToken(string data)
        {
            return new TransformToken(TransformTokenType.Function, data, _currentPosition);
        }

        private TransformToken EOFToken()
        {
            return new TransformToken(TransformTokenType.EOF, Symbols.EndOfFile.ToString(), _currentPosition);
        }

        private TransformToken InvalidToken(string data)
        {
            return new TransformToken(TransformTokenType.Invalid, data, _currentPosition);
        }

        private TransformToken Tokenise(char current)
        {
            switch (current)
            {
                case Symbols.FormFeed:
                case Symbols.LineFeed:
                case Symbols.CarriageReturn:
                case Symbols.Tab:
                case Symbols.Space:
                    return WhitespaceToken(current);
                case Symbols.RoundBracketOpen:
                    return OpenParenthesesToken();
                case Symbols.RoundBracketClose:
                    return CloseParenthesesToken();
                case Symbols.Plus:
                case Symbols.Minus:
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return Number(current);
                case Symbols.Comma:
                    return CommaToken();
                case Symbols.Dot:
                    return Number(GetPrevious());
                case Symbols.EndOfFile:
                    return EOFToken();
                default:
                    if (!current.IsNameStart())
                    {
                        return InvalidToken(current.ToString());
                    }
                    return Func(current);
            }
        }

        private TransformToken Func(char current)
        {
            StringBuffer.Append(current);

            while (true)
            {
                current = GetNext();
                if (current.IsName())
                {
                    StringBuffer.Append(current);
                }
                else if (current == Symbols.RoundBracketOpen)
                {
                    string name = FlushBuffer();
                    return FunctionToken(name);
                }
                else if (IsWhitespace(current))
                {
                    continue;
                }
                else
                {
                    StringBuffer.Append(current);
                    return InvalidToken(FlushBuffer());
                }
            }
        }

        private TransformToken Number(char current)
        {
            while (true)
            {
                if ((current == Symbols.Plus) || (current == Symbols.Minus))
                {
                    StringBuffer.Append(current);
                    current = GetNext();

                    if (current == Symbols.Dot)
                    {
                        StringBuffer.Append(current);
                        StringBuffer.Append(GetNext());
                        return DecimalPlaces();
                    }

                    StringBuffer.Append(current);
                    return NumberRest();
                }
                else if (current == Symbols.Dot)
                {
                    StringBuffer.Append(current);
                    StringBuffer.Append(GetNext());
                    return DecimalPlaces();
                }
                else if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                    return NumberRest();
                }

                current = GetNext();
            }
        }

        private TransformToken NumberRest()
        {
            char current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if ((current != 'e' && (current != 'E') && current.IsNameStart()))
                {
                    StringBuffer.Append(current);
                    return Dimension();
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = GetNext();

                    if (current.IsDigit())
                    {
                        StringBuffer.Append(Symbols.Dot).Append(current);
                        return DecimalPlaces();
                    }

                    Back();
                    return NumberToken(FlushBuffer());
                case Symbols.Percent:
                    StringBuffer.Append(Symbols.Percent);
                    return PercentageToken(FlushBuffer());
                case 'e':
                case 'E':
                    StringBuffer.Append(current);
                    return NumberExponential();
                case Symbols.Minus:
                    return NumberDash();
                default:
                    Back();
                    return NumberToken(FlushBuffer());
            }
        }

        private TransformToken DecimalPlaces()
        {
            char current = GetNext();

            while (true)
            {
                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if ((current != 'e') && (current != 'E') && current.IsNameStart())
                {
                    StringBuffer.Append(current);
                    return Dimension();
                }
                else
                {
                    break;
                }

                current = GetNext();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    StringBuffer.Append(current);
                    return NumberExponential();
                case Symbols.Percent:
                    StringBuffer.Append(current);
                    return PercentageToken(FlushBuffer());
                case Symbols.Minus:
                    return NumberDash();
                default:
                    Back();
                    return NumberToken(FlushBuffer());
            }
        }

        private TransformToken Dimension()
        {
            while (true)
            {
                char current = GetNext();

                if (current.IsLetter())
                {
                    StringBuffer.Append(current);
                }
                else
                {
                    Back();
                    return DimensionToken(FlushBuffer());
                }
            }
        }

        private TransformToken SciNotation()
        {
            while (true)
            {
                char current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(current);
                }
                else if (current.IsNameStart())
                {
                    StringBuffer.Append(current);
                    return Dimension();
                }
                else
                {
                    Back();
                    return NumberToken(FlushBuffer());
                }
            }
        }

        private TransformToken NumberExponential()
        {
            char current = GetNext();

            if (current.IsDigit())
            {
                StringBuffer.Append(current);
                return SciNotation();
            }
            else if ((current == Symbols.Plus) || (current == Symbols.Minus))
            {
                char op = current;
                current = GetNext();

                if (current.IsDigit())
                {
                    StringBuffer.Append(op).Append(current);
                    return SciNotation();
                }

                Back();
            }

            Back();
            return Dimension();
        }

        private TransformToken NumberDash()
        {
            char current = GetNext();

            if (current.IsNameStart())
            {
                StringBuffer.Append(Symbols.Minus).Append(current);
                return Dimension();
            }
            else
            {
                Back(2);
                return NumberToken(FlushBuffer());
            }
        }

        private static bool IsWhitespace(char current)
        {
            return (current == Symbols.FormFeed) || (current == Symbols.LineFeed) || (current == Symbols.CarriageReturn)
                || (current == Symbols.Tab) || (current == Symbols.Space);
        }

    }
}
