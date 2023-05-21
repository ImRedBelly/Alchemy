using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataModels
{
    public class ElementsDataModel
    {
        [JsonProperty] private Dictionary<string, StateElement> _elementData = new Dictionary<string, StateElement>();

        public StateElement GetStateElement(string keyElement)
        {
            if (_elementData.ContainsKey(keyElement))
                return _elementData[keyElement];
            return StateElement.Close;
        }

        public void SetStateElement(string keyElement, StateElement stateElement)
        {
            if (_elementData.ContainsKey(keyElement))
                _elementData[keyElement] = stateElement;
            else
                _elementData.Add(keyElement, stateElement);
        }
    }
}