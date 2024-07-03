using Silk.NET.Maths;

namespace AngleSharp.DomGeometry
{
    public class DomPointReadonly
    {

        protected Vector4D<double> _v;

        public DomPointReadonly(double x = 0, double y = 0, double z = 0, double w = 1)
        {
            _v = new Vector4D<double>(x, y, z, w);
        }

        internal DomPointReadonly(Vector4D<double> v)
        {
            _v = v;
        }

        public virtual double X
        {
            get => _v.X;
            set => throw new NotImplementedException("Cannot set X Property of DomReadOnlyPoint");
        }

        public virtual double Y
        {
            get => _v.Y;
            set => throw new NotImplementedException("Cannot set Y Property of DomReadOnlyPoint");
        }

        public virtual double Z
        {
            get => _v.Z;
            set => throw new NotImplementedException("Cannot set Z Property of DomReadOnlyPoint");
        }

        public virtual double W
        {
            get => _v.W;
            set => throw new NotImplementedException("Cannot set W Property of DomReadOnlyPoint");
        }

        public DomPoint MatrixTransform(DomMatrixReadOnly matrix)
        {
            return new DomPoint(_v * matrix.M);
        }

    }
}
