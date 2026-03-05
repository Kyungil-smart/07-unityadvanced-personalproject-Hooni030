using UnityEngine;

public class MoveState : IState
{
    private static readonly int Move = Animator.StringToHash("Move");
    
    private PlayerController _player;
    
    public MoveState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        _player.IsMove = true;
        _player.CanMove = true;
        _player._animator.SetBool(Move, true);
    }

    public void Update()
    {
        // 움직임이 없으면 Idle 상태로 전환
        if (_player.MoveInput == Vector2.zero)
        {
            _player.ChangeState(_player.Idle);
        }

        if (_player.AttackInput)
        {
            _player.ChangeState(_player.Attack);
        }
    }

    public void Exit()
    {
    }
}