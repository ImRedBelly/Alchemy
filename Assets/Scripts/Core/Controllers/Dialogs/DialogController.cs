using Setups;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers.Dialogs
{
    public abstract class DialogController : MonoBehaviour
    {
        [SerializeField] private Button _buttonCloseDialog;
        public abstract void Initialize(ElementSetup elementSetup);

        private void Start()
        {
            _buttonCloseDialog.onClick.AddListener(CloseDialog);
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }
    }
}