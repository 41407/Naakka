using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class CrowInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle();
            Container.Bind<Rigidbody2D>().FromComponentInParents().AsTransient();
            Container.Bind<SpriteRenderer>().FromComponentInParents().AsTransient();
            Container.Bind<CrowController>().FromComponentInParents().AsTransient();
            Container.Bind<CooldownTimer>().FromNewComponentSibling().AsTransient();
        }
    }
}
