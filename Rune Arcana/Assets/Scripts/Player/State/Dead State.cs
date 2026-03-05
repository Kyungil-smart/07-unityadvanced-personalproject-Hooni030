using UnityEngine;

public class DeadState : IState
{
    private PlayerController _player;

    public DeadState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.CanMove = false;
        _player._animator.SetTrigger("Dead");
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
    }
}
