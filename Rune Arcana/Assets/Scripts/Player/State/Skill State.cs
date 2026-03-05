using UnityEngine;

public class SkillState : IState
{
    private PlayerController _player;
    
    public SkillState(PlayerController player)
    {
        _player = player;
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
