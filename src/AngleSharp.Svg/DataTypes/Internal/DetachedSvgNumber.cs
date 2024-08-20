using AngleSharp.Css.Values;
using AngleSharp.Dom;

namespace AngleSharp.Svg.DataTypes
{
    internal sealed class DetachedSvgNumber : ISvgNumber
    {

        private readonly bool _isReadOnly;

        private CssNumberValue _storedValue;

        public float Value
        {
            get => (float)_storedValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                _storedValue = new CssNumberValue(value);
            }
        }

        internal DetachedSvgNumber(float initialValue = default, bool isReadOnly = false)
        {
            _isReadOnly = isReadOnly;
            _storedValue = new CssNumberValue(initialValue);
        }

    }
}
