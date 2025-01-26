using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    [SerializeField] float WalkThreshold;
    Rigidbody controller;

    [SerializeField] Animator animator;

    [SerializeField] GameObject Cork;
    [SerializeField] GameObject ChampagneEffect;

    public float MaxChampagneFill;
    [SerializeField] float CurrentChampagneFill;

    [SerializeField] LiquidUpdated myBottle;

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
    }
    public void UpdatePlayer()
    {
        float fill = Mathf.Lerp(1, 0, CurrentChampagneFill / MaxChampagneFill);
        myBottle.SetFill(fill);

        CurrentChampagneFill -= Time.deltaTime;

        if(CurrentChampagneFill <= 0)
        {
            GamesManager.Instance.SwitchState<LoseState>();
        }

        Playing s = GamesManager.Instance.GetState<Playing>() as Playing; 
        float x, y;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 finalVelocity = new();
        Vector3 rightDir = s.mainCamera.transform.right;
        Vector3 forwardDir = s.mainCamera.transform.forward;
        forwardDir.y = 0;
        forwardDir.Normalize();

        finalVelocity += rightDir * x;
        finalVelocity += forwardDir * y;

        finalVelocity = Vector3.ClampMagnitude(finalVelocity, 1);

        controller.linearVelocity = finalVelocity * MovementSpeed;


        float xSpeed = Vector3.Dot(finalVelocity, rightDir);
        float YSpeed = Vector3.Dot(finalVelocity, forwardDir);

        //Vector3 relativeVelocity = transform.InverseTransformPoint(finalVelocity);

        animator.SetFloat("X", xSpeed);
        animator.SetFloat("Y", YSpeed);


        if (finalVelocity.magnitude > WalkThreshold)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        //controller.(finalVelocity * MovementSpeed * Time.deltaTime);
    }

    public void PopCork()
    {
        StartCoroutine(PopTheCork());
    }

    public void ResetBottle()
    {
        myBottle.SetFill(0);
        CurrentChampagneFill = MaxChampagneFill;
        Cork.SetActive(true);
        ChampagneEffect.SetActive(false);
    }

    public IEnumerator PopTheCork()
    {
        Cork.SetActive(true);
        ChampagneEffect.SetActive(false);
        yield return new WaitForSeconds(1);

        Cork.SetActive(false);
        ChampagneEffect.SetActive(true);

    }
}
