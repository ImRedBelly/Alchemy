using System;
using Setups;
using Core.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Controllers
{
    public class BarElementController : MonoBehaviour
    {
        public bool ElementEmpty => _elementSetup == null;
        [SerializeField] private Button _buttonRemoveElement;
        [SerializeField] private BarElementsView _barElementsView;
        [SerializeField] private Sprite _defaultIconElement;

        [Inject] private SessionDataController _sessionDataController;
        private ElementSetup _elementSetup;


        private void Start()
        {
            _sessionDataController.AppendButtonBarElementController(this);
            _buttonRemoveElement.onClick.AddListener(RemoveElementSetup);
        }

        private void RemoveElementSetup()
        {
            if (ElementEmpty) return;
            _sessionDataController.OnRemoveElement(_elementSetup);
            UpdateElementSetup(null);
        }

        public void UpdateElementSetup(ElementSetup elementSetup)
        {
            _elementSetup = elementSetup;
            var icon = _elementSetup == null ? _defaultIconElement : _elementSetup.iconElement;
            _barElementsView.UpdateIconElement(icon);
        }
    }
}