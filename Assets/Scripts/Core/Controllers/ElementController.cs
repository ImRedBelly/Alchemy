using Setups;
using Zenject;
using Core.Views;
using Services;
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
        [Inject] protected DataHelper _dataHelper;

        protected bool stateElement = true;

        private void Start()
            => _buttonElement.onClick.AddListener(OnClickToButton);

        public void Initialize(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            UpdateElementSetup();
        }

        protected virtual void UpdateElementSetup()
        {
            _elementView.UpdateNameElement(stateElement ? _elementSetup.keyElement : "???");
            _elementView.UpdateIconElement(_elementSetup.iconElement);

            var materialIcon = stateElement
                ? _materialsContainer.defaultMaterial
                : _materialsContainer.colorMaterial;

            _elementView.UpdateMaterialIcon(materialIcon);
        }

        public bool CheckSameElementSetup(string keyElement)
            => _elementSetup.keyElement == keyElement;

        protected abstract void OnClickToButton();
    }
}