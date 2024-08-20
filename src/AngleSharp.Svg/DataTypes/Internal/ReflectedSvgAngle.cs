using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.DOM;
using AngleSharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.Svg.DataTypes.Internal
{
    internal sealed class ReflectedSvgAngle : SvgReflectedValue, ISvgAngle
    {

        private readonly bool _isReadOnly;

        public SvgAngleType UnitType { get; private set; }

        public float Value
        {
            get => (float)StoredValue.AsDeg();
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                var degValue = new CssAngleValue(value, CssAngleValue.Unit.Deg);
                double convertedValue = ISvgAngle.DegConversionMap[UnitType](degValue.Value);
                CssAngleValue.Unit targetUnit = ISvgAngle.CssUnitMap[UnitType];

                UpdateValue(new CssAngleValue(convertedValue, targetUnit));
            }
        }

        public float ValueInSpecifiedUnits
        {
            get => (float)StoredValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                UpdateValue(new CssAngleValue(value, ISvgAngle.CssUnitMap[UnitType]));
            }
        }

        public string ValueAsString
        {
            get => StoredValue.CssText;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                UpdateValue(UnitParser.ParseAngle(new StringSource(value)) ?? throw new DomException(DomError.NotSupported));
            }
        }

        internal ReflectedSvgAngle(ISvgElement associatedElement, string reflectedAttributeName, ICssComputeContext computeContext, bool isReadOnly = false)
            : base(associatedElement, reflectedAttributeName, computeContext)
        {
            _isReadOnly = isReadOnly;
            StoredValue = Reflect();
        }

        protected override ICssMetricValue Reflect()
        {
            var cssString = new StringSource(AssociatedElement.GetAttribute(ReflectedAttributeName) ?? throw new DomException(DomError.NotFound));

            ICssValue parsedValue = UnitParser.ParseAngleOrCalc(cssString)
                ?? throw new DomException(DomError.TypeMismatch);

            return (ICssMetricValue)parsedValue.Compute(ComputeContext);
        }

        public void NewValueSpecifiedUnits(SvgAngleType unitType, float valueInSpecifiedUnits)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            UpdateValue(new CssAngleValue(valueInSpecifiedUnits, ISvgAngle.CssUnitMap[unitType]));
            UnitType = unitType;
        }

        public void ConvertToSpecifiedUnits(SvgAngleType unitType)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgAngleType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            double degrees = Value;

            CssAngleValue newValue = unitType switch
            {
                SvgAngleType.Unspecified or SvgAngleType.Deg => new CssAngleValue(degrees, CssAngleValue.Unit.Deg),
                SvgAngleType.Rad => new CssAngleValue(double.DegreesToRadians(degrees), CssAngleValue.Unit.Rad),
                SvgAngleType.Grad => new CssAngleValue(degrees / 90.0 * 100.0, CssAngleValue.Unit.Grad),
                _ => throw new DomException(DomError.NotSupported),
            };

            UpdateValue(newValue);

            UnitType = unitType;
        }

    }
}
