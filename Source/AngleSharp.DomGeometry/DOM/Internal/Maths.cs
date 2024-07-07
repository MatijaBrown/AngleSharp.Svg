namespace AngleSharp.DomGeometry.DOM.Internal
{
    internal static class Maths
    {

        internal static bool IsZero(double value)
        {
            return Math.Abs(value) < double.Epsilon;
        }

    }
}
