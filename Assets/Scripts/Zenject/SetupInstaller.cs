using Setups;
using UnityEngine;

namespace Zenject
{
    public class SetupInstaller : MonoInstaller
    {
        [SerializeField] private MaterialsContainer _materialsContainer;

        public override void InstallBindings()
        {
            Container.Bind<MaterialsContainer>().FromInstance(_materialsContainer).AsSingle();
        }
    }
}