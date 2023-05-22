﻿using Zenject;
using Services;
using Setups;

namespace Core.Controllers
{
    public class LockElementController : ElementController
    {
        [Inject] private DialogHelper _dialogHelper;

        protected override void UpdateElementSetup()
        {
            stateElement = _dataHelper.ElementsDataModel
                .GetHintStateElement(_elementSetup.keyElement,
                    _elementSetup.keyElement) == StateElement.Open;
            base.UpdateElementSetup();
        }

        protected override void OnClickToButton()
        {
            // Show info Dialog Or Open View Element
            _dialogHelper.CreateDialogElementInfo(_elementSetup);
        }
    }
}