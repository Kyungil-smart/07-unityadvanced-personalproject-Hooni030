using UnityEngine;

public class Mon_HurtState : IState
{
    private MonsterControlller _monster;

    public Mon_HurtState(MonsterControlller monster)
    {
        _monster = monster;
    }

    public void Enter()
    {
        _monster._canMove = false;
        _monster._animator.SetBool("Hurt", true);
        _monster._isHurt = true;
    }

    public void Update()
    {
        if (_monster.HP <= 0)
        {
            _monster.ChangeState(_monster.Dead);
        }

        if (!_monster._isHurt)
        {
            _monster.ChangeState(_monster.Idle);
        }
    }

    public void Exit()
    {
        _monster._animator.SetBool("Hurt", false);   
    }
}
