using Services;
using Zenject;

namespace Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SessionDataController>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataHelper>().AsSingle();
        }
    }
}