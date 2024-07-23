namespace AngleSharp.DomGeometry.Dom
{
    internal static class Utils
    {

        internal static bool IsZero(double value)
        {
            return Math.Abs(value) < double.Epsilon;
        }

    }
}
