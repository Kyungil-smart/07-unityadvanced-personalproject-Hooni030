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
    }

    public void Update()
    {
        if (_player.isDead)
        {
            _player.ChangeState(_player.Dead);
        }
    }

    public void Exit()
    {
    }
}
