using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class bflyPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float vertSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Scene scene;
    public string sceneName;

    public float rotationSpeed = 200f;
    //public float vertSpeed;


  //  public int pollenCount;
  //  public bool inFlower;

    public float gravity;

    public int TimeToFly;
    public int maxTimeToFly;

    public float autoForward;


    public float boostForce;
    public float boostDuration;

  //  public float glideSpeed;
   // public bool gliding;

  //  public float glideGravity;

    public float sideSpeed;
    public float vertSpeeds;

    public float autoForwardOrig;
    public float autoForwardLimit;

    public bflyCamBasic bfly;

    private int score;

    public float maxY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        sceneName = scene.name;

        TimeToFly = maxTimeToFly;

        //  glideSpeed = moveSpeed * 3;    // 1.5f;
        //need to rotate in direction of movement if in TS scene

        autoForwardOrig = autoForward;
    }

    //movement:

    //up and down

    //forward and backward

    //rotate


    //NEW

    //automatically moves forward
    //stop cam rotation
    //can move up, down, left, right 
    //other moves?

    private void Update()
    {

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        // if (grounded)
        {
            rb.drag = groundDrag;
        }
        // else
        {
            //     rb.drag = 0;
        }

        TimeToFly--;

        if (TimeToFly < 0)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //up
                //rb.AddForce(Vector3.up.normalized * vertSpeed * 10f, ForceMode.Force);
                // rb.AddForce(Vector3.up.normalized * vertSpeed * 10f, ForceMode.Impulse);

                TimeToFly = maxTimeToFly;
            }
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {
            //down
            //  rb.AddForce(-Vector3.up.normalized * vertSpeed * 10f, ForceMode.Force);
        }

        if (Time.timeScale == 1)
        {
            DownDrift();

            Boosts();

            // bfly.gliding = gliding;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void DownDrift()
    {
       // if (!gliding)
        {
            rb.AddForce(-Vector3.up.normalized * gravity * 10f, ForceMode.Force);
        }

      //  else
        {
       //     rb.AddForce(-Vector3.up.normalized * glideGravity * 10f, ForceMode.Force);
        }
    }

    public void Boosts()
    {
      //  if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {

                transform.position = transform.position + new Vector3(-sideSpeed, 0, 0);

            }


            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
               
                transform.position = transform.position + new Vector3(sideSpeed, 0, 0);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {

                transform.position = transform.position + new Vector3(0, vertSpeeds, 0);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                
                transform.position = transform.position + new Vector3(0, -vertSpeeds, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //forward boost

            // rb.AddForce(orientation.forward.normalized * boostForce * 10f, ForceMode.Force);


            //glide - speed increase, more drop, can't turn?

          //  gliding = true;

            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
          //  gliding = false;
        }
    }
  

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

      

    }

    private void MovePlayer()
    {
        // calculate movement direction
      //  moveDirection = orientation.forward * verticalInput;    // + orientation.right * horizontalInput;


        moveDirection = orientation.forward * verticalInput;  

        //rotation sideways



        // on ground
        //  if (grounded)

     //   if (!gliding)
        {
        //    rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

    //    else
        {
          //  rb.AddForce(moveDirection.normalized * glideSpeed * 10f, ForceMode.Force);


          //  DownDrift();
        }

        //change from add force to transform position?


        score = bfly.score;

        //speed = original speed + 1% of score

        autoForward = autoForwardOrig + ((score * 0.01f)/2);

        //0.05 + 1% of score - if score is 100

        //max speed

        if (autoForward > autoForwardLimit)
        {
            autoForward = autoForwardLimit;
        }

        transform.position = transform.position + new Vector3(0, 0, autoForward);



        //rb.AddForce(transform.forward * autoForward * 10f, ForceMode.Force);


        // in air
        //  else if (!grounded)
        //    rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        if (transform.position.x < 12)
        {
            transform.position = new Vector3(12, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 30)
        {
            transform.position = new Vector3(30, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }



    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Die()
    {
        //display score + stop scene movement / activity

        //reset

        Time.timeScale = 0;
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "plant")
        {
           // inFlower = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "plant")
        {
         //   inFlower = false;
        }
    }

}