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
    }

    public void Update()
    {
        if (_player.isHit)
        {
            _player.ChangeState(_player.Hit);
        }
    }

    public void Exit()
    {
    }
}
