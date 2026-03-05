using UnityEngine;

public class Mon_IdleState : IState
{
    private MonsterControlller _monster;

    public Mon_IdleState(MonsterControlller monster)
    {
        _monster = monster;
    }
    
    public void Enter()
    {
        _monster._canMove = true;
        _monster._isMove = false;
    }

    public void Update()
    {
        if (_monster._isMove)
        {
            _monster.ChangeState(_monster.Move);
        }
        if (_monster._canAttack)
        {
            _monster.ChangeState(_monster.Attack);
        }
        if(_monster.HP < 0)
        {
            _monster.ChangeState(_monster.Dead);
        }
    }

    public void Exit()
    {
    }
}
