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
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}