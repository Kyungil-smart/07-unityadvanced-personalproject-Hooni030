using UnityEngine;

public class AttackState : IState
{
    private PlayerController _player;
    
    public AttackState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
        if (_player.AttackInput)
        {
            _player.ChangeState(_player.Attack);
        }
    }

    public void Exit()
    {
    }
}
