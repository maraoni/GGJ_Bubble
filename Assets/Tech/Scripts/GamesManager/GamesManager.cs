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
    public void DisplayText(string someText)
    {
        DialogueMachineText.DisplayText(someText);
    }
}
