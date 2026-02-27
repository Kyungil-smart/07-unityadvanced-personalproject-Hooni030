using UnityEngine;

public class TeleportState : IState
{
    private PlayerController _player;
    
    public TeleportState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
        if (_player.TeleInput)
        {
            _player.ChangeState(_player.Teleport);
        }
    }

    public void Exit()
    {
    }
}
