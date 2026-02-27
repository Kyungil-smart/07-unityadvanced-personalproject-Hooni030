using UnityEngine;

public class IdleState : IState
{
    private PlayerController _player;
    
    public IdleState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
        // 움직임이 있으면 Move 상태로 전환
        if (_player.MoveInput.x != 0)
        {
            _player.ChangeState(_player.Move);
        }
        if (_player.AttackInput)
        {
            _player.ChangeState(_player.Attack);
        }
        if (_player.TeleInput)
        {
            _player.ChangeState(_player.Teleport);
            return;
        }
    }

    public void Exit()
    {
    }
}
