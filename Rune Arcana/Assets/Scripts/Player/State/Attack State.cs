using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    
    private PlayerController _player;
    
    public AttackState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        _player._animator.SetBool(Attack, _player.AttackInput);
        _player.CanMove = false;
        _player.IsMove = false;
    }

    public void Update()
    {
        _player._animator.SetBool(Attack, false);
        
        if (!_player.AttackInput)
        {
            _player.ChangeState(_player.Idle);
        }
    }

    public void Exit()
    {
    }
}
