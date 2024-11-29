using System.Collections;
using Unity.VisualScripting;
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
    private float[] bossJumps = new float[] {9f,10f,30f};
    private float[] bossPause = new float[] {5f,10f};
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
        if (startBoss)
        {
            if (isBossAtEdge())
            {
                if (!isJumping)
                {
                    isJumping = true;
                    Jump();
                }
            }
            else
            {
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
        yield return new WaitForSeconds(timeToPause);
        //add animator for huff and puff animation
    }

    private void BossPause (int pIndex)
    {
        try
        {
            float ttp = bossPause[pIndex];
            BossPauseTimer(ttp);
        }
        catch
        {
            pauseIndex = 0;
        }
    }
}
