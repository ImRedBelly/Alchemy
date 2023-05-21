using Setups;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public abstract class ElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonElement;

        protected ElementSetup _elementSetup;
        [Inject] protected SessionDataController _sessionDataController;

        private void Start()
            => _buttonElement.onClick.AddListener(OnClickToButton);

        public virtual void UpdateElementSetup(ElementSetup elementSetup)
            => _elementSetup = elementSetup;

        public bool CheckSameElementSetup(string keyElement)
            => _elementSetup.keyElement == keyElement;

        protected abstract void OnClickToButton();
    }
}