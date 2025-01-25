using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> states = new();
    public State CurrentState;
    public void InitializeStateMachine()
    {
        State[] st = FindObjectsByType<State>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        states = st.ToList();

        SwitchState<Playing>();
    }
    public virtual void SwitchState<T>() where T : State
    {
        foreach(State state in states)
        {
            if(state is T)
            {
                CurrentState?.OnExit();
                CurrentState = state;
                CurrentState.OnEnter();
            }
        }
    }

    private void Update()
    {
        CurrentState?.OnUpdate();
    }
}
