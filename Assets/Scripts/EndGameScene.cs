using System;
using System.Collections;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class EndGameScene : MonoBehaviour
{

    public GameObject playerCanvas;
    public GameObject bossCanvas;
    public GameObject endBossCanvas;
    public TextMeshProUGUI playerTMP;
    public TextMeshProUGUI bossTMP;
    public TextMeshProUGUI endBossTMP;
    public GameObject neutralCanvas;
    public GameObject bossGameObject;
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
                    LittleBossTalks("Junkman has been watching cartoons and drinking chocolate milk instead of working");    
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
                    LittleBossStopTalks();
                    BigBossTalks("Wow Junkman really? Ever since you made the weather building block you've really dialed it in");
                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("What is it Junkman?");
                }
                break;
            case 3:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("Everyone has a down week. I'm starting to work on... *adhd moment*");
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
                    PlayerStopTalks();
                    BigBossTalks("You are starting to work on what?");
                }
                else
                {
                    LittleBossTalks("Junkman hasn't done any work all day!!! All he does is talk to people");
                }
                break;
            case 5:
                if (BossWin)
                {
                    BigBossStopTalks();
                    PlayerTalks("*continues to have intense adhd moment*");
                }
                else
                {
                    LittleBossStopTalks();
                    PlayerStopTalks();
                    BigBossTalks("ENOUGH TROY! WHAT DO YOU EVEN DO HERE... Sorry Junkman, please continue");
                }
                break;
            case 6:
                if (BossWin)
                {
                    PlayerTalks("*squirrel finally runs away* ...starting to work on deez"); 
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
                    PlayerStopTalks();
                    LittleBossTalks("(*tries to take credit*) Yea deez is the next big thing, it was my idea");
                }
                else
                {
                    PlayerTalks("Everyone has been stepping up, working overtime, and staying committed to delivering results");
                }
                break;
            case 8:
                if (BossWin)
                {
                    LittleBossStopTalks();
                    BigBossTalks("Oh wow! If Troy is excited about deez I know it will be great!");
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
                    PlayerTalks("*snickering quietly*");
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
                    PlayerStopTalks();
                    BigBossTalks("Junkman, what is deez?");
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
                    //if I could zoom in on his face until a mouse click I would
                    BigBossStopTalks();
                    LittleBossTalks("Yea, what is deez? Can you elaborate?");
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
                    LittleBossStopTalks();
                    //add a new canvas so I can have crickets sounds int he middle of the screen
                    neutralCanvas.SetActive(true);
                    neutralCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText("**crickets**");
                }
                else
                {                    
                    BigBossTalks("...");
                }
                break;
            case 13:
                if (BossWin)
                {
                    //more crickets
                    neutralCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText("**crickets continue**");
                }
                else
                {
                    PlayerStopTalks();
                    BigBossTalks("We need managers less like Troy and more like you Junkman");
                }
                break;
            case 14:
                if (BossWin)
                {
                    //hide the middle canvas
                    neutralCanvas.SetActive(false);
                    PlayerTalks("Ligma balls");
                }
                else
                {
                    BigBossTalks("In fact I think we should get rid of Troy. Junkman, should we get rid of him... permanently? Yes or No");
                }
                break;
            case 15:
                if (BossWin)
                {
                    PlayerStopTalks();
                    Player.Instance.GetAnimator().SetBool("IsDead", true);
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
                    BigBossTalks("Troy, what is deez?");
                }
                else
                {
                    PlayerStopTalks();
                    Boss.Instance.GetAnimator().SetBool("IsDead", true);                    
                }                
                break;
            case 17:
                if (BossWin)
                {
                    BigBossStopTalks();
                    LittleBossTalks("Let me google it... it stands for Deez nuts. Must be a food program or something");                    
                }
                else
                {
                    
                }
                break;
            case 18:
                if (BossWin)
                {
                    bossGameObject.GetComponent<SpriteRenderer>().flipX = false;
                    LittleBossTalks("For the record, what is our policy on having fourths at corporate lunches ?");                    
                }
                else
                {

                }
                break;
            case 19:
                if (BossWin)
                {
                    LittleBossStopTalks();
                    neutralCanvas.SetActive(true);
                    neutralCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText("**awkward silence**");
                }
                else
                {

                }
                break;
            case 20:
                if (BossWin)
                {
                    neutralCanvas.SetActive(false);
                    BigBossTalks("God you are the worst");
                }
                else
                {

                }
                break;
            case 21:
                if (BossWin)
                {
                    BigBossStopTalks();
                    Boss.Instance.GetAnimator().SetBool("IsDead", true);
                }
                else
                {

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
