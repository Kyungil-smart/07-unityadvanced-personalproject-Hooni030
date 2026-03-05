using UnityEngine;

public class StateMachine
{
    public IState _currentState;

    public void ChangeState(IState state)
    {
        state?.Exit();
        _currentState = state;
        state?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}