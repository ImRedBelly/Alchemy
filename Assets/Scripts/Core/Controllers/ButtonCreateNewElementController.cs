﻿using Zenject;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class ButtonCreateNewElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonCreateNewElement;

        [Inject] private SessionDataController _sessionDataController;
        [Inject] private DataHelper _dataHelper;

        private void Start()
        {
            _buttonCreateNewElement.onClick.AddListener(CreateNewElement);
        }

        private void CreateNewElement()
        {
            if (_sessionDataController.BarIsEmpty) return;
            var elements = _sessionDataController.GetElementInBar();
            var newElement = elements[0].CheckCreateNewElement(elements);
            if (newElement != null)
            {
                newElement.OpenElement();
                _dataHelper.SaveElementsDataModel();
            }

            var elementsName = " ";
            foreach (var elementSetup in elements)
                elementsName += elementSetup.keyElement;

            Debug.LogError(elementsName + ": " + newElement);
        }
    }
}