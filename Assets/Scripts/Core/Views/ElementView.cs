using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class ElementView : MonoBehaviour
    {
        [SerializeField] private Image iconElement;
        [SerializeField] private TextMeshProUGUI nameElement;

        public void UpdateIconElement(Sprite newIcon) => iconElement.sprite = newIcon;
        public void UpdateNameElement(string newName) => nameElement.text = newName;
        public void UpdateColorIcon(Color color) => iconElement.color = color;
    }
}