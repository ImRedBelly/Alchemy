using Setups;

namespace Core.Controllers
{
    public class ElementInfoController : ElementController
    {
        private ElementSetup _parentElementSetup;

        public void Initialize(ElementSetup parentElementSetup, ElementSetup chieldElementSetup)
        {
            _parentElementSetup = parentElementSetup;
            Initialize(chieldElementSetup);
        }

        protected override void UpdateElementSetup()
        {
            stateElement = _dataHelper.ElementsDataModel
                .GetHintStateElement(_parentElementSetup.keyElement,
                    _elementSetup.keyElement) == StateElement.Open;
            base.UpdateElementSetup();
        }

        protected override void OnClickToButton()
        {
            //Open View Element
            _dataHelper.ElementsDataModel.SetHintStateElement(_parentElementSetup.keyElement,
                _elementSetup.keyElement, StateElement.Open);
            _dataHelper.SaveElementsDataModel();
            UpdateElementSetup();
        }
    }
}