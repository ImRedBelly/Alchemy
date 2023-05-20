using UnityEngine;
using Core.Controllers;
using OdinNode.ElementsGraph;

namespace Core
{
    public class ElementsCreator : MonoBehaviour
    {
        [SerializeField] private UnlockElementController _unlockElementController;
        [SerializeField] private AlchemyGraph _alchemyGraph;
        [SerializeField] private Transform _parentElementController;

        private void Start()
        {
            foreach (var elementSetup in _alchemyGraph.nodes)
            {
                var setup = (elementSetup as ElementDataNode)?.elementSetup;
                var elementController = Instantiate(_unlockElementController, _parentElementController);
                elementController.UpdateElementSetup(setup);
            }
        }
    }
}