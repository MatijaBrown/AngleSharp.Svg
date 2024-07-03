using Silk.NET.Maths;

namespace AngleSharp.DomGeometry
{
    public class DomPoint : DomPointReadonly
    {

        public DomPoint(double x = 0, double y = 0, double z = 0, double w = 1)
            : base(x, y, z, w) { }

        internal DomPoint(Vector4D<double> v)
            : base(v) { }

        public override double X
        {
            get => _v.X;
            set => _v.X = value;
        }

        public override double Y
        {
            get => _v.Y;
            set => _v.Y = value;
        }

        public override double Z
        {
            get => _v.Z;
            set => _v.Z = value;
        }

        public override double W
        {
            get => _v.W;
            set => _v.W = value;
        }

    }
}
