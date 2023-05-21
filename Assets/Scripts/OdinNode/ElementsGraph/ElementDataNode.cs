using Setups;
using System.Linq;
using UnityEngine;
using UnityEditor;
using OdinNode.Scripts;
using Sirenix.OdinInspector;

namespace OdinNode.ElementsGraph
{
    public class ElementDataNode : Node
    {
        [ShowIf(nameof(_setupNull))] public string keyNewElement;

        [HideIf(nameof(_setupNull)), InlineEditor]
        public ElementSetup elementSetup;

        [field: Input(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never), SerializeField]
        private ElementDataNode inputElementNode;

        [field: Output(connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never), SerializeField]
        private ElementDataNode outputElementNode;

        private bool _setupNull => elementSetup == null;

        [Button]
        public void InitElement()
        {
            if (keyNewElement == "") return;

            if (elementSetup == null)
            {
                ElementSetup asset = CreateInstance<ElementSetup>();
                asset.keyElement = keyNewElement;
                asset.name = keyNewElement;

                AssetDatabase.CreateAsset(asset, $"Assets/Setups/Elements/{keyNewElement}.asset");
                elementSetup = asset;
            }

            EditorUtility.SetDirty(elementSetup);
            var parentPort = GetOutputPort(nameof(outputElementNode)).GetConnections();
            elementSetup.parentElements = parentPort.Select(x => x.node as ElementDataNode)
                .Select(x => x.elementSetup).ToList();


            var childPort = GetInputPort(nameof(inputElementNode)).GetConnections();
            elementSetup.childElements = childPort.Select(x => x.node as ElementDataNode)
                .Select(x => x.elementSetup).ToList();
            name = elementSetup.keyElement;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void OnValidate()
        {
            if (elementSetup != null)
                name = elementSetup.keyElement;
        }
    }
}