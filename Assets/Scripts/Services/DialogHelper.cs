using Setups;
using Zenject;
using UnityEngine;
using Core.Controllers;

namespace Services
{
    public class DialogHelper : MonoBehaviour
    {
        [SerializeField] private DialogElementInfoController dialogElementInfoController;
        [SerializeField] private Transform _parentDialog;
        [Inject] private DiContainer _diContainer;


        public void CreateDialogElementInfo(ElementSetup elementSetup)
        {
            var dialog = Instantiate(dialogElementInfoController, _parentDialog);
            dialog.Initialize(elementSetup);
            _diContainer.Inject(dialog);
        }
    }
}