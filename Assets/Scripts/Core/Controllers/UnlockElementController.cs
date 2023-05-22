namespace Core.Controllers
{
    public class UnlockElementController : ElementController
    {
        protected override void OnClickToButton()
        {
            _sessionDataController.OnAppendElement(_elementSetup);
        }
    }
}