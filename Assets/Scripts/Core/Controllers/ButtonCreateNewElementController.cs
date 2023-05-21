using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Controllers
{
    public class ButtonCreateNewElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonCreateNewElement;
        [Inject] private SessionDataController _sessionDataController;

        private void Start()
        {
            _buttonCreateNewElement.onClick.AddListener(CreateNewElement);
        }

        private void CreateNewElement()
        {
            if (_sessionDataController.BarIsEmpty) return;
            var elements = _sessionDataController.GetElementSetups();
            var newElement = elements[0].CheckCreateNewElement(elements);

            var elementsName = " ";
            foreach (var elementSetup in elements)
                elementsName += elementSetup.keyElement;

            Debug.LogError(elementsName + ": " + newElement);
        }
    }
}