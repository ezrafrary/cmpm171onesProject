using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;
    [Space]
    public float jumpHeight = 5f;


    private Vector2 input;
    private Rigidbody rb;

    private bool sprinting;
    private bool jumping;

    private bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void Update()
    {
        /*
        getting mouseinputs and movement button inputs in update rather than fixed update in order to make 
        movement feel smoother. Technically this shuould be done in fixed update, but this is a movement 
        based game and I would rather the game feel better to run around in than have the shooting be 100%
        accurate to what you see on your end.
        */

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("Sprint");
        jumping = Input.GetButton("Jump");
    }


    void FixedUpdate(){
        if (grounded){
            if (jumping){
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
        }
        rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
        grounded = false; 
    }

    private void OnTriggerStay(Collider other){
        grounded = true;
    }



    //basic movment script, we can change anything in here whenever we want. this is not set in stone
    Vector3 CalculateMovement(float speed){
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y); //input is a vector2, so we are converting it to vector3
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= speed;

        Vector3 velocity = rb.velocity;

        if(input.magnitude > 0.5f){
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        } else {
            if(grounded){ 
                rb.velocity = new Vector3(0,rb.velocity.y,0);
            }
            return new Vector3(0,0,0);
        }
    }
}
