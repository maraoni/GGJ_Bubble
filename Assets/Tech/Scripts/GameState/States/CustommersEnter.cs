using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustommersEnter : State
{
    [SerializeField] List<Transform> GuestPoints;
    [SerializeField] GameObject mainCamera;

    [SerializeField] GameObject Guest;
    [SerializeField] Transform GuestSpawn;
    public override void OnEnter()
    {
        base.OnEnter();
        mainCamera.SetActive(true);
        StartCoroutine(SpawnGuests());
    }

    public IEnumerator SpawnGuests()
    {
        yield return new WaitForSeconds(2);

        const float GuestTime = 0.5f;


        for (int i = 0; i < 3; i++)
        {
            if (GuestPoints.Count == 0)
            {
                break;
            }
            yield return new WaitForSeconds(GuestTime);

            int rndGuestPoint = Random.Range(0, GuestPoints.Count);
            GameObject guest = Instantiate(Guest, GuestSpawn.position, GuestPoints[rndGuestPoint].transform.rotation);

            Vector3 Point = GuestPoints[rndGuestPoint].transform.position;
            StartCoroutine(WalkGuestToPoint(guest.transform, Point));

            Destroy(GuestPoints[rndGuestPoint].gameObject);
            GuestPoints.RemoveAt(rndGuestPoint);

            yield return null;
        }

        yield return new WaitForSeconds(2);
        GamesManager.Instance.SwitchState<Playing>();
    }

    public IEnumerator WalkGuestToPoint(Transform aGuest, Vector3 aPoint)
    {
        const float WalkTime = 0.5f;
        Vector3 startPos = aGuest.transform.position;
        for (float t = 0; t < WalkTime; t += Time.deltaTime)
        {
            aGuest.transform.position = Vector3.Lerp(startPos, aPoint, t / WalkTime);

            yield return null;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        mainCamera.SetActive(false);
    }
}
