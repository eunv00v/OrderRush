using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class StageLifetimeScope : LifetimeScope
{
    [SerializeField] Transform _root;
    [SerializeField] Transform _injectObjectRoot;

    protected override void Configure(IContainerBuilder builder)
    {
        // Services
        builder.Register<IOrderService, OrderService>(Lifetime.Singleton);
        builder.Register<ILevelsDataService, LevelsDataService>(Lifetime.Singleton);

        // Factories
        builder.Register<SpawnFactory>(Lifetime.Singleton);

        // Initiators
        builder.RegisterEntryPoint<GameInitiator>();
        builder.RegisterEntryPoint<PlayerInputHandler>();

        // Grid
        builder.RegisterComponentInHierarchy<GridSystem>();

        // Level 
        builder.Register<LevelFactory>(Lifetime.Scoped).WithParameter(_root);
        builder.Register<LevelContextPresenter>(Lifetime.Scoped)
                .As<ILevelContextPresenter>()
                .WithParameter(_root);
    }
}
