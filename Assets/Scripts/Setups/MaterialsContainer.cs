using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "MaterialsContainer", menuName = "Containers/MaterialsContainer")]
    public class MaterialsContainer : ScriptableObject
    {
        public Material defaultMaterial;
        public Material colorMaterial;
    }
}