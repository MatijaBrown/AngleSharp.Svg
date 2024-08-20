using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.DOM;
using AngleSharp.Text;
using System.Globalization;

namespace AngleSharp.Svg.DataTypes.Internal
{
    internal sealed class ReflectedSvgNumber : SvgReflectedValue, ISvgNumber
    {

        private readonly bool _isReadOnly;

        public float Value
        {
            get => (float)StoredValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                AssociatedElement.SetAttribute(ReflectedAttributeName, value.ToString(CultureInfo.InvariantCulture));
            }
        }

        internal ReflectedSvgNumber(ISvgElement associatedElement, string reflectedAttributeName,
            ICssComputeContext computeContext, bool isReadOnly = false)
            : base(associatedElement, reflectedAttributeName, computeContext)
        {
            _isReadOnly = isReadOnly;
            StoredValue = Reflect();
        }

        protected override ICssMetricValue Reflect()
        {
            var cssString = new StringSource(AssociatedElement.GetAttribute(ReflectedAttributeName) ?? throw new DomException(DomError.NotFound));

            return NumberParser.ParseNumber(cssString)
                ?? throw new DomException(DomError.TypeMismatch);
        }

    }
}
