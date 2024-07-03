using AngleSharp.Css.Dom;
using AngleSharp.Css.Values;
using AngleSharp.Css.Parser;
using AngleSharp.Text;

namespace AngleSharp.DomGeometry.Internal
{
    internal static class TransformParser
    {
        private static readonly Dictionary<string, Func<StringSource, ITransformFunction?>> TransformFunctions = new(StringComparer.OrdinalIgnoreCase)
        {
            { "skew", ParseSkew2d },
            { "skewX", ParseSkewX },
            { "skewY", ParseSkewY },
            { "matrix", ParseMatrix2d },
            { "matrix3d", ParseMatrix3d },
            { "rotate", ParseRotate2d },
            { "rotate3d", ParseRotate3d },
            { "rotateX", ParseRotateX },
            { "rotateY", ParseRotateY },
            { "rotateZ", ParseRotateZ },
            { "scale", ParseScale2d },
            { "scale3d", ParseScale3d },
            { "scaleX", ParseScaleX },
            { "scaleY", ParseScaleY },
            { "scaleZ", ParseScaleZ },
            { "translate", ParseTranslate2d },
            { "translate3d", ParseTranslate3d },
            { "translateX", ParseTranslateX },
            { "translateY", ParseTranslateY },
            { "translateZ", ParseTranslateZ }
        };

        public static IList<ITransformFunction?> ParseTransform(StringSource source)
        {
            var transformFunctions = new List<ITransformFunction?>
            {
                // Add Identity matrix so that the transform list is never empty.
                new MatrixTransformFunction(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1)
            };
            var pos = source.Index;

            string? ident;
            while ((ident = source.ParseIdent()) != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    if (TransformFunctions.TryGetValue(ident, out Func<StringSource, ITransformFunction?>? function))
                    {
                        source.SkipCurrentAndSpaces();
                        transformFunctions.Add(function.Invoke(source));
                    }
                    else
                    {
                        transformFunctions.Add(null);
                    }
                    source.SkipSpacesAndComments();
                }
            }

            source.BackTo(pos);
            return transformFunctions;
        }

        private static SkewTransformFunction? ParseSkew2d(StringSource source)
        {
            ICssValue x = source.ParseAngleOrCalc();
            ICssValue y = Angle.Zero;
            var c = source.SkipGetSkip();

            if (x == null)
            {
                return null;
            }

            if (c == Symbols.Comma)
            {
                y = source.ParseAngleOrCalc();
                c = source.SkipGetSkip();
            }

            if (c != Symbols.RoundBracketClose)
            {
                return null;
            }

            return new SkewTransformFunction(((Angle)x).ToRadian(), ((Angle)y).ToRadian());
        }

        private static SkewTransformFunction? ParseSkewX(StringSource source)
        {
            var x = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (x != null && f == Symbols.RoundBracketClose)
            {
                return new SkewTransformFunction(((Angle)x).ToRadian(), 0.0);
            }

            return null;
        }

        private static SkewTransformFunction? ParseSkewY(StringSource source)
        {
            var y = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (y != null && f == Symbols.RoundBracketClose)
            {
                return new SkewTransformFunction(0, ((Angle)y).ToRadian());
            }

            return null;
        }

        private static MatrixTransformFunction? ParseMatrix2d(StringSource source)
        {
            var numbers = new double[6];
            Array.Fill(numbers, 0);
            var num = source.ParseNumber();

            if (num.HasValue)
            {
                numbers[0] = num.Value;
                var index = 1;

                while (index < numbers.Length)
                {
                    var c = source.SkipGetSkip();
                    num = source.ParseNumber();

                    if (c != Symbols.Comma || !num.HasValue)
                        break;

                    numbers[index++] = num.Value;
                }

                var f = source.SkipGetSkip();

                if (index == 6 && f == Symbols.RoundBracketClose)
                {
                    return new MatrixTransformFunction(numbers[0], numbers[1], 0, numbers[4], numbers[2], numbers[3], 0, numbers[5], 0, 0, 1, 0, 0, 0, 0, 1);
                }
            }

            return null;
        }

