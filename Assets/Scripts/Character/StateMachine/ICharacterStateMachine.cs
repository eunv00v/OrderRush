public interface ICharacterStateMachine
{
    ICharacterState CurrentState { get; }
    void ChangeState(ICharacterState newState);
}
