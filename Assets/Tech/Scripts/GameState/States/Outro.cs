using System.Collections;
using UnityEngine;

public class Outro : State
{
    [SerializeField] GameObject OutroCamera;
    [SerializeField] Transform FinalCameraPosition;
    [SerializeField] Transform StartCameraPosition;
    [SerializeField] GameObject OutroObject;

    [SerializeField] MachineText UpperText;
    [SerializeField] MachineText LowerText;

    [SerializeField] GameObject MainMenuButton;
    const float MaxTime = 29.0f;
    float currentTime = 0;
    public override void OnEnter()
    {
        base.OnEnter();
        currentTime = 0;
        OutroObject.SetActive(true);
        MainMenuButton.SetActive(false);

        SoundManager.Instance.PlaySong(SoundManager.Songs.Outro);

        UpperText.DisplayText("", false);
        LowerText.DisplayText("", false);
        StartCoroutine(ExitSequence());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (currentTime < MaxTime)
        {
            OutroCamera.transform.position = Vector3.Lerp(StartCameraPosition.position, FinalCameraPosition.position, currentTime / MaxTime);
            currentTime += Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        OutroObject.SetActive(false);
        base.OnExit();
    }

    public IEnumerator ExitSequence()
    {
        yield return new WaitForSeconds(5);
        UpperText.DisplayText("And so it was that after a long night of mingling with plenty of bubbly", false);
        yield return new WaitForSeconds(4);
        LowerText.DisplayText("That all of the guests completly buzzed up, suddenly had their economic bubble pop", false);
        yield return new WaitForSeconds(6);
        UpperText.DisplayText("It matters not for you Bubba, the tips now makes you the richest in the room", false);
        yield return new WaitForSeconds(4);
        LowerText.DisplayText("And it was worth every drop", false);
        yield return new WaitForSeconds(4);
        UpperText.DisplayText("THANK YOU FOR PLAYING", false);
        yield return new WaitForSeconds(2);
        LowerText.DisplayText("AND THANK YOU GGJ", false);
        yield return new WaitForSeconds(4);



        MainMenuButton.SetActive(true);
    }

    public void FinishGame()
    {
        GamesManager.Instance.SwitchState<MainMenu>();
    }
}
