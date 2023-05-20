using Setups;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Core.Controllers
{
    public class BarController : MonoBehaviour
    {
        public bool BarIsFull => _elementControllers.FindAll(x => !x.ElementEmpty).Count >= 3;
        public bool BarIsEmpty => _elementControllers.FindAll(x => !x.ElementEmpty).Count == 0;

        [SerializeField] private Button _buttonCreateNewElement;
        [SerializeField] private List<BarElementController> _elementControllers;



        private void Start()
        {
            _buttonCreateNewElement.onClick.AddListener(CreateNewElement);
        }

        private void CreateNewElement()
        {
            if (BarIsEmpty) return;
            var currentElements = _elementControllers.Where(x => !x.ElementEmpty)
                .Select(x => x.GetElementSetup())
                .ToList();

            var newElement = currentElements[0].CheckCreateNewElement(currentElements);

            var elements = " ";

            foreach (var elementSetup in currentElements)
            {
                elements += elementSetup.keyElement;
            }

            Debug.LogError(elements + ": " + newElement);
        }

        public void AddElement(ElementSetup elementSetup)
        {
            if (BarIsFull) return;
            var elementBar = _elementControllers.FirstOrDefault(x => x.ElementEmpty);
            if (elementBar != null) elementBar.UpdateElementSetup(elementSetup);
        }
    }
}