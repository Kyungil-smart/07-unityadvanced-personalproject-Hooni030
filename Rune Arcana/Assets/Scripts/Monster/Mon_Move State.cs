using UnityEngine;

public class Mon_MoveState : IState
{
    private MonsterControlller _monster;

    public Mon_MoveState(MonsterControlller monster)
    {
        _monster = monster;
    }
    public void Enter()
    {
        _monster._isMove = true;
        _monster._animator.SetBool("Move", true);
    }

    public void Update()
    {
        if (_monster.HP <= 0)
            _monster.ChangeState(_monster.Dead);
        
        else if (_monster._canAttack)
            _monster.ChangeState(_monster.Attack);
        
        else if (!_monster._canHurt)
            _monster.ChangeState(_monster.Hurt);
        
        else if (!_monster._isMove)
            _monster.ChangeState(_monster.Idle);
    }

    public void Exit()
    {
        _monster._animator.SetBool("Move", false);
    }
}