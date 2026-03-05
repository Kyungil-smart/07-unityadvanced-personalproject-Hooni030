using UnityEngine;

public class HitState : IState
{
    private PlayerController _player;
    
    public HitState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        _player.CanMove = false;
        _player._animator.SetTrigger("Hit");
    }

    public void Update()
    {
        if(!_player.IsMove)
            _player.ChangeState(_player.Idle);
        
        if(_player.isDead)
            _player.ChangeState(_player.Dead);
    }

    public void Exit()
    {
        _player.CanMove = true;
    }
}
