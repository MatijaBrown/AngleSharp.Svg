using AngleSharp.DomGeometry.Parser.Tokeniser;
using AngleSharp.Text;
using AngleSharp.DomGeometry.Parser.Functions;
using AngleSharp.Dom;
using System.Globalization;

namespace AngleSharp.DomGeometry.Parser
{
    internal static class TransformParser
    {

        private static readonly IDictionary<string, Func<TransformTokeniser, ITransformFunction>> SvgTransformFunctions
            = new Dictionary<string, Func<TransformTokeniser, ITransformFunction>>(StringComparer.OrdinalIgnoreCase)
        {
            { TransformFunctionNames.TranslateFunction, ParseSvgTranslate },
            { TransformFunctionNames.ScaleFunction, ParseSvgScale },
            { TransformFunctionNames.RotateFunction, ParseSvgRotate },
            { TransformFunctionNames.SkewXFunction, ParseSvgSkewX },
            { TransformFunctionNames.SkewYFunction, ParseSvgSkewY },
            { TransformFunctionNames.MatrixFunction, ParseSvgMatrix }
        };

        private static TransformToken ReadUntil(TransformTokeniser tokeniser, params TransformTokenType[] tokenTypes)
        {
            TransformToken token;
            while (!tokenTypes.Contains((token = tokeniser.Next()).Type) && (token.Type != TransformTokenType.EOF));
            return token;
        }

        private static void EnsureTokenType(TransformToken token, params TransformTokenType[] tokenTypes)
        {
            if (!tokenTypes.Contains(token.Type))
            {
                throw new DomException(DomError.Syntax);
            }
        }

        private static TransformToken SkipWhitespace(TransformTokeniser tokeniser)
        {
            TransformToken next;
            while ((next = tokeniser.Next()).Type == TransformTokenType.Whitespace) ;
            return next;
        }

        private static bool ParseCommaWsp(TransformTokeniser tokeniser, out TransformToken next)
        {
            uint count = 0;
            while (((next = tokeniser.Next()).Type == TransformTokenType.Whitespace) || (next.Type == TransformTokenType.Comma))
            {
                if (next.Type == TransformTokenType.Comma)
                {
                    next = SkipWhitespace(tokeniser);
                    return true;
                }
                count++;
            }
            return count > 0;
        }

        private static bool NewParamOrCloseParen(TransformTokeniser tokeniser, out TransformToken next)
        {
            return ParseCommaWsp(tokeniser, out next) || (next.Type == TransformTokenType.CloseParentheses);
        }

        private static double ParseDouble(string str)
        {
            try
            {
                return double.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new DomException(DomError.TypeMismatch);
            }
        }

        private static ITransformFunction ParseSvgTranslate(TransformTokeniser tokeniser)
        {
            var firstCoord = ReadUntil(tokeniser, TransformTokenType.Number);
            EnsureTokenType(firstCoord, TransformTokenType.Number);

            if (!NewParamOrCloseParen(tokeniser, out TransformToken next))
            {
                throw new DomException(DomError.Syntax);
            }
            EnsureTokenType(next, TransformTokenType.Number, TransformTokenType.CloseParentheses);

            var secondCoord = (next.Type == TransformTokenType.Number) ? next : new TransformToken(TransformTokenType.Number, "0", TextPosition.Empty);

            if (next.Type != TransformTokenType.CloseParentheses)
            {
                // Make sure the () are actuall closed at the end of the function.
                EnsureTokenType(SkipWhitespace(tokeniser), TransformTokenType.CloseParentheses);
            }

            double x = ParseDouble(firstCoord.Data);
            double y = ParseDouble(secondCoord.Data);
            return new TranslateFunction(x, y);
        }

        private static ITransformFunction ParseSvgScale(TransformTokeniser tokeniser)
        {
            var firstScale = ReadUntil(tokeniser, TransformTokenType.Number);
            EnsureTokenType(firstScale, TransformTokenType.Number);

            if (!NewParamOrCloseParen(tokeniser, out TransformToken next))
            {
                throw new DomException(DomError.Syntax);
            }
            EnsureTokenType(next, TransformTokenType.Number, TransformTokenType.CloseParentheses);

            var secondScale = (next.Type == TransformTokenType.Number) ? next
                : new TransformToken(TransformTokenType.Number, firstScale.Data, TextPosition.Empty);

            if (next.Type != TransformTokenType.CloseParentheses)
            {
                // Make sure the () are actuall closed at the end of the function.
                EnsureTokenType(SkipWhitespace(tokeniser), TransformTokenType.CloseParentheses);
            }

            double x = ParseDouble(firstScale.Data);
            double y = ParseDouble(secondScale.Data);
            return new ScaleFunction(x, y);
        }

