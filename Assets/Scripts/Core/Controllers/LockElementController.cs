using Zenject;
using Services;

namespace Core.Controllers
{
    public class LockElementController : ElementController
    {
        [Inject] private DialogHelper _dialogHelper;

        protected override void OnClickToButton()
        {
            // Show info Dialog Or Open View Element
            _dialogHelper.CreateDialogElementInfo(_elementSetup);
        }
    }
}