namespace AngleSharp.Svg.Rendering.Paths
{
    public class SvgPathCommand
    {

        public SvgPathCommandType Type { get; }

        public double X { get; }

        public double Y { get; }

        public double X1 { get; }

        public double Y1 { get; }

        public double X2 { get; }

        public double Y2 { get; }

        public double RX { get; }

        public double RY { get; }

        public double XAxisRotation { get; }

        public bool LargeArcFlag { get; }

        public bool SweepFlag { get; }

        public SvgPathCommand? Next { get; internal set; }

        internal SvgPathCommand()
        {
            Type = SvgPathCommandType.Close;
        }

        internal SvgPathCommand(SvgPathCommandType type, double x, double y)
        {
            Type = type;
            X = x;
            Y = y;
        }

        internal SvgPathCommand(double rx, double ry, double xAxisRotation, bool largeArcFlag, bool sweepFlag, double x, double y)
        {
            Type = SvgPathCommandType.Ellipse;
            RX = rx;
            RY = ry;
            XAxisRotation = xAxisRotation;
            LargeArcFlag = largeArcFlag;
            SweepFlag = sweepFlag;
            X = x;
            Y = y;
        }

        internal void Append(SvgPathCommand next)
        {
            if (Next == null)
            {
                Next = next;
            }
            else
            {
                Next.Append(next);
            }
        }

    }
}
