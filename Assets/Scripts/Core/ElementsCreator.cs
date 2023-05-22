using Setups;
using Zenject;
using Services;
using UnityEngine;
using Core.Controllers;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class ElementsCreator : MonoBehaviour
    {
        [SerializeField] private ElementController _unlockElementController;
        [SerializeField] private ElementController _lockElementController;

        [SerializeField] private ElementSetup[] _baseElements;
        [SerializeField] private Transform _parentUnlockElements;
        [SerializeField] private Transform _parentFuturelements;

        [Inject] private SessionDataController _sessionDataController;
        [Inject] private DataHelper _dataHelper;

        private List<ElementController> _unlockElements = new List<ElementController>();
        private List<ElementController> _futureElements = new List<ElementController>();

        private void Start()
        {
            StartCoroutine(CheckElementSetups());
            _sessionDataController.CreateNewElement += RemoveFutureAndCreateUnlockElement;
        }

        private void OnDisable()
        {
            _sessionDataController.CreateNewElement -= RemoveFutureAndCreateUnlockElement;
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
            CreateOpenElements(_sessionDataController.GetUnlockElements());
            CreateFutureElements(_sessionDataController.GetFutureElements());
        }

        private void CreateOpenElements(List<ElementSetup> elementSetups)
        {
            foreach (var elementSetup in elementSetups)
                _unlockElements.Add(CreateUnlockElementPanel(elementSetup, _parentUnlockElements));
        }

        private void CreateFutureElements(List<ElementSetup> elementSetups)
        {
            foreach (var elementSetup in elementSetups)
                _futureElements.Add(CreateLockElementPanel(elementSetup, _parentFuturelements));
        }

        private void RemoveFutureAndCreateUnlockElement(ElementSetup elementSetup)
        {
            foreach (var futureElement in _futureElements)
            {
                if (futureElement.CheckSameElementSetup(elementSetup.keyElement))
                {
                    elementSetup.Init(_sessionDataController, _dataHelper);
                    CreateUnlockElementPanel(elementSetup, _parentUnlockElements);


                    foreach (var newFutureElementSetup in elementSetup.parentElements)
                        if (!_sessionDataController.FutureElementIsCreate(newFutureElementSetup))
                        {
                            newFutureElementSetup.Init(_sessionDataController, _dataHelper);
                            _futureElements.Add(CreateLockElementPanel(newFutureElementSetup, _parentFuturelements));
                            _sessionDataController.AddFutureElements(newFutureElementSetup);
                        }


                    Destroy(futureElement.gameObject);
                    _futureElements.Remove(futureElement);
                    break;
                }
            }
        }

        private ElementController CreateUnlockElementPanel(ElementSetup elementSetup, Transform parent)
        {
            var elementController = Instantiate(_unlockElementController, parent);
            elementController.Initialize(elementSetup);
            return elementController;
        }

        private ElementController CreateLockElementPanel(ElementSetup elementSetup, Transform parent)
        {
            var elementController = Instantiate(_lockElementController, parent);
            elementController.Initialize(elementSetup);
            return elementController;
        }
    }
}