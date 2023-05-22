using TMPro;
using UnityEngine;

namespace Core.Views
{
    public class HintPointView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textHintPoint;
        public void UpdateTextHintPoint(string hintAmount) => _textHintPoint.text = hintAmount;
    }
}