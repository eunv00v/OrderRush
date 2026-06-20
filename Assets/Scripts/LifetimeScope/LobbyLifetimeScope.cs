using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<LobbyView>();
        builder.RegisterEntryPoint<LobbyPresenter>();
    }
}
