using Setups;
using Zenject;
using UnityEngine;
using Core.Controllers;
using Core.Controllers.Dialogs;

namespace Services
{
    public class DialogHelper : MonoBehaviour
    {
        [SerializeField] private DialogElementInfoController _dialogElementInfoController;
        [SerializeField] private DialogNewElementController _dialogNewElementController;
        [SerializeField] private Transform _parentDialog;
        [Inject] private DiContainer _diContainer;


        public void CreateDialogElementInfo(ElementSetup elementSetup)
        {
            var dialog = Instantiate(_dialogElementInfoController, _parentDialog);
            dialog.Initialize(elementSetup);
            _diContainer.Inject(dialog);
        } 
        public void CreateDialogNewElement(ElementSetup elementSetup)
        {
            var dialog = Instantiate(_dialogNewElementController, _parentDialog);
            dialog.Initialize(elementSetup);
            _diContainer.Inject(dialog);
        }
    }
}