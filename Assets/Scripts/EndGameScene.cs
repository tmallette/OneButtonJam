using System;
using System.Collections;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
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
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");
                    
                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("Hey, I wanted to take a moment to discuss something that I think is really important for the team");
                }                
                break;
            case 2:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("What is it Chad?");
                }
                break;
            case 3:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that...");
                }
                break;
            case 4:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    LittleBossTalks("Chad hasn't done any work all day!!! All he does is talk to people");
                }
                break;
            case 5:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    LittleBossStopTalks();
                    PlayerStopTalks();
                    BigBossTalks("ENOUGH TROY! WHAT DO YOU EVEN DO HERE... Sorry Chad, please continue");
                }
                break;
            case 6:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("I have been talking to people because I want to make things better. As you know, we’ve all been putting in a lot of hard work lately");
                }
                break;                
            case 7:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    PlayerTalks("Everyone has been stepping up, working overtime, and staying committed to delivering results");
                }
                break;
            case 8:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("...listening intently...");
                }
                break;
            case 9:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("It seems like the morale across the team is starting to dip, and I believe a raise for everyone could really help boost the team’s motivation");
                    
                }
                break;                
            case 10:
                if (BossWin)
                {

                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("...");
                }
                break;
            case 11:
                if (BossWin)
                {                    

                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("When employees feel valued and see that their efforts are being recognized financially, they’re more likely to continue putting in their best work");
                }
                break;
            case 12:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {                    
                    BigBossTalks("...");
                }
                break;
            case 13:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("We need managers less like Troy and more like you Chad");
                }
                break;
            case 14:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    BigBossTalks("In fact I think we should get rid of Troy. Chad, should we get rid of him... permanently? Yes or No");
                }
                break;
            case 15:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("I came to tell you that");

                }
                else
                {
                    BigBossStopTalks();
                    PlayerTalks("> Fire Troy! <                        Spare Troy?");
                }
                break;
            case 16:
                BigBossStopTalks();
                //kills someone
                if (BossWin)
                {
                    Player.Instance.GetAnimator().SetBool("IsDead", true);
                    //kill User
                }
                else
                {
                    PlayerStopTalks();
                    Boss.Instance.GetAnimator().SetBool("IsDead", true);                    
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
            BigBossTalks("I had better hear a good reason for why you aren't working right now...");
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
