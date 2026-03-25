using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] GameObject _playerPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        // Character State Machine
        builder.RegisterEntryPoint<CharacterStateMachine>(Lifetime.Scoped)
               .AsImplementedInterfaces();
        builder.Register<IdleState>(Lifetime.Scoped);
        builder.Register<MoveState>(Lifetime.Scoped);
        builder.Register<WorkState>(Lifetime.Scoped);

        builder.RegisterBuildCallback(container =>
        {
            container.InjectGameObject(_playerPrefab); // Addressables 로드 예정
        });
    }
}