using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //referencet to player
    private Rigidbody2D rb;
    private Animator animator;
    //movement Vector
    private Vector2 movement;
    //start position
    private Vector2 startPOS = new Vector2(-4, 0);
    // jump properties
    private float holding = 0f;
    private bool jumping = false;

    public float jumpLowEnd = 3f;
    public float jumpHighEnd = 24f;

    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(jumping)
        {
            holding += Time.deltaTime * 10f;
        }

        //Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Crouching", true);
            JumpHoldBegin();
        }



        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Crouching", false);
            JumpHoldEnd();
        }


        if (rb.transform.position.y < -50)
        {
            Respawn();
        }
    }


    private void JumpHoldBegin()
    {
        holding = 0;
        jumping = true;
    }

    private void JumpHoldEnd()
    {
        jumping = false;
        Jump();
    }

    private void Jump()
    {
        if(holding > jumpHighEnd)
        {
            holding = jumpHighEnd;
        } 
        else if(holding < jumpLowEnd)
        {
            holding = jumpLowEnd;
        }

        rb.AddForce(new Vector2(0.7f, 0.7f) * holding, ForceMode2D.Impulse);
    }


    private void FixedUpdate()
    {
        FixedMovement();
    }

    private void Movement()
    {
        movement = new Vector2 (Input.GetAxisRaw("Horizontal"), 0).normalized;
    }

    private void FixedMovement()
    {
        Vector2 moveForce = movement * 7f; 

        if(moveForce != Vector2.zero)
        {
            rb.linearVelocity = moveForce;
        } 
        else
        {
            //rb.linearVelocity = Vector2.zero;
        }
    }

    private void Respawn()
    {
        transform.position = startPOS;
        rb.linearVelocity = new Vector2(0,0);
    }
}