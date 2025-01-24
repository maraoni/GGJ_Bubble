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
    private void Start()
    {
        
    }
}
