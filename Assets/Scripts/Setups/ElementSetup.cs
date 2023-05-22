using Core;
using System;
using Services;
using UnityEngine;
using System.Collections.Generic;

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

        public void ProcessOpenElements()
        {
            if (GetStateElement() == StateElement.Open)
            {
                _sessionDataController.AddUnlockElements(this);
                foreach (var elementSetup in parentElements)
                {
                    elementSetup.Init(_sessionDataController, _dataHelper);
                    elementSetup.ProcessOpenElements();
                }
            }
            else
                _sessionDataController.AddFutureElements(this);
        }


        public ElementSetup CheckCreateNewElement(List<ElementSetup> currentElements)
        {
            if (currentElements.Count == 0) return null;

            foreach (ElementSetup element in parentElements)
                if (element.ChildElementsMatch(currentElements))
                    return element;

            return null;
        }

        private bool ChildElementsMatch(List<ElementSetup> containsElementSetups)
        {
            if (childElements.Count != containsElementSetups.Count)
                return false;

            HashSet<ElementSetup> childElementSet = new HashSet<ElementSetup>(childElements);
            foreach (ElementSetup containsElementSetup in containsElementSetups)
                if (!childElementSet.Contains(containsElementSetup))
                    return false;

            return true;
        }
    }
}