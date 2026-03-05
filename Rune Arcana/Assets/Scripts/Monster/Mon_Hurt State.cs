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
        _monster._canHurt = false;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        
    }
}
