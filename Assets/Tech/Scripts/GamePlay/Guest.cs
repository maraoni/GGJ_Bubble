using System.Collections;
using UnityEngine;

public class Guest : MonoBehaviour
{

    const float BoredDistance = 15.0f;
    public enum GuestState
    {
        WaitingForDrink,
        WaitingForFill,
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

    private void Start()
    {
        state = GuestState.WaitingForDrink;

        myGlass.gameObject.SetActive(true);
        myMoney.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (PlayerTransform)
        {
            if (Vector3.Distance(transform.position, PlayerTransform.position) > BoredDistance)
            {
                state = GuestState.WaitingForDrink;
            }
        }

        switch (state)
        {
            case GuestState.WaitingForDrink:

                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                break;
            case GuestState.WaitingForFill:

                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandFillPoint.transform.position, 10 * Time.deltaTime);

                if (myGlass.IsSatisfied)
                {
                    StartCoroutine(GiveMoney());
                }

                break;
            case GuestState.GivingMoney:
                Hand.transform.position = Vector3.Lerp(Hand.transform.position, HandDefault.transform.position, 10 * Time.deltaTime);

                if(myMoney.gameObject.activeSelf)
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
        myGlass.gameObject.SetActive(false);
        myMoney.gameObject.SetActive(true);
        state = GuestState.GivingMoney;

        for (float i = 0; i < 1.5f; i += Time.deltaTime) 
        {
            Hand.transform.position = Vector3.Lerp(HandFillPoint.transform.position, HandDefault.transform.position, i / 1.5f);
            yield return null;
        }

        for (float i = 0; i < 1.5f; i += Time.deltaTime)
        {
            Hand.transform.position = Vector3.Lerp(HandDefault.transform.position, HandFillPoint.transform.position, 10 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTransform = other.transform;
            if (state == GuestState.WaitingForDrink)
            {
                state = GuestState.WaitingForFill;
            }
        }
    }
}
