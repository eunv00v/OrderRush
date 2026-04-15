using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class StageLifetimeScope : LifetimeScope
{
    [SerializeField] Transform _root;

    protected override void Configure(IContainerBuilder builder)
    {
        // Services
        builder.Register<IOrderService, OrderService>(Lifetime.Singleton);
        builder.Register<ILevelProgressService, LevelProgressService>(Lifetime.Singleton);
        builder.Register<CustomerService>(Lifetime.Singleton).AsImplementedInterfaces();

        // Factories
        builder.Register<SpawnFactory>(Lifetime.Singleton);

        // Initiators
        builder.RegisterEntryPoint<GameInitiator>();
        builder.RegisterEntryPoint<PlayerInputHandler>();

        // Level 
        builder.Register<LevelFactory>(Lifetime.Scoped).WithParameter(_root);
        builder.Register<LevelContextPresenter>(Lifetime.Scoped)
                .As<ILevelContextPresenter>()
                .WithParameter(_root);
    }
}
