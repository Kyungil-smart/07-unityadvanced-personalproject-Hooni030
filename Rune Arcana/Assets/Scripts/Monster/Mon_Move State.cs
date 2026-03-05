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
        _monster.isMove = true;
        _monster._animator.SetBool("Move", true);
    }

    public void Update()
    {
        if (_monster.HP <= 0)
            _monster.ChangeState(_monster.Dead);
        
        if (_monster.attack)
            _monster.ChangeState(_monster.Attack);
        
        if (_monster.isHurt)
            _monster.ChangeState(_monster.Hurt);
    }

    public void Exit()
    {
        _monster._animator.SetBool("Move", false);
    }
}