using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    [SerializeField] GameObject mat1;
    [SerializeField] GameObject mat2;

    [SerializeField] float InBetweener;

    bool stage = false;

    float timer = 0;

    [SerializeField] bool ShouldRotate = false;

    void Update()
    {

        if (ShouldRotate)
        {
            transform.Rotate(Vector3.up * 35 * Time.deltaTime);
        }

        if (timer > InBetweener)
        {
            if(stage)
            {
                stage = !stage;
                mat1.SetActive(false);
                mat2.SetActive(true);

            }
            else
            {
                stage = !stage;
                mat2.SetActive(false);
                mat1.SetActive(true);
            }


            timer = 0;
            Debug.Log("Changed material");
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
