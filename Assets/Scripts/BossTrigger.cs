using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossChat bossChat;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss boss = GetComponentInParent<Boss>();
        if (!boss.triggered)
        {            
            bossChat.ToggleChat(true);
        }
        boss.triggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("leaving boss area, starting boss");
        Boss boss = GetComponentInParent<Boss>();
        boss.StartBoss();
    }
}
