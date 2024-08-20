using AngleSharp.Css;
using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.Viewport;
using AngleSharp.Text;

namespace AngleSharp.Svg.DataTypes.Internal
{
    internal sealed class DetachedSvgLength : ISvgLength
    {

        private readonly bool _isReadOnly;

        private readonly RenderMode _directionality;
        private readonly IEstablishesViewport? _viewportProvider;

        private CssLengthValue _storedValue;

        public SvgLengthType UnitType { get; private set; }

        public float Value
        {
            get => (float)_storedValue.ToPixel(RenderDimensions, _directionality);
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                CssLengthValue.Unit targetUnit = ISvgLength.CssUnitMap[UnitType];
                var pixelLength = new CssLengthValue(value, CssLengthValue.Unit.Px);
                double convertedValue = pixelLength.To(targetUnit, RenderDimensions, _directionality);

                _storedValue = new CssLengthValue(convertedValue, targetUnit);
            }
        }

        public float ValueInSpecifiedUnits
        {
            get => (float)_storedValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                _storedValue = new CssLengthValue(value, _storedValue.Type);
            }
        }

        public string ValueAsString
        {
            get => _storedValue.CssText;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                _storedValue = UnitParser.ParseLength(new StringSource(value)) ?? throw new DomException(DomError.TypeMismatch);
                string unitString = _storedValue.UnitString;
                if (!ISvgLength.UnitStringMap.TryGetValue(unitString, out SvgLengthType unit))
                {
                    UnitType = SvgLengthType.Unkown;
                }
                else
                {
                    UnitType = unit;
                }
            }
        }

        private IRenderDimensions RenderDimensions
        {
            get
            {
                IRenderDimensions renderDimensions;
                if (_viewportProvider is null)
                {
                    renderDimensions = new SvgRenderDimensions(100.0, 100.0, CssLengthValue.Medium.Value);
                }
                else
                {
                    renderDimensions = _viewportProvider.RenderDimensions;
                }
                return renderDimensions;
            }
        }

        internal DetachedSvgLength(float initialValue = default, SvgLengthType unitType = SvgLengthType.Unkown,
            RenderMode directionality = RenderMode.Undefined, IEstablishesViewport? viewportProvider = null, bool isReadOnly = false)
        {
            _isReadOnly = isReadOnly;
            _directionality = directionality;
            _viewportProvider = viewportProvider;
            _storedValue = new CssLengthValue(initialValue, ISvgLength.CssUnitMap[unitType]);
            UnitType = unitType;
        }

        public void NewValueSpecifiedUnits(SvgLengthType unitType, float valueInSpecifiedUnits)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgLengthType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            _storedValue = new CssLengthValue(valueInSpecifiedUnits, ISvgLength.CssUnitMap[unitType]);
            UnitType = unitType;
        }

        public void ConvertToSpecifiedUnits(SvgLengthType unitType)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgLengthType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            CssLengthValue.Unit newCssUnit = ISvgLength.CssUnitMap[unitType];

            double converted = _storedValue.To(newCssUnit, RenderDimensions, _directionality);
            _storedValue = new CssLengthValue(converted, newCssUnit);

            UnitType = unitType;
        }

    }
}
