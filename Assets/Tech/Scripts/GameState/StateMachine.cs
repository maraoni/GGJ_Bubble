using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> states = new();
    public State CurrentState;
    public void InitializeStateMachine()
    {
        State[] states = FindObjectsByType<State>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    public virtual void SwitchState<T>() where T : State
    {
        foreach(var state in states)
        {
            if(state is T)
            {
                CurrentState = state;
            }
        }
    }
}
