using VContainer.Unity;

public class CharacterStateMachine : ICharacterStateMachine, ITickable, IStartable
{
    public ICharacterState CurrentState { get; private set; }

    readonly IdleState _idleState;


    public CharacterStateMachine(IdleState idleState)
    {
        _idleState = idleState;
    }


    public void Start()
    {
        ChangeState(_idleState);
    }

    public void ChangeState(ICharacterState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Tick()
    {
        CurrentState?.Update();
    }
}
