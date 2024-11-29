using Unity.VisualScripting;
using UnityEngine;
using static Unity.Cinemachine.CinemachineOrbitalTransposer;

public class Bug : MonoBehaviour
{
    public Transform leftRayOrigin;
    public Transform rightRayOrigin;
    public LayerMask groundLayer;
    public float rayLength = 0.05f;
    public float speed = 50f;
    public bool isFacingLeft = true;
    private bool bugMove = false;
    private Rigidbody2D rb;
    public bool IsMovingDefault = true;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool triggered = false;
    public bool isFriendly = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalkiing", IsMovingDefault);        
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            return;
        }

        bugMove = checkGround();
        if (bugMove)
        {
            moveBug();
        }        
    }

    private void moveBug ()
    {        
        if (isFacingLeft)
        {
            rb.linearVelocity = Vector2.left * Time.deltaTime * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.right * Time.deltaTime * speed;
        }
    }

    /*
     * This function checks if the bug has ground to move too. If the bug does not have room to move, it will flip his orientation
     * 
     * Returns: bool
     */
    private bool checkGround ()
    {
        RaycastHit2D leftHit;
        RaycastHit2D rightHit;
        bool result = false;

        if (isFacingLeft)
        { 
            leftHit = Physics2D.Raycast(leftRayOrigin.position, Vector2.down, rayLength, groundLayer);            
            if (leftHit.collider == null)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("IsWalkiing", true);
                //turn right
                isFacingLeft = false;
            }
            else
            {
                result = true;
            }
        }
        else
        {
            rightHit = Physics2D.Raycast(rightRayOrigin.position, Vector2.down, rayLength, groundLayer);
            if (rightHit.collider == null)
            {
                //turn left
                spriteRenderer.flipX = true;
                isFacingLeft = true;
                animator.SetBool("IsWalkiing", true);
            }
            else
            {
                result = true;
            }
        }        

        return result;

    }

    //when this is called the bug is being talked to, so before we return it's value we set it to triggered
    //and set it's velocity to zero
    public bool IsBugFriendly ()
    {
        triggered = true;
        rb.linearVelocity = Vector2.zero;

        return isFriendly;
    }

}

