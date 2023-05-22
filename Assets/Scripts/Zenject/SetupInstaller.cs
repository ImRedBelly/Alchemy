using Setups;
using UnityEngine;

namespace Zenject
{
    public class SetupInstaller : MonoInstaller
    {
        [SerializeField] private ColorContainer colorContainer;

        public override void InstallBindings()
        {
            Container.Bind<ColorContainer>().FromInstance(colorContainer).AsSingle();
        }
    }
}