using UnityEngine;

public class GamesManager : StateMachine
{
    #region Singleton
    public static GamesManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] MachineText DialogueMachineText;

    private void Start()
    {
        InitializeStateMachine();

        SwitchState<MainMenu>();
    }
    public void DisplayText(string someText, bool ShouldDissapear)
    {
        DialogueMachineText.DisplayText(someText, ShouldDissapear);
    }
}
