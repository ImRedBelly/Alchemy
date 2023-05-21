using Core.Views;
using Setups;
using UnityEngine;

namespace Core.Controllers
{
    public class UnlockElementController : ElementController
    {
        [SerializeField] private UnlockElementView _elementView;

        public override void UpdateElementSetup(ElementSetup elementSetup)
        {
            base.UpdateElementSetup(elementSetup);
            _elementView.UpdateNameElement(_elementSetup.keyElement);
            _elementView.UpdateIconElement(_elementSetup.iconElement);
        }

        protected override void OnClickToButton()
        {
            _sessionDataController.OnAppendElement(_elementSetup);
        }
    }
}