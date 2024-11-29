using UnityEngine;

public class ActivateBug : MonoBehaviour
{    
    public ChatBubbles chat;
    private bool isTriggered = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {            
            chat.ToggleCanvas(true); 
            isTriggered = true;
        }
    }
}
