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
        if(_monster.HP <= 0)
            _monster.ChangeState(_monster.Dead);
        
        else if (_monster._canAttack)
            _monster.ChangeState(_monster.Attack);
        
        else if (_monster._isHurt)
            _monster.ChangeState(_monster.Hurt);
        
        else if (_monster._isMove)
            _monster.ChangeState(_monster.Move);
        
    }

    public void Exit()
    {
    }
}
