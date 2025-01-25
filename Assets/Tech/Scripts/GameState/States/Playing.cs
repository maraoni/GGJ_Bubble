using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Playing : State
{
    [SerializeField] GameObject mainCamera;

    List<Guest> guests = new();

    [SerializeField] Controller controller;
    public override void OnEnter()
    {
        base.OnEnter();
        Guest[] st = FindObjectsByType<Guest>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        guests.Clear();
        guests = st.ToList();
        mainCamera.SetActive(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.UpdatePlayer();

        bool allAreSatisfied = true;
        foreach (Guest g in guests)
        {
            if(!g.IsSatisfied)
            {
                allAreSatisfied = false;
            }
        }

        if(allAreSatisfied)
        {
            GamesManager.Instance.SwitchState<CustommersEnter>();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        mainCamera.SetActive(false);
    }
}
