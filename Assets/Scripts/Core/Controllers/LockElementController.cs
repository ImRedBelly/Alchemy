using Zenject;
using Services;

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
            if (stateElement)
                _dialogHelper.CreateDialogElementInfo(_elementSetup);
            else
            {
                _dataHelper.ElementsDataModel.SetHintStateElement(_elementSetup.keyElement, 
                    _elementSetup.keyElement, StateElement.Open);
                _dataHelper.SaveElementsDataModel();
                UpdateElementSetup();
            }
        }
    }
}