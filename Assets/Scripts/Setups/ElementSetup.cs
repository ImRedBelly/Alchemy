using System;
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