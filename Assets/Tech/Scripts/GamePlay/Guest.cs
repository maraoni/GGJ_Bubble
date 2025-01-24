using System.Collections;
using UnityEngine;

public class Guest : MonoBehaviour
{

    const float BoredDistance = 15.0f;
    public enum GuestState
    {
        WaitingForDrink,
        WaitingForFill,
        PassingMoney,
        GivingMoney,
        Satisfied
    }

    public GuestState state;


    public Transform Hand;

    public Transform HandDefault;
    public Transform HandFillPoint;

    Transform PlayerTransform;

    [SerializeField] Glass myGlass;
    [SerializeField] Money myMoney;

    [SerializeField] string WantText;
    [SerializeField] string ThanksText;

    private void Start()
    {
        state = GuestState.WaitingForDrink;

        myGlass.gameObject.SetActive(true);
        myMoney.gameObject.SetActive(false);
    }


    void CheckOutside()
    {
        if (PlayerTransform)
        {
            if (Vector3.Distance(transform.position, PlayerTransform.position) > BoredDistance)
            {
                myGlass.gameObject.SetActive(true);
                myMoney.gameObject.SetActive(false);
                state = GuestState.WaitingForDrink;
            }
        }
    }
    private void Update()
    {

        

        switch (state)
        {
            case GuestState.WaitingForDrink:
                CheckOutside();
                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                break;
            case GuestState.WaitingForFill:
                CheckOutside();
                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandFillPoint.transform.position, 10 * Time.deltaTime);

                if (myGlass.IsSatisfied)
                {
                    StartCoroutine(GiveMoney());
                }

                break;
            case GuestState.PassingMoney:
                
                break;
            case GuestState.GivingMoney:
                //Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                if(!myMoney.gameObject.activeSelf)
                {
                    state = GuestState.Satisfied;

                }

                break;
            case GuestState.Satisfied:

                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                break;
        }
    }

    IEnumerator GiveMoney()
    {
        state = GuestState.PassingMoney;
        GamesManager.Instance.DisplayText(ThanksText);

        const float MaxTime = 1.5f;

        for (float i = 0; i < MaxTime; i += Time.deltaTime) 
        {
            Hand.transform.position = Vector3.Lerp(HandFillPoint.transform.position, HandDefault.transform.position, i / MaxTime);
            yield return null;
        }

        myGlass.gameObject.SetActive(false);
        myMoney.gameObject.SetActive(true);

        for (float i = 0; i < MaxTime; i += Time.deltaTime)
        {
            Hand.transform.position = Vector3.Lerp(HandDefault.transform.position, HandFillPoint.transform.position, i / MaxTime);
            yield return null;
        }

        state = GuestState.GivingMoney;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTransform = other.transform;
            if (state == GuestState.WaitingForDrink)
            {
                GamesManager.Instance.DisplayText(WantText);
                state = GuestState.WaitingForFill;
            }
        }
    }
}
