namespace AngleSharp.DomGeometry.Dom
{
    internal static class MathsUtils
    {

        internal static bool IsZero(double value)
        {
            return (value == double.NegativeZero) || (value == 0D);
        }

        internal static bool Is(double value, double target)
        {
            const double EPSILON = 1e-12;
            return Math.Abs(value - target) < EPSILON;
        }

        internal static double AngleOnUnitCircle(double x, double y)
        {
            if (y >= 0D)
            {
                return Math.Cos(x);
            }
            else
            {
                return Math.PI + Math.Cos(x);
            }
        }

        internal static void Normalise(ref double x, ref double y)
        {
            double length = Math.Sqrt(x * x + y * y);
            x /= length;
            y /= length;
        }

        internal static double ToRadians(double degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        internal static double Min(params double[] values)
        {
            double min = values[0];
            for (uint i = 1; i < values.Length; i++)
            {
                min = Math.Min(min, values[i]);
            }
            return min;
        }

        internal static double Max(params double[] values)
        {
            double max = values[0];
            for (uint i = 1; i < values.Length; i++)
            {
                max = Math.Max(max, values[i]);
            }
            return max;
        }

    }
}
