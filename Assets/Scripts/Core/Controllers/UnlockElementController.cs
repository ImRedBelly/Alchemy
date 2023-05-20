﻿using System;
using Core.Views;
using Setups;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class UnlockElementController : MonoBehaviour
    {
        [SerializeField] private Button _buttonChooseElement;
        [SerializeField] private UnlockElementView _elementView;

        private ElementSetup _elementSetup;

        private void Start()
        {
            _buttonChooseElement.onClick.AddListener(AddElementToBar);
        }

        public void UpdateElementSetup(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            _elementView.UpdateNameElement(_elementSetup.keyElement);
            _elementView.UpdateIconElement(_elementSetup.iconElement);
        }

        private void AddElementToBar()
        {
            if (!FindObjectOfType<BarController>().BarIsFull)
                FindObjectOfType<BarController>().AddElement(_elementSetup);
        }
    }
}