using Setups;
using Core.Views;
using UnityEngine;

namespace Core.Controllers
{
    public class LockElementController : ElementController
    {
        [SerializeField] private LockElementView _elementView;

        public override void UpdateElementSetup(ElementSetup elementSetup)
        {
            base.UpdateElementSetup(elementSetup);
            _elementView.UpdateIconElement(_elementSetup.iconElement);
        }
        
        protected override void OnClickToButton()
        {
            //Show Info Dialog
        }
    }
}