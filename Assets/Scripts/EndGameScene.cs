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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered boss dialogue");
        StartEndGameDialogue();
    }
    
    public void StartEndGameDialogue()
    {
        //pause for 2 seconds

        BigBossTalks("What on earth are you doing here? Why aren't you at your machine?");
    }

    private void BigBossTalks (string message)
    {
        endBossTMP.SetText(message);
        endBossCanvas.gameObject.SetActive(true);
        //wait for player click, then close the chat
        StartCoroutine(WaitForAnswer());
        endBossCanvas.gameObject.SetActive(false);
    }

    private void LittleBossTalks ()
    {

    }

    private void PlayerTalks ()
    {

    }

    IEnumerator WaitForAnswer()
    {
        for (; ; )
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;    
            }
            yield return null;
        }
        yield return null;
    }

}
