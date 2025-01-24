using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    Rigidbody controller;
    private void Start()
    {
        controller = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float x, y;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 finalVelocity = new();
        Vector3 rightDir = Camera.main.transform.right;
        Vector3 forwardDir = Camera.main.transform.forward;
        forwardDir.y = 0;
        forwardDir.Normalize();

        finalVelocity += rightDir * x;
        finalVelocity += forwardDir * y;

        finalVelocity = Vector3.ClampMagnitude(finalVelocity, 1);

        controller.linearVelocity = finalVelocity * MovementSpeed;
        //controller.(finalVelocity * MovementSpeed * Time.deltaTime);
    }
}
