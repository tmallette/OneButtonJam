using System.Security.Cryptography;
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
        if (toggle)
        {
            bug = GetComponentInParent<Bug>();
            bool isProProletariat = bug.IsBugFriendly();
            if (isProProletariat)
            {
                int index = Random.Range(0,proProletariatArray.Length);
                tmp.SetText(proProletariatArray[index]);
            }
            else
            {
                int index = Random.Range(0, antiProletariatArray.Length);
                tmp.SetText(antiProletariatArray[index]);
            }
        }
        gameObject.SetActive(toggle);
    }


}
