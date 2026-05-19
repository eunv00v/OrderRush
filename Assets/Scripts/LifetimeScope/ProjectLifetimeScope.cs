using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;
using Services.UpdateService;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] UpdateSubscriptionService _updateService;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();
        builder.RegisterComponent(_updateService)
               .As<IUpdateSubscriptionService>();
        builder.Register<ResourcesLoaderService>(Lifetime.Singleton)
               .As<IResourcesLoaderService>();
        builder.Register<UserDataService>(Lifetime.Singleton)
               .As<IUserDataService>();
        builder.RegisterEntryPoint<AppBootstrap>();
    }

}