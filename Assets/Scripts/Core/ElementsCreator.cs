using System;
using Setups;
using Zenject;
using Services;
using UnityEngine;
using Core.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core
{
    public class ElementsCreator : MonoBehaviour
    {
        public event Action LoadElements;

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
            CheckElementSetupsAsync();
            _sessionDataController.CreateNewElement += RemoveFutureAndCreateUnlockElement;
        }

        private void OnDisable()
        {
            _sessionDataController.CreateNewElement -= RemoveFutureAndCreateUnlockElement;
        }

        private async void CheckElementSetupsAsync()
        {
            foreach (var elementSetup in _baseElements)
            {
                elementSetup.Init(_sessionDataController, _dataHelper);
                elementSetup.OpenElement();
            }

            foreach (var elementSetup in _baseElements)
                elementSetup.ProcessOpenElements();


            Task taskForOpenElements = CreateOpenElements(_sessionDataController.GetUnlockElements());
            Task taskForFutureElements = CreateFutureElements(_sessionDataController.GetFutureElements());

            await Task.WhenAll(taskForOpenElements, taskForFutureElements);
            LoadElements?.Invoke();
        }


        private async Task CreateOpenElements(List<ElementSetup> elementSetups)
        {
            foreach (var elementSetup in elementSetups)
                _unlockElements.Add(CreateUnlockElementPanel(elementSetup, _parentUnlockElements));
        }

        private async Task CreateFutureElements(List<ElementSetup> elementSetups)
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


                    foreach (var newFutureElementSetup in elementSetup.parentElements.Keys)
                        if (!_sessionDataController.FutureElementIsCreated(newFutureElementSetup))
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