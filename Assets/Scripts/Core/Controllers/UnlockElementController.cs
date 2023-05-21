using Setups;
using Zenject;
using Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class UnlockElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonChooseElement;
        [SerializeField] private UnlockElementView _elementView;

        private ElementSetup _elementSetup;

        [Inject] private SessionDataController _sessionDataController;

        private void Start()
        {
            _buttonChooseElement.onClick.AddListener(AddElementToBar);
        }

        public void UpdateElementSetup(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            _elementView.UpdateNameElement(_elementSetup.keyElement);
            _elementView.UpdateIconElement(_elementSetup.iconElement);
        }

        public bool CheckSameElementSetup(string keyElement) => _elementSetup.keyElement == keyElement;

        private void AddElementToBar()
        {
            _sessionDataController.OnAppendElement(_elementSetup);
        }
    }
}