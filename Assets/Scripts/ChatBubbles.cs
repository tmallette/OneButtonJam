using System.Collections;
using System.Security.Cryptography;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.LightTransport;

public class ChatBubbles : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Bug bug;


    private string[] proProletariatArray = new string[] {"Shhhh... I play hide and seek for $800 a week","I heard they make the drivers use piss bottles","I won't do nothing new after 2:00","Boss gets a 20, I get a buck. That's why I poop on the company clock", "What is our company's policy on mental health days?","So do you want it done fast or done right? You can only have one","Why did my benefits go up? We had a record year!", "We can have democracy in this country, or we can have great wealth concentrated in the hands of a few, but we can't have both", "They call it the American Dream, because you've got to be asleep to believe it.", "Growth for the sake of growth is the ideology of the cancer cell", "Workers of the factory, unite! You have nothing to lose but your chains!", "From Each According to His Abilities, to Each According to His Needs", "Religion Is the Opium of the People","Landlords, like all other men, love to reap where they never sowed","Abolish all private property!","Revolutions are the locomotives of history","Just work your wage"};
    private string[] antiProletariatArray = new string[] { "If you have time to lean you have time to clean", "I prefer to work 60 hour weeks, I'm glad they allow it", "Ask not what your company can do for you, but what you can do for your company!", "My only regret is I have but one life to lose for my company","The tree of overtime must be refreshed from time to time with the fruits from the salaried laborer","Protect individual worker's rights, vote no to a labor union","I realy hope the vote goes well. $20 a month in dues?? Fascists"};

    public void ToggleCanvas (bool toggle)
    {
        bug = GetComponentInParent<Bug>();
        if (toggle)
        {            
            bool isProProletariat = bug.IsBugFriendly();
            if (isProProletariat)
            {
                int index = Random.Range(0, proProletariatArray.Length);                
                gameObject.SetActive(toggle);
                bug.GetAnimator().SetBool("IsWalking", false);
                StartCoroutine(CanvasTimer());
                
                if (bug.isFinalBug)
                {
                    tmp.SetText("Jump as far as you can!!!!");
                }
                else
                {
                    tmp.SetText(proProletariatArray[index]);
                }
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
