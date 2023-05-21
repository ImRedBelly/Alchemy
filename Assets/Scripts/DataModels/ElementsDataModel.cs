using System;
using System.Linq;
using System.Collections.Generic;

namespace DataModels
{
    public class ElementsDataModel
    {
        private List<ElementData> _elementData = new List<ElementData>();

        public StateElement GetStateElement(string keyElement) => GetElement(keyElement).stateElement;

        public void SetStateElement(string keyElement, StateElement stateElement)
        {
            var element = GetElement(keyElement);
            element.stateElement = stateElement;
        }


        private ElementData GetElement(string keyElement)
        {
            var element = _elementData.FirstOrDefault(x => x.keyElement == keyElement);
            if (element == null)
            {
                element = new ElementData() {keyElement = keyElement, stateElement = StateElement.Close};
                _elementData.Add(element);
            }

            return element;
        }
    }

    [Serializable]
    public class ElementData
    {
        public string keyElement;
        public StateElement stateElement;
    }
}