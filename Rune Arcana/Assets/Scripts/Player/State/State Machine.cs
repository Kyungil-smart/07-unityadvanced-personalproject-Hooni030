using UnityEngine;

public class StateMachine
{
    public IState _currentState;

    public void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        state?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}