using UnityEngine;

public class Mon_DeadState : IState
{
    private MonsterControlller _monster;

    public Mon_DeadState(MonsterControlller monster)
    {
        _monster = monster;
    }
    public void Enter()
    {
        Debug.Log("몬스터 사망");
        _monster.canMove = false;
        _monster.isHurt = true;
        _monster._animator.SetTrigger("Death");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}