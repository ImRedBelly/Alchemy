using System;
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
                _unlockElements.Add(CreateElementPanel(elementSetup, _parentUnlockElements));
        }

        private void CreateFutureElements(List<ElementSetup> elementSetups)
        {
            foreach (var elementSetup in elementSetups)
                _futureElements.Add(CreateElementPanel(elementSetup, _parentFuturelements));
        }

        private void RemoveFutureAndCreateUnlockElement(ElementSetup elementSetup)
        {
            foreach (var futureElement in _futureElements)
            {
                if (futureElement.CheckSameElementSetup(elementSetup.keyElement))
                {
                    elementSetup.Init(_sessionDataController, _dataHelper);
                    CreateElementPanel(elementSetup, _parentUnlockElements);


                    foreach (var newFutureElementSetup in elementSetup.parentElements)
                        if (!_sessionDataController.FutureElementIsCreate(newFutureElementSetup))
                        {
                            newFutureElementSetup.Init(_sessionDataController, _dataHelper);
                            _futureElements.Add(CreateElementPanel(newFutureElementSetup, _parentFuturelements));
                            _sessionDataController.AddFutureElements(newFutureElementSetup);
                        }


                    Destroy(futureElement.gameObject);
                    _futureElements.Remove(futureElement);
                    break;
                }
            }
        }

        private UnlockElementController CreateElementPanel(ElementSetup elementSetup, Transform parent)
        {
            var elementController = Instantiate(_unlockElementController, parent);
            elementController.UpdateElementSetup(elementSetup);
            return elementController;
        }
    }
}