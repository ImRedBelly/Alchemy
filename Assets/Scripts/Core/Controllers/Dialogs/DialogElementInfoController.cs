using Setups;
using UnityEngine;

namespace Core.Controllers.Dialogs
{
    public class DialogElementInfoController : DialogController
    {
        [SerializeField] private ElementInfoController _elementInfoController;
        [SerializeField] private Transform _parentElementInfo;
        
        public override void Initialize(ElementSetup elementSetup)
        {
            foreach (var childElement in elementSetup.childElements)
            {
                var elementInfo = Instantiate(_elementInfoController, _parentElementInfo);
                elementInfo.Initialize(elementSetup, childElement.Key);
            }
        }
    }
}