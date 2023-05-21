using System;
using Setups;
using System.Linq;
using Core.Controllers;
using System.Collections.Generic;

namespace Core
{
    public class SessionDataController
    {
        public event Action<ElementSetup> AppendElement;
        public event Action<ElementSetup> RemoveElement;
        public event Action<ElementSetup> AppendUnlockElement;
        public event Action<ElementSetup> RemoveFutureElement;

        public bool BarIsFull => _elementSetups.Count >= 3;
        public bool BarIsEmpty => _elementSetups.Count == 0;


        private List<ElementSetup> _unlockElements = new List<ElementSetup>();
        private List<ElementSetup> _futureElements = new List<ElementSetup>();
        private List<ElementSetup> _elementSetups = new List<ElementSetup>();
        private List<BarElementController> _barButtonsElementControllers = new List<BarElementController>();

        public List<ElementSetup> GetElementSetups() => _elementSetups;

        public void AppendButtonBarElementController(BarElementController elementController)
        {
            if (!_barButtonsElementControllers.Contains(elementController))
                _barButtonsElementControllers.Add(elementController);
        }

        public void OnAppendElement(ElementSetup elementSetup)
        {
            if (BarIsFull) return;
            var elementBar = _barButtonsElementControllers.FirstOrDefault(x => x.ElementEmpty);
            if (elementBar != null) elementBar.UpdateElementSetup(elementSetup);


            _elementSetups.Add(elementSetup);
            AppendElement?.Invoke(elementSetup);
        }

        public void OnRemoveElement(ElementSetup elementSetup)
        {
            if (_elementSetups.Contains(elementSetup))
            {
                _elementSetups.Remove(elementSetup);
                RemoveElement?.Invoke(elementSetup);
            }
        }

        public List<ElementSetup> GetUnlockElements() => _unlockElements;

        public void AddUnlockElements(ElementSetup elementSetup)
        {
            if (!_unlockElements.Contains(elementSetup))
            {
                _unlockElements.Add(elementSetup);
                AppendUnlockElement?.Invoke(elementSetup);
            }
        }

        public List<ElementSetup> GetFutureElements() => _futureElements;

        public void AddFutureElements(ElementSetup elementSetup)
        {
            if (!_futureElements.Contains(elementSetup))
                _futureElements.Add(elementSetup);
        }

        public void RemoveFutureElements(ElementSetup elementSetup)
        {
            if (_futureElements.Contains(elementSetup))
            {
                _futureElements.Remove(elementSetup);
                RemoveFutureElement?.Invoke(elementSetup);
            }
        }
    }
}