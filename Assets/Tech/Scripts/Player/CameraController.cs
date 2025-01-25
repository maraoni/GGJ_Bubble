using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    public static CameraController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public enum CameraState
    {
        FollowPlayer,
        FocusPoint
    }
    [SerializeField] CameraState state;
    [SerializeField] Transform Player;
    [SerializeField] Transform FocusPoint;

    [SerializeField] Vector3 PlayerOffset;

    private void Start()
    {
        state = CameraState.FollowPlayer;
    }
    public void UpdateCamera()
    {
        switch (state)
        {
            case CameraState.FollowPlayer:

                transform.position = Vector3.Lerp(transform.position, Player.position + PlayerOffset, 5 * Time.deltaTime);

                transform.LookAt(Player.position);

                break;
            case CameraState.FocusPoint:
                if(FocusPoint)
                {
                    transform.position = Vector3.Lerp(transform.position, FocusPoint.position, 15 * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, FocusPoint.rotation, 35 * Time.deltaTime);
                }
                else
                {
                    state = CameraState.FollowPlayer;
                }
                break;
        }
    }

    public void SetFocusPoint(Transform aFocusPoint)
    {
        FocusPoint = aFocusPoint;
        state = CameraState.FocusPoint;
    }
    public void ClearIfSame(Transform aFocusPoint)
    {
        if(FocusPoint == aFocusPoint)
        {
            FocusPoint = null;
            state = CameraState.FocusPoint;
        }
    }
    public void ClearFocusPoint()
    {
        FocusPoint = null;
        state = CameraState.FocusPoint;
    }
}
