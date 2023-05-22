using Setups;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class DialogElementInfoController : MonoBehaviour
    {
        [SerializeField] private Button _buttonCloseDialog;
        [SerializeField] private ElementInfoController _elementInfoController;
        [SerializeField] private Transform _parentElementInfo;

        private void Start()
        {
            _buttonCloseDialog.onClick.AddListener(CloseDialog);
        }


        public void Initialize(ElementSetup elementSetup)
        {
            foreach (var parentElementSetup in elementSetup.childElements)
            {
                var elementInfo = Instantiate(_elementInfoController, _parentElementInfo);
                elementInfo.UpdateElementSetup(parentElementSetup);
            }
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }
    }
}