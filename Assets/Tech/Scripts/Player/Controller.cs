using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    [SerializeField] float WalkThreshold;
    Rigidbody controller;

    [SerializeField] Animator animator;

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
    }
    public void UpdatePlayer()
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
}
