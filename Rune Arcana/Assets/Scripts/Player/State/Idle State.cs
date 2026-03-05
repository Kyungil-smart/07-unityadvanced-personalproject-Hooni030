using UnityEngine;

public class IdleState : IState
{
    private static readonly int Move = Animator.StringToHash("Move");
    
    private PlayerController _player;
    
    public IdleState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        _player.CanMove = true;
        _player.AttackInput = false;
        _player._animator.SetBool(Move, false);
    }

    public void Update()
    {
        // 움직임이 있으면 Move 상태로 전환
        if (_player.MoveInput != Vector2.zero)
            _player.ChangeState(_player.Move);
        
        if (_player.AttackInput)
            _player.ChangeState(_player.Attack);
        
        if(_player.isHit)
            _player.ChangeState(_player.Hit);
        
        if (_player.isDead)
            _player.ChangeState(_player.Dead);
    }

    public void Exit()
    {
    }
}
