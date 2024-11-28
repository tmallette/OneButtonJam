using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool facingRight = true;
    public GameObject respawnPoint;
    public int savePoint = 0;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (GameDataManager.Instance != null && savePoint != GameDataManager.Instance.respawnPoint){
                 GameDataManager.Instance.SaveGame(savePoint);
            }
            Player.Instance.SetRespawnPoint(respawnPoint.transform.position, facingRight);
        }
    }   

    public bool GetFacing()
    {
        return facingRight;
    }

    public Vector2 GetPosition()
    {
        return respawnPoint.transform.position;
    }
}
