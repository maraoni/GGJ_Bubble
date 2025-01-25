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
    }
    public void DisplayText(string someText)
    {
        DialogueMachineText.DisplayText(someText);
    }
}
