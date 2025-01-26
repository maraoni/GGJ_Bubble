using System.Collections;
using UnityEngine;

public class LoseState : State
{
    [SerializeField] GameObject LoseUI;
    [SerializeField] GameObject MainMenuButton;

    [SerializeField] string LoseText;
    [SerializeField] MachineText LoseMachineText;
    [SerializeField] GameObject LoseCamera;
    public override void OnEnter()
    {
        SoundManager.Instance.PlaySong(SoundManager.Songs.Lost);
        MainMenuButton.SetActive(false);
        LoseCamera.SetActive(true);
        LoseUI.gameObject.SetActive(true);
        StartCoroutine(EnableMainMenuButton());
        LoseMachineText.DisplayText(LoseText, false);
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public IEnumerator EnableMainMenuButton()
    {
        yield return new WaitForSeconds(2);
        MainMenuButton.SetActive(true);
    }

    public void GotoMainMenu()
    {
        GamesManager.Instance.SwitchState<MainMenu>();
    }
    public override void OnExit()
    {
        LoseMachineText.DisplayText("", false);
        LoseUI.gameObject.SetActive(false);
        MainMenuButton.SetActive(false);
        LoseCamera.SetActive(false);
        base.OnExit();
    }
}
