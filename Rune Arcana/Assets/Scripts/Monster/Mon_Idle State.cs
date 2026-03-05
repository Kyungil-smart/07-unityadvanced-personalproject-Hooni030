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
        _monster.canMove = true;
        _monster.isMove = false;
    }

    public void Update()
    {
        if(_monster.HP <= 0)
            _monster.ChangeState(_monster.Dead);
        
        if (_monster.attack)
            _monster.ChangeState(_monster.Attack);
        
        if (_monster.isHurt)
            _monster.ChangeState(_monster.Hurt);
        
        if (_monster.isMove)
            _monster.ChangeState(_monster.Move);
        
    }

    public void Exit()
    {
    }
}
