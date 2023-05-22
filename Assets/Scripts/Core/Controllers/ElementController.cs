using Setups;
using Zenject;
using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public abstract class ElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonElement;
        [SerializeField] private ElementView _elementView;

        protected ElementSetup _elementSetup;
        [Inject] protected SessionDataController _sessionDataController;
        [Inject] protected MaterialsContainer _materialsContainer;

        private void Start()
            => _buttonElement.onClick.AddListener(OnClickToButton);

        public void UpdateElementSetup(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            _elementView.UpdateNameElement(_elementSetup.keyElement);
            _elementView.UpdateIconElement(_elementSetup.iconElement);

            var materialIcon = elementSetup.GetStateElement() == StateElement.Close
                ? _materialsContainer.colorMaterial
                : _materialsContainer.defaultMaterial;

            _elementView.UpdateMaterialIcon(materialIcon);
        }

        public bool CheckSameElementSetup(string keyElement)
            => _elementSetup.keyElement == keyElement;

        protected abstract void OnClickToButton();
    }
}