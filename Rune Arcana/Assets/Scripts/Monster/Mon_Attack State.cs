using UnityEngine;

public class Mon_AttackState : IState
{
    private MonsterControlller _monster;

    public Mon_AttackState(MonsterControlller monster)
    {
        _monster = monster;
    }
    public void Enter()
    {
        _monster._canMove = false;
        _monster._animator.SetBool("Attack", true);
    }

    public void Update()
    {
        if (_monster._canMove)
        {
            _monster.ChangeState(_monster.Move);
        }
        else if (!_monster._isMove)
        {
            _monster.ChangeState(_monster.Idle);
        }
    }

    public void Exit()
    {
        _monster._animator.SetBool("Attack", false);
    }
}