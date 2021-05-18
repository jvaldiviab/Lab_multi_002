using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;

    float x;
    float z;
    Vector3 move;

    //gravedad
    Vector3 velocity;
    public float gravity = -15f;

    public Transform groundCheck;
    float radius = 0.4f;
    public LayerMask mask;
    bool isGrounded = false;

    //jump
    public float jumpForce = 1f;

    private float jumpValue;

    // Start is called before the first frame update
    void Start()
    {
        jumpValue = Mathf.Sqrt(jumpForce * -2 * gravity);
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, radius, mask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //salto
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // Formula realista para obtener una fuerza hacia arriba
            velocity.y = jumpValue;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
