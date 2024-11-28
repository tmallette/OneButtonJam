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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
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
                isFacingLeft = true;
            }
            else
            {
                result = true;
            }
        }        

        return result;

    }
}
