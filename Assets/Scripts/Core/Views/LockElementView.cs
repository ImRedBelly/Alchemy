using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class LockElementView : MonoBehaviour
    {
        [SerializeField] private Image iconElement;

        public void UpdateIconElement(Sprite newIcon) => iconElement.sprite = newIcon;
    }
}