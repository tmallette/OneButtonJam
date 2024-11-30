using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBubbles : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Bug bug;

    private string[] proProletariatArray = new string[] {"This job fucking sucks","I heard they make the drivers use piss bottles"};
    private string[] antiProletariatArray = new string[] { "I love the factory", "Long live Beff Jezos!" };

    public void ToggleCanvas (bool toggle)
    {
        bug = GetComponentInParent<Bug>();
        if (toggle)
        {            
            bool isProProletariat = bug.IsBugFriendly();
            if (isProProletariat)
            {
                int index = Random.Range(0,proProletariatArray.Length);
                tmp.SetText(proProletariatArray[index]);
                gameObject.SetActive(toggle);
                bug.GetAnimator().SetBool("IsWalking", false);
                StartCoroutine(CanvasTimer());
            }
            else
            {
                int index = Random.Range(0, antiProletariatArray.Length);
                tmp.SetText(antiProletariatArray[index]);
                gameObject.SetActive(toggle);
                bug.GetAnimator().SetBool("IsWalking", false);
            }
        }
        else
        {
            gameObject.SetActive(toggle);
            bug.GetAnimator().SetBool("IsWalking", true);
        }
    }

    IEnumerator CanvasTimer()
    {
        yield return new WaitForSeconds(3f); 
        gameObject.SetActive(false);
        bug.GetAnimator().SetBool("IsDead", true);
    }
}
