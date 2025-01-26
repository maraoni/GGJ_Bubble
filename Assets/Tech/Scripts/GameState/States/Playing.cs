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

        CustommersEnter s = GamesManager.Instance.GetState<CustommersEnter>() as CustommersEnter;
        s.GetLevel();
        if (s.GetLevel() == 4)
        {
            SoundManager.Instance.PlaySong(SoundManager.Songs.Disco);
        }
        else if (s.GetLevel() == 5)
        {
            SoundManager.Instance.StopSong();
        }
        else
        {
            SoundManager.Instance.PlaySong(SoundManager.Songs.Playing);
        }


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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamesManager.Instance.SwitchState<MainMenu>();
            return;
        }

        controller.UpdatePlayer();

        if (guests.Count > 0)
        {
            bool allAreSatisfied = true;

            foreach (Guest g in guests)
            {
                g.UpdateGuest();
                if (!g.IsSatisfied)
                {
                    allAreSatisfied = false;
                }
            }

            if (allAreSatisfied)
            {
                GamesManager.Instance.SwitchState<CustommersEnter>();
            }
        }
    }
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
        CameraController.Instance.UpdateCamera();
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
        guests.Clear();
        mainCamera.SetActive(false);
        playingUI.SetActive(false);
    }
}
