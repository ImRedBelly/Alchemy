using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataModels
{
    public class ElementsDataModel
    {
        [JsonProperty] private Dictionary<string, ElementData> _elementData
            = new Dictionary<string, ElementData>();

        public StateElement GetStateElement(string keyElement)
            => GetElementData(keyElement).stateElement;

        public StateElement GetHintStateElement(string keyElement, string keyHintElement)
            => GetElementData(keyElement).GetStateElement(keyHintElement);

        public void SetStateElement(string keyElement, StateElement stateElement)
        {
            var data = GetElementData(keyElement);
            data.stateElement = stateElement;
        }

        public void SetHintStateElement(string keyElement, string keyHintElement, StateElement stateElement)
        {
            GetElementData(keyElement).SetStateElement(keyHintElement, stateElement);
        }

        private ElementData GetElementData(string keyElement)
        {
            if (!_elementData.ContainsKey(keyElement))
                _elementData.Add(keyElement, new ElementData() {stateElement = StateElement.Close});
            return _elementData[keyElement];
        }
    }

    [Serializable]
    public class ElementData
    {
        public StateElement stateElement;

        [JsonProperty] private Dictionary<string, StateElement> _hintElementData
            = new Dictionary<string, StateElement>();

        public StateElement GetStateElement(string keyElement)
        {
            CheckHintElement(keyElement);
            return _hintElementData[keyElement];
        }

        public void SetStateElement(string keyElement, StateElement stateElement)
        {
            CheckHintElement(keyElement);
            _hintElementData[keyElement] = stateElement;
        }

        private void CheckHintElement(string keyElement)
        {
            if (!_hintElementData.ContainsKey(keyElement))
                _hintElementData.Add(keyElement, StateElement.Close);
        }
    }
}