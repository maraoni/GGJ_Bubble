using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Playing : State
{
    public GameObject mainCamera;

    List<Guest> guests = new();

    [SerializeField] Controller controller;
    [SerializeField] GameObject playingUI;

    Vector3 SpawnPosition;

    private void Awake()
    {
        SpawnPosition = controller.transform.position;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        CameraController.Instance.ClearFocusPoint();


        playingUI?.SetActive(true);
        mainCamera.SetActive(true);
        Guest[] st = FindObjectsByType<Guest>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        guests.Clear();
        guests = st.ToList();

        foreach (Guest gue in st)
        {
            gue.InitializeGuest();
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GamesManager.Instance.SwitchState<Outro>();
            return;
        }

        CameraController.Instance.UpdateCamera();
        controller.UpdatePlayer();

        bool allAreSatisfied = true;

        foreach (Guest g in guests)
        {
            g.UpdateGuest();
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

    public void PlayCorkAnimation()
    {
        controller.PopCork();
    }

    public void ResetPlayerPosition()
    {
        controller.ResetBottle();
        controller.transform.position = SpawnPosition;
    }

    public override void OnExit()
    {
        base.OnExit();
        mainCamera.SetActive(false);
        playingUI.SetActive(false);
    }
}