        private static ITransformFunction ParseSvgRotate(TransformTokeniser tokeniser)
        {
            var firstRot = ReadUntil(tokeniser, TransformTokenType.Number);
            EnsureTokenType(firstRot, TransformTokenType.Number);

            if (!NewParamOrCloseParen(tokeniser, out TransformToken next))
            {
                throw new DomException(DomError.Syntax);
            }

            double rx = ParseDouble(firstRot.Data);
            if (next.Type == TransformTokenType.CloseParentheses)
            {
                return new RotateFunction(0, 0, rx);
            }
            else
            {
                var secondRot = next;
                EnsureTokenType(secondRot, TransformTokenType.Number);

                if (!ParseCommaWsp(tokeniser, out next) || (next.Type != TransformTokenType.Number))
                {
                    throw new DomException(DomError.Syntax);
                }

                var thirdRot = next;

                EnsureTokenType(SkipWhitespace(tokeniser), TransformTokenType.CloseParentheses);

                double ry = ParseDouble(secondRot.Data);
                double rz = ParseDouble(thirdRot.Data);
                return new RotateFunction(rx, ry, rz);
            }
        }

        private static ITransformFunction ParseSvgSkewX(TransformTokeniser tokeniser)
        {
            var skewAngle = ReadUntil(tokeniser, TransformTokenType.Number);
            EnsureTokenType(skewAngle, TransformTokenType.Number);
            EnsureTokenType(tokeniser.Next(), TransformTokenType.CloseParentheses);
            return new SkewXFunction(ParseDouble(skewAngle.Data));
        }

        private static ITransformFunction ParseSvgSkewY(TransformTokeniser tokeniser)
        {
            var skewAngle = ReadUntil(tokeniser, TransformTokenType.Number);
            EnsureTokenType(skewAngle, TransformTokenType.Number);
            EnsureTokenType(tokeniser.Next(), TransformTokenType.CloseParentheses);
            return new SkewYFunction(ParseDouble(skewAngle.Data));
        }

        private static ITransformFunction ParseSvgMatrix(TransformTokeniser tokeniser)
        {
            var parameters = new TransformToken[6];
            parameters[0] = tokeniser.Next();
            EnsureTokenType(parameters[0], TransformTokenType.Number);
            for (int i = 0; i < 5; i++)
            {
                if (!ParseCommaWsp(tokeniser, out TransformToken next))
                {
                    throw new DomException(DomError.Syntax);
                }
                parameters[1 + i] = next;
                EnsureTokenType(parameters[1 + i], TransformTokenType.Number);
            }

            EnsureTokenType(tokeniser.Next(), TransformTokenType.CloseParentheses);

            var parsedParameters = new double[6];
            for (int i = 0; i < 6; i++)
            {
                parsedParameters[i] = ParseDouble(parameters[i].Data);
            }
            return new MatrixFunction(parsedParameters[0], parsedParameters[1], parsedParameters[2], parsedParameters[3], parsedParameters[4], parsedParameters[5]);
        }

        internal static IList<ITransformFunction> ParseSvgTransform(string transformString)
        {
            var functionList = new List<ITransformFunction>();
            var tokeniser = new TransformTokeniser(new TextSource(transformString));
            TransformToken token = SkipWhitespace(tokeniser);
            while (token.Type != TransformTokenType.EOF)
            {
                if (token.Type == TransformTokenType.Function)
                {
                    string funcName = token.Data;
                    if (!SvgTransformFunctions.TryGetValue(funcName, out var func))
                    {
                        throw new DomException(DomError.Syntax);
                    }
                    functionList.Add(func(tokeniser));
                    if (!ParseCommaWsp(tokeniser, out token) && (token.Type != TransformTokenType.EOF))
                    {
                        throw new DomException(DomError.Syntax);
                    }
                }
                else
                {
                    throw new DomException(DomError.Syntax);
                }
            }
            return functionList;
        }

    }
}
