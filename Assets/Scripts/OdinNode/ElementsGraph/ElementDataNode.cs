using Setups;
using UnityEngine;
using UnityEditor;
using OdinNode.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;

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
                elementSetup.parentElements = new Dictionary<ElementSetup, int>();
                elementSetup.childElements = new Dictionary<ElementSetup, int>();
            }

            EditorUtility.SetDirty(elementSetup);
            var parentPort = GetOutputPort(nameof(outputElementNode)).GetConnections();
            Dictionary<ElementSetup, int> setupsParent = new Dictionary<ElementSetup, int>();
            foreach (var nodePort in parentPort)
            {
                var amountSetup = 1;
                var node = nodePort.node as ElementDataNode;
                var setup = node.elementSetup;
                if (setupsParent.ContainsKey(setup))
                    setupsParent[setup]++;
                else
                {
                    if (elementSetup.childElements.ContainsKey(setup))
                        amountSetup = elementSetup.childElements[setup];
                    setupsParent.Add(setup, amountSetup);
                }
            }

            elementSetup.parentElements = setupsParent;


            var icon = AssetDatabase.LoadAssetAtPath<Sprite>(
                $"Assets/Sprites/IconsElement/{elementSetup.keyElement}.png");


            var childPort = GetInputPort(nameof(inputElementNode)).GetConnections();
            Dictionary<ElementSetup, int> setupsChild = new Dictionary<ElementSetup, int>();
            foreach (var nodePort in childPort)
            {
                var amountSetup = 1;
                var node = nodePort.node as ElementDataNode;
                var setup = node.elementSetup;
                if (setupsChild.ContainsKey(setup))
                    setupsChild[setup]++;
                else
                {
                    if (elementSetup.childElements.ContainsKey(setup))
                        amountSetup = elementSetup.childElements[setup];
                    setupsChild.Add(setup, amountSetup);
                }
            }


            elementSetup.childElements = setupsChild;
            name = elementSetup.keyElement;
            elementSetup.iconElement = icon;
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