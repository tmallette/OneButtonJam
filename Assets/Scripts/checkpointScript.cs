using UnityEngine;

public class checkpointScript : MonoBehaviour
{

    public bool facingRight = true;
    public GameObject respawnPoint;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {                
            Player.Instance.setRespawnPoint(respawnPoint.transform.position, facingRight);
        }
    }   

}
