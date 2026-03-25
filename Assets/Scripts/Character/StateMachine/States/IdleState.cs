using UnityEngine;

public class IdleState : ICharacterState
{
    public void Enter()
    {
        Debug.Log("Idle 상태 진입");
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
