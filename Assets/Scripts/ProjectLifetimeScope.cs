using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;
using Services.UpdateService;
using UnityEngine.SceneManagement;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] UpdateSubscriptionService _updateService;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();
        builder.RegisterComponent(_updateService)
               .AsImplementedInterfaces()
               .AsSelf();
        builder.RegisterEntryPoint<Launcher>();
    }

}