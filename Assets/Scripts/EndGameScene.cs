using System.Collections;
using TMPro;
using UnityEngine;

public class EndGameScene : MonoBehaviour
{

    public GameObject playerCanvas;
    public GameObject bossCanvas;
    public GameObject endBossCanvas;
    public TextMeshProUGUI playerTMP;
    public TextMeshProUGUI bossTMP;
    public TextMeshProUGUI endBossTMP;
    private bool endSceneStarted = false;
    private int endSceneStep = 0;
    private bool BossWin = false;
    private bool PlayerWin = false;
    private bool bossOnPlatform = false;
    private bool playerOnPlatform = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && endSceneStarted){
            endSceneStep += 1;
        }


        if (bossOnPlatform && playerOnPlatform)
        {
            StartEndGameDialogue();
        }

        switch (endSceneStep)
        {
            case 1:
                BigBossStopTalks();
                PlayerTalks("Trying to get laid.");
                break;
            case 2:
                PlayerStopTalks();
                LittleBossTalks("See he should be Fired");
                break;
            case 3:
                LittleBossStopTalks();
                BigBossTalks("I dont give a fuck. You're his manager. Your done Kiddo!");
                break;
            case 4:
                BigBossStopTalks();
                //kills someone
                if (BossWin)
                {
                    Player.Instance.GetAnimator().SetBool("IsDead", true);
                    //kill User
                }
                else
                {
                    Boss.Instance.GetAnimator().SetBool("IsDead", true);
                    //kil Boss
                }
                
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for winner
        checkWinnner(collision);

        //start dialog for user
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            bossOnPlatform = true;

        }
    }

    public void checkWinnner(Collider2D collision)

    {
        if(collision.gameObject.CompareTag("Player") && BossWin == false)
        {
            PlayerWin = true;
        }
        else if(collision.gameObject.CompareTag("Enemy") && PlayerWin == false){
            BossWin = true;
        }
    }
    
    public void StartEndGameDialogue()
    {
        if (endSceneStarted == false)
        {
            endSceneStarted = true;
            BigBossTalks("What on earth are you doing here? Why aren't you at your machine?");
        }
    }

    private void BigBossTalks (string message)
    {
        endBossTMP.SetText(message);
        endBossCanvas.gameObject.SetActive(true);
        //wait for player click, then close the chat
    }

    private void BigBossStopTalks()
    {
        endBossTMP.SetText("");
        endBossCanvas.gameObject.SetActive(false);
    }

    private void LittleBossTalks (string message)
    {
        bossTMP.SetText(message);
        bossCanvas.gameObject.SetActive(true);
    }

    private void LittleBossStopTalks()
    {
        bossTMP.SetText("");
        bossCanvas.gameObject.SetActive(false);
    }

    private void PlayerTalks(string message)
    {
        playerTMP.SetText(message);
        playerCanvas.gameObject.SetActive(true);
    }

    private void PlayerStopTalks()
    {
        playerTMP.SetText("");
        playerCanvas.gameObject.SetActive(false);
    }
}
