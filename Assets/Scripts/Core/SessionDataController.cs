using System;
using Setups;
using System.Linq;
using Core.Controllers;
using System.Collections.Generic;

namespace Core
{
    public class SessionDataController
    {
        public event Action<ElementSetup> CreateNewElement;

        public bool BarIsFull => _elementInBar.Count >= 3;
        public bool BarIsEmpty => _elementInBar.Count == 0;

        private List<ElementSetup> _unlockElements = new List<ElementSetup>();
        private List<ElementSetup> _futureElements = new List<ElementSetup>();
        private List<ElementSetup> _elementInBar = new List<ElementSetup>();
        private List<BarElementController> _barButtonsElementControllers = new List<BarElementController>();


        public void AppendButtonBarElementController(BarElementController elementController)
        {
            if (!_barButtonsElementControllers.Contains(elementController))
                _barButtonsElementControllers.Add(elementController);
        }

        public void ResetButtonBarElementControllers()
        {
            foreach (var barElement in _barButtonsElementControllers)
                barElement.RemoveElementSetup();
        }

        public List<ElementSetup> GetElementInBar() => _elementInBar;

        public void OnAppendElement(ElementSetup elementSetup)
        {
            if (BarIsFull) return;
            var elementBar = _barButtonsElementControllers.FirstOrDefault(x => x.ElementEmpty);
            if (elementBar != null)
            {
                elementBar.UpdateElementSetup(elementSetup);
                _elementInBar.Add(elementSetup);
            }
        }

        public void OnRemoveElement(ElementSetup elementSetup)
        {
            if (_elementInBar.Contains(elementSetup))
                _elementInBar.Remove(elementSetup);
        }

        public List<ElementSetup> GetUnlockElements() => _unlockElements;

        public void AddUnlockElements(ElementSetup elementSetup)
        {
            if (!_unlockElements.Contains(elementSetup))
            {
                _unlockElements.Add(elementSetup);
                CreateNewElement?.Invoke(elementSetup);
            }
        }

        public bool FutureElementIsCreated(ElementSetup elementSetup)
            => _futureElements.Contains(elementSetup);

        public List<ElementSetup> GetFutureElements()
            => _futureElements;

        public void AddFutureElements(ElementSetup elementSetup)
        {
            if (!_futureElements.Contains(elementSetup))
                _futureElements.Add(elementSetup);
        }

        public void RemoveFutureElements(ElementSetup elementSetup)
        {
            if (_futureElements.Contains(elementSetup))
                _futureElements.Remove(elementSetup);
        }
    }
}