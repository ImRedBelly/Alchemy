using System.Collections;
using Setups;
using Zenject;
using Services;
using UnityEngine;
using Core.Controllers;
using System.Collections.Generic;

namespace Core
{
    public class ElementsCreator : MonoBehaviour
    {
        [SerializeField] private UnlockElementController _unlockElementController;
        [SerializeField] private ElementSetup[] _baseElements;
        [SerializeField] private Transform _parentUnlockElements;
        [SerializeField] private Transform _parentFuturelements;

        [Inject] private SessionDataController _sessionDataController;
        [Inject] private DataHelper _dataHelper;

        private List<UnlockElementController> _unlockElements = new List<UnlockElementController>();
        private List<UnlockElementController> _futureElements = new List<UnlockElementController>();

        private void Start()
        {
            StartCoroutine(CheckElementSetups());
        }

        private IEnumerator CheckElementSetups()
        {
            foreach (var elementSetup in _baseElements)
            {
                elementSetup.Init(_sessionDataController, _dataHelper);
                elementSetup.OpenElement();
            }

            foreach (var elementSetup in _baseElements)
                elementSetup.CheckOpenElements();
            yield return new WaitForSeconds(1);
            CreateOpenElements();
            CreateFutureElements();
        }

        private void CreateOpenElements()
        {
            foreach (var elementSetup in _sessionDataController.GetUnlockElements())
            {
                var elementController = Instantiate(_unlockElementController, _parentUnlockElements);
                elementController.UpdateElementSetup(elementSetup);
                _unlockElements.Add(elementController);
            }
        }

        private void CreateFutureElements()
        {
            foreach (var elementSetup in _sessionDataController.GetFutureElements())
            {
                var elementController = Instantiate(_unlockElementController, _parentFuturelements);
                elementController.UpdateElementSetup(elementSetup);
                _futureElements.Add(elementController);
            }
        }
    }
}