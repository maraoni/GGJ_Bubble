using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : State
{
    public GameObject MainMenuCamera;
    public GameObject MainUI;

    [SerializeField] Image TransitionImage;
    bool hasStarted = false;
    public override void OnEnter()
    {
        MainUI.SetActive(true);
        MainMenuCamera.SetActive(true);

        TransitionImage.color = Color.clear;
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        MainUI.SetActive(false);
        MainMenuCamera.SetActive(false);

        TransitionImage.color = Color.clear;

        base.OnExit();
    }

    public void StartGame()
    {
        if (!hasStarted)
        {
            StartCoroutine(StartingGame());
        }
    }

    public IEnumerator StartingGame()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, t);
            yield return null;
        }
        TransitionImage.color = Color.black;
        yield return new WaitForSeconds(1);

        GamesManager.Instance.SwitchState<Intro>();
        hasStarted = false;
    }
}