        private static MatrixTransformFunction? ParseMatrix3d(StringSource source)
        {
            var numbers = new double[16];
            Array.Fill(numbers, 0);
            var num = source.ParseNumber();

            if (num.HasValue)
            {
                numbers[0] = num.Value;
                var index = 1;

                while (index < numbers.Length)
                {
                    var c = source.SkipGetSkip();
                    num = source.ParseNumber();

                    if (c != Symbols.Comma || !num.HasValue)
                        break;

                    numbers[index++] = num.Value;
                }

                var f = source.SkipGetSkip();

                if (index == 16 && f == Symbols.RoundBracketClose)
                {
                    return new MatrixTransformFunction(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5], numbers[6], numbers[7],
                        numbers[8], numbers[9], numbers[10], numbers[11], numbers[12], numbers[13], numbers[14], numbers[15]);
                }
            }

            return null;
        }

        private static RotateTransformFunction? ParseRotate2d(StringSource source)
        {
            return ParseRotate(source, 0.0, 0.0, 0.0);
        }

        private static RotateTransformFunction? ParseRotate3d(StringSource source)
        {
            var x = source.ParseNumber();
            var c1 = source.SkipGetSkip();
            var y = source.ParseNumber();
            var c2 = source.SkipGetSkip();
            var z = source.ParseNumber();
            var c3 = source.SkipGetSkip();

            if (x.HasValue && y.HasValue && z.HasValue && c1 == c2 && c1 == c3 && c1 == Symbols.Comma)
            {
                return ParseRotate(source, x.Value, y.Value, z.Value);
            }

            return null;
        }

        private static RotateTransformFunction? ParseRotateX(StringSource source)
        {
            return ParseRotate(source, 1.0, 0.0, 0.0);
        }

        private static RotateTransformFunction? ParseRotateY(StringSource source)
        {
            return ParseRotate(source, 0.0, 1.0, 0.0);
        }

        private static RotateTransformFunction? ParseRotateZ(StringSource source)
        {
            return ParseRotate(source, 0.0, 0.0, 1.0);
        }

        private static RotateTransformFunction? ParseRotate(StringSource source, double x, double y, double z)
        {
            var angle = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (angle != null && f == Symbols.RoundBracketClose)
            {
                return new RotateTransformFunction(x, y, z, ((Angle)angle).ToRadian());
            }

            return null;
        }

        private static ScaleTransformFunction? ParseScale2d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                double? y = null;

                if (f == Symbols.Comma)
                {
                    y = source.ParseNumber();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new ScaleTransformFunction(x.Value, y ?? x.Value, 0.0);
                }
            }

            return null;
        }

        private static ScaleTransformFunction? ParseScale3d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                double? y = null;
                double? z = null;

                if (f == Symbols.Comma)
                {
                    y = source.ParseNumber();
                    f = source.SkipGetSkip();

                    if (y is null)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseNumber();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new ScaleTransformFunction(x.Value, y ?? x.Value, z ?? x.Value);
                }
            }

            return null;
        }

        private static ScaleTransformFunction? ParseScaleX(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransformFunction(x.Value, 0.0, 0.0);
            }

            return null;
        }

        private static ScaleTransformFunction? ParseScaleY(StringSource source)
        {
            var y = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransformFunction(0.0, y.Value, 0.0);
            }

            return null;
        }

        private static ScaleTransformFunction? ParseScaleZ(StringSource source)
        {
            var z = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (z.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransformFunction(0.0, 0.0, z.Value);
            }

            return null;
        }

        private static TranslateTransformFunction? ParseTranslate2d(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null)
            {
                var y = default(ICssValue);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistanceOrCalc();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransformFunction(((Length)x).Value, y != null ? ((Length)y).Value : 0.0, 0.0);
                }
            }

            return null;
        }

        private static TranslateTransformFunction? ParseTranslate3d(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null)
            {
                var y = default(ICssValue);
                var z = default(ICssValue);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistanceOrCalc();
                    f = source.SkipGetSkip();

                    if (y == null)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseDistanceOrCalc();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransformFunction(((Length)x).Value, y != null ? ((Length)y).Value : 0.0, z != null ? ((Length)z).Value : 0.0);
                }
            }

            return null;
        }

        private static TranslateTransformFunction? ParseTranslateX(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransformFunction(((Length)x).Value, 0.0, 0.0);
            }

            return null;
        }

        private static TranslateTransformFunction? ParseTranslateY(StringSource source)
        {
            var y = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (y != null && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransformFunction(0.0, ((Length)y).Value, 0.0);
            }

            return null;
        }

        private static TranslateTransformFunction? ParseTranslateZ(StringSource source)
        {
            var z = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (z != null && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransformFunction(0.0, 0.0, ((Length)z).Value);
            }

            return null;
        }
    }
}
