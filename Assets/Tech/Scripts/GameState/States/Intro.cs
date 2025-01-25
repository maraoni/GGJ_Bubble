using UnityEngine;

public class Intro : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        GamesManager.Instance.SwitchState<CustommersEnter>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
