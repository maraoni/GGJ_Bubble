using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustommersEnter : State
{
    [SerializeField] List<GameObject> Levels = new();
    [SerializeField] List<string> LevelDescriptions = new();
    [SerializeField] GameObject mainCamera;

    [SerializeField] GameObject Guest;
    [SerializeField] Transform GuestSpawn;

    [SerializeField] GameObject NextStageButton;
    [SerializeField] GameObject CustommersEnterUI;

    [SerializeField] Image TransitionImage;
    [SerializeField] MachineText LevelDescriptionText;
    int CurrentLevel = 0;

    public void ResetGame()
    {
        CurrentLevel = 0;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        

        if (CurrentLevel + 1 > Levels.Count)
        {
            GamesManager.Instance.SwitchState<MainMenu>();
            return;
        }

        TransitionImage.color = Color.clear;
        mainCamera.SetActive(true);
        NextStageButton.SetActive(false);
        CustommersEnterUI.SetActive(true);

        StartCoroutine(NextStage());
    }
    public IEnumerator NextStage()
    {
        LevelDescriptionText.gameObject.SetActive(true);
        LevelDescriptionText.DisplayText(LevelDescriptions[CurrentLevel], false);

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, t);
        }

        TransitionImage.color = Color.black;

        yield return new WaitForSeconds(3);
        NextStageButton.SetActive(true);
    }

    public void SpawnNextLevel()
    {
        NextStageButton.SetActive(false);
        foreach(GameObject g in Levels)
        {
            g.SetActive(false);
        }
        Levels[CurrentLevel].SetActive(true);
        StartCoroutine(StartNextStage());
    }

    public IEnumerator StartNextStage()
    {

        LevelDescriptionText.gameObject.SetActive(false);

        Playing s = GamesManager.Instance.GetState<Playing>() as Playing;
        s.ResetPlayerPosition();

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            TransitionImage.color = Color.Lerp(Color.black, Color.clear, t);
            yield return null;
        }

        s.PlayCorkAnimation();

        yield return new WaitForSeconds(2.0f);
        TransitionImage.color = Color.clear;
        CurrentLevel++;
        GamesManager.Instance.SwitchState<Playing>();
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();

        mainCamera.SetActive(false);

        TransitionImage.color = Color.clear;
        NextStageButton.SetActive(false);
        CustommersEnterUI.SetActive(false);

    }
}
