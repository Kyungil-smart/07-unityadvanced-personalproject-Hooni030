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
        if (_player.SkillInput)
        {
            _player.ChangeState(_player.Skill);
        }
    }

    public void Exit()
    {
    }
}
