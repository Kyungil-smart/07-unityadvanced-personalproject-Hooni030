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
    }

    public void Exit()
    {
    }
}
