using Zenject;
using Services;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class ButtonCreateNewElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonCreateNewElement;

        [Inject] private SessionDataController _sessionDataController;
        [Inject] private DialogHelper _dialogHelper;
        [Inject] private DataHelper _dataHelper;

        private void Start()
        {
            _buttonCreateNewElement.onClick.AddListener(CreateNewElement);
        }

        private void CreateNewElement()
        {
            if (_sessionDataController.BarIsEmpty) return;
            var elements = _sessionDataController.GetElementInBar();
            var newElement = elements.First().Key.CheckCreateNewElement(elements);
            
            if (newElement != null && _dataHelper.ElementsDataModel.GetStateElement(newElement.keyElement) == StateElement.Close)
            {
                newElement.OpenElement();
                _dataHelper.SaveElementsDataModel();
                _sessionDataController.ResetButtonBarElementControllers();
                _dialogHelper.CreateDialogNewElement(newElement);
            }
        }
    }
}