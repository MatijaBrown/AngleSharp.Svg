using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.DomGeometry.DOM
{
    public interface IDomMatrix : IDomMatrixReadOnly
    {

        public double M11 { get; set; }

    }
}
