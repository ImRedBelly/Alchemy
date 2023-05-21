using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Core;
using Services;

namespace Setups
{
    [Serializable, CreateAssetMenu(fileName = "AlchemyGraph", menuName = "Graphs/ElementSetup")]
    public class ElementSetup : ScriptableObject
    {
        public string keyElement;
        public Sprite iconElement;
        public List<ElementSetup> parentElements;
        public List<ElementSetup> childElements;


        private SessionDataController _sessionDataController;
        private DataHelper _dataHelper;

        public void Init(SessionDataController sessionDataController, DataHelper dataHelper)
        {
            _sessionDataController = sessionDataController;
            _dataHelper = dataHelper;
        }

        public void OpenElement()
        {
            _dataHelper.ElementsDataModel.SetStateElement(keyElement, StateElement.Open);
            _sessionDataController.RemoveFutureElements(this);
            _sessionDataController.AddUnlockElements(this);
        }

        public StateElement GetStateElement() => _dataHelper.ElementsDataModel.GetStateElement(keyElement);

        public void CheckOpenElements()
        {
            if (GetStateElement() == StateElement.Open)
            {
                _sessionDataController.AddUnlockElements(this);
                foreach (var elementSetup in parentElements)
                {
                    elementSetup.Init(_sessionDataController, _dataHelper);
                    elementSetup.CheckOpenElements();
                }
            }
            else
                _sessionDataController.AddFutureElements(this);
        }

        public ElementSetup CheckCreateNewElement(List<ElementSetup> currentElements)
        {
            if (currentElements.Count == 0) return null;

            foreach (ElementSetup element in parentElements)
                if (ListEquals(element, currentElements))
                    return element;

            return null;
        }

        private bool ListEquals(ElementSetup elementSetup, List<ElementSetup> containsElementSetups)
        {
            if ((elementSetup == null || containsElementSetups == null)
                || (elementSetup.childElements.Count != containsElementSetups.Count))
                return false;

            for (int i = 0; i < elementSetup.childElements.Count; i++)
                if (!elementSetup.childElements.Contains(containsElementSetups[i]))
                    return false;

            return true;
        }
    }
}