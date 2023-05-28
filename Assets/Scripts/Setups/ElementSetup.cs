using Core;
using System;
using Services;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace Setups
{
    [Serializable, CreateAssetMenu(fileName = "AlchemyGraph", menuName = "Graphs/ElementSetup")]
    public class ElementSetup : SerializedScriptableObject
    {
        public string keyElement;
        [PreviewField(50)] public Sprite iconElement;
        [OdinSerialize] public Dictionary<ElementSetup, int> parentElements;
        [OdinSerialize] public Dictionary<ElementSetup, int> childElements;


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
                foreach (var elementSetup in parentElements.Keys)
                {
                    elementSetup.Init(_sessionDataController, _dataHelper);
                    elementSetup.ProcessOpenElements();
                }
            }
            else
                _sessionDataController.AddFutureElements(this);
        }


        public ElementSetup CheckCreateNewElement(Dictionary<ElementSetup, int> currentElements)
        {
            if (currentElements.Count == 0) return null;

            foreach (ElementSetup element in parentElements.Keys)
                if (element.ChildElementsMatch(currentElements))
                    return element;

            return null;
        }

        private bool ChildElementsMatch(Dictionary<ElementSetup, int> containsElementSetups)
        {
            if (childElements.Count != containsElementSetups.Count)
                return false;

            int childValuesSum = childElements.Values.Sum();
            int containsValuesSum = containsElementSetups.Values.Sum();

            if (childValuesSum != containsValuesSum)
                return false;

            HashSet<ElementSetup> childKeysSet = new HashSet<ElementSetup>(childElements.Keys);

            foreach (ElementSetup containsKey in containsElementSetups.Keys)
                if (!childKeysSet.Contains(containsKey))
                    return false;

            return true;
        }
    }
}