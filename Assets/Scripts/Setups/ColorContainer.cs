using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "ColorContainer", menuName = "Containers/ColorContainer")]
    public class ColorContainer : ScriptableObject
    {
        public Color defaultColor;
        public Color lockColor;
    }
}