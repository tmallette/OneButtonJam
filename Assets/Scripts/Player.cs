using UnityEngine;
using UnityEngine.UI;
using static FlipBlock;

public class Player : MonoBehaviour
{
    //referencet to player
    private Rigidbody2D rb;
    private Animator animator;
    //start position
    private Vector2 startPOS = new Vector2(-4, 0);
    // jump properties
    private float holding = 0f;
    private bool jumping = false;

    public float jumpLowEnd = 3f;
    public float jumpHighEnd = 24f;

    public Transform leftRayOrigin;
    public Transform rightRayOrigin;
    public float rayLength = 0.05f;
    public LayerMask groundLayer;
    private bool grounded = false;
    private Vector2 respawnPoint;

    [SerializeField] private Slider power;

    public Vector2 playerTrajectory = new Vector2(0.7f, 1f);
    public Vector2 trajectory;

    [SerializeField] private Sound jumpSound;
    [SerializeField] private Sound deadSound;
    [SerializeField] private Sound checkpointSound;

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

        trajectory = playerTrajectory;
    }

    private void Update()
    {
        GroundCheck();

        if (jumping)
        {
            holding += Time.deltaTime * 10f;

            power.SetValueWithoutNotify(holding/jumpHighEnd);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            JumpHoldBegin();
        }

        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            JumpHoldEnd();
        }

        if (rb.transform.position.y < -10f)
        {
            AudioManager.Instance.PlaySFXClip(deadSound);
            Respawn();
        }
    }

    private void JumpHoldBegin()
    {
        holding = 0;
        jumping = true;
        animator.SetBool("Crouching", true);
        power.gameObject.SetActive(true);
    }

    private void JumpHoldEnd()
    {
        animator.SetBool("Crouching", false);
        power.gameObject.SetActive(false);
        jumping = false;
        AudioManager.Instance.PlaySFXClip(jumpSound);
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

        rb.AddForce(trajectory * holding, ForceMode2D.Impulse);
    }

    public void Respawn()
    {
        //check for checkpoints
        if (respawnPoint != null)
        {
            transform.position = respawnPoint;
        }
        else
        {
            transform.position = startPOS;                        
        }
        rb.linearVelocity = Vector2.zero;
        FlipRight();
    }

    private void GroundCheck()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin.position, Vector2.down, rayLength, groundLayer);

        grounded = leftHit.collider != null || rightHit.collider != null;

        if(!grounded)
        {
            power.gameObject.SetActive(false);
        }
    }

    private void FlipRight()
    {
        trajectory = playerTrajectory;
        transform.localScale = new Vector2(1,1);
    }

    private void FlipLeft()
    {
        trajectory = new Vector2(playerTrajectory.x * -1, playerTrajectory.y);
        transform.localScale = new Vector2(-1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlipBlock fb = collision.GetComponent<FlipBlock>();

        if(fb != null)
        {
            FlipDirections direction = fb.GetDirection();

            if(FlipDirections.Right == direction)
            {
                FlipRight();
            } else
            {
                FlipLeft();
            }
        }
    }  

    public void SetRespawnPoint (Vector2 position, bool facingRight)
    {
        //facingRight can be used later to make them face the correct direction
        Player.Instance.respawnPoint = position;
        AudioManager.Instance.PlaySFXClip(checkpointSound);
    }

}