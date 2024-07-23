using AngleSharp.Dom;
using System.Globalization;

namespace AngleSharp.Svg.DataTypes
{
    public class SvgNumber(bool isReadonly = false) : ISvgNumber
    {

        private readonly IElement? _associatedElement;
        private readonly IAttr? _associatedAttribute;
        private readonly MutationObserver? _observer;

        private float _value;

        public float Value {
            get => _value;
            set
            {
                if (isReadonly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }
                if (_associatedAttribute != null)
                {
                    _associatedAttribute.Value = value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    _value = value;
                }
            }
        }

        public SvgNumber(IElement associatedElement, string attributeName)
            : this(false)
        {
            _associatedElement = associatedElement;
            _associatedAttribute = _associatedElement.Attributes[attributeName];
            _observer = new MutationObserver(OnMutation);
            _observer.Connect(_associatedElement, attributes: true, attributeFilter: [attributeName]);
            UpdateValue();
        }

        private void UpdateValue()
        {
            if (!float.TryParse(_associatedAttribute!.Value, out _value))
            {
                throw new DomException(DomError.TypeMismatch);
            }
        }

        private void OnMutation(IMutationRecord[] mutations, MutationObserver observer)
        {
            UpdateValue();
        }

    }
}
