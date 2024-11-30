using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static Unity.Cinemachine.CinemachineOrbitalTransposer;

public class Boss : MonoBehaviour
{
    public Transform leftRayOrigin;
    public Transform rightRayOrigin;
    public LayerMask groundLayer;
    public float rayLength = 0.05f;
    public float speed = 50f;
    public bool triggered = false;
    public BossChat bossChat;

    //You can set the jump power values for the boss based on what level design we come up with
    private bool bossPaused = false;
    private float[] bossJumps = new float[] {11.3f,4.5f,4.5f,15f};
    private float[] bossPause = new float[] {7f,4f,4f,5f};
    private int jumpIndex = 0;
    private int pauseIndex = 0;    
    private bool isJumping = false;
    private Rigidbody2D rb;
    private Animator animator;    
    private bool startBoss = false;
    private SpriteRenderer spriteRenderer;

    public static Boss Instance { get; private set; }
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();        
    }

    private void FixedUpdate()
    {
        //check if the player is no longer colliding with bug
        if (!triggered)
        {
            return;
        }        

        //movement logic
        if (startBoss && !bossPaused)
        {
            if (isBossAtEdge())
            {
                if (!isJumping)
                {
                    isJumping = true;
                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsFlying", true);
                    Debug.Log("Bug just jumped! isJumping=" + isJumping);
                    Jump();
                }
            }
            else
            {
                animator.SetBool("IsMoving", true);
                rb.linearVelocity = speed * Vector2.right * Time.deltaTime;
            }
        }
    }

    public void StartBoss ()
    {
        bossChat.ToggleChat(false);
        startBoss = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
    }

    private void Jump ()
    {
        try
        {
            rb.linearVelocity = Vector2.zero;
            float jumpPower = bossJumps[jumpIndex];
            rb.AddForce(new Vector2(0.7f, 1f) * jumpPower, ForceMode2D.Impulse);
            jumpIndex += 1;            
        }
        catch 
        {
            jumpIndex = 0;
        }
    }
    private bool isBossAtEdge ()
    {        
        RaycastHit2D rightHit;
        bool result = false;

        rightHit = Physics2D.Raycast(rightRayOrigin.position, Vector2.down, rayLength, groundLayer);
        if (rightHit.collider == null)
        {
            result = true;
        }
        else
        {
            if (isJumping)
            {

                animator.SetBool("IsFlying", false);
                Debug.Log("Bug just landed!");
                //first time we detect ground after the boss lands, pause so he can huff and puff for X seconds                         
                BossPause(pauseIndex);
                pauseIndex += 1;
            }
            isJumping = false;
        }

        return result;

    }

    IEnumerator BossPauseTimer (float timeToPause)
    {

        bossPaused = true;
        animator.SetBool("IsBreathing", true);
        yield return new WaitForSeconds(timeToPause);
        //add animator for huff and puff animation
        animator.SetBool("IsBreathing", false);
        bossPaused = false;
    }

    private void BossPause (int pIndex)
    {
        float ttp = 0f;
        try
        {
            ttp = bossPause[pIndex];
            StartCoroutine(BossPauseTimer(ttp));
        }
        catch
        {
            pauseIndex = 0;            
        }        
    }
}
