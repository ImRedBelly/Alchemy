using Core.Views;
using Setups;
using UnityEngine;

namespace Core.Controllers.Dialogs
{
    public class DialogNewElementController : DialogController
    {
        [SerializeField] private ElementView _elementView;

        public override void Initialize(ElementSetup elementSetup)
        {
            _elementView.UpdateNameElement(elementSetup.keyElement);
            _elementView.UpdateIconElement(elementSetup.iconElement);
        }
    }
}