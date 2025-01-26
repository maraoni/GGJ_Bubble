using System.Collections;
using UnityEngine;

public class Guest : MonoBehaviour
{

    const float BoredDistance = 22.0f;
    public enum GuestState
    {
        Idle,
        WaitingForDrink,
        WaitingForFill,
        PassingMoney,
        GivingMoney,
        Satisfied
    }

    public GuestState state;

    Transform PlayerTransform;

    [SerializeField] Glass myGlass;
    [SerializeField] Money myMoney;

    [SerializeField] Transform myFocusPoint;

    [SerializeField] string WantText;
    [SerializeField] string ThanksText;

    [SerializeField] Animator myAnim;

    public bool IsSatisfied => state == GuestState.Satisfied;

    float WantDrinkTime;

    private void Start()
    {
        InitializeGuest();
    }

    public void InitializeGuest()
    {
        WantDrinkTime = Random.Range(3.0f, 6.0f);

        state = GuestState.Idle;

        myGlass.Initialize();

        myGlass.gameObject.SetActive(false);
        myMoney.gameObject.SetActive(false);
    }

    void CheckOutside()
    {
        if (PlayerTransform)
        {
            if (Vector3.Distance(transform.position, PlayerTransform.position) > BoredDistance)
            {
                myGlass.gameObject.SetActive(false);
                myMoney.gameObject.SetActive(false);
                state = GuestState.WaitingForDrink;
                CameraController.Instance.ClearIfSame(myFocusPoint);
                if (state != GuestState.GivingMoney && state != GuestState.Satisfied)
                {
                    myAnim.SetTrigger("WantDrink");
                }
            }
        }
    }
    public void UpdateGuest()
    {


        switch (state)
        {
            case GuestState.Idle:
                if (WantDrinkTime <= 0)
                {
                    state = GuestState.WaitingForDrink;
                    myAnim.SetTrigger("WantDrink");
                }
                else
                {
                    WantDrinkTime -= Time.deltaTime;
                }
                break;
            case GuestState.WaitingForDrink:
                CheckOutside();
                //Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                break;
            case GuestState.WaitingForFill:
                CheckOutside();
                //Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandFillPoint.transform.position, 10 * Time.deltaTime);

                if (myGlass.IsSatisfied)
                {
                    StartCoroutine(GiveMoney());
                }

                break;
            case GuestState.PassingMoney:

                break;
            case GuestState.GivingMoney:
                //Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                if (!myMoney.gameObject.activeSelf)
                {
                    state = GuestState.Satisfied;
                    myMoney.gameObject.SetActive(false);
                    myAnim.SetTrigger("Satisfied");

                    CameraController.Instance.ClearIfSame(myFocusPoint);
                }

                break;
            case GuestState.Satisfied:

                //Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);
                break;
        }
    }

    IEnumerator GiveMoney()
    {
        state = GuestState.PassingMoney;
        myGlass.gameObject.SetActive(false);
        myMoney.gameObject.SetActive(true);
        myAnim.SetTrigger("PresentMoney");
        GamesManager.Instance.DisplayText(ThanksText, true);
        yield return new WaitForSeconds(2);
        myAnim.SetTrigger("WavingMoney");
        state = GuestState.GivingMoney;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (WantDrinkTime <= 0)
            {

                PlayerTransform = other.transform;
                if (state == GuestState.WaitingForDrink)
                {
                    CameraController.Instance.SetFocusPoint(myFocusPoint);
                    GamesManager.Instance.DisplayText(WantText, false);
                    state = GuestState.WaitingForFill;
                    myGlass.gameObject.SetActive(true);
                    myAnim.SetTrigger("PresentGlass");
                }
            }
        }
    }
}
