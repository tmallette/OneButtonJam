using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private float holding = 0f;
    private bool jumping = false;


    public float jumpLowEnd = 3f;
    public float jumpHighEnd = 12f;


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
            JumpHoldBegin();
        }



        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpHoldEnd();
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
}