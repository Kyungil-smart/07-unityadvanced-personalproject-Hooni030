using UnityEngine;

public class MoveState : IState
{
    private PlayerController _player;
    
    public MoveState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
        // 움직임이 없으면 Idle 상태로 전환
        if (_player.MoveInput.x == 0)
        {
            _player.ChangeState(_player.Idle);
        }
        if (_player.TeleInput)
        {
            _player.ChangeState(_player.Teleport);
        }
    }

    public void Exit()
    {
    }
}