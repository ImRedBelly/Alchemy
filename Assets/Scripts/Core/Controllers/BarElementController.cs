using Setups;
using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class BarElementController : MonoBehaviour
    {
        public bool ElementEmpty => _elementSetup == null;
        [SerializeField] private Button _buttonRemoveElement;
        [SerializeField] private BarElementsView _barElementsView;
        [SerializeField] private Sprite _defaultIconElement;

        private ElementSetup _elementSetup;

        private void Start()
        {
            _buttonRemoveElement.onClick.AddListener(() => UpdateElementSetup(null));
            UpdateElementSetup(null);
        }

        public ElementSetup GetElementSetup() => _elementSetup;

        public void UpdateElementSetup(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            var icon = _elementSetup == null ? _defaultIconElement : _elementSetup.iconElement;
            _barElementsView.UpdateIconElement(icon);
        }
    }
}