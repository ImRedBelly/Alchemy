using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class BarElementsView : MonoBehaviour
    {
        [SerializeField] private Image iconElement;

        public void UpdateIconElement(Sprite newIcon) => iconElement.sprite = newIcon;
    }

}