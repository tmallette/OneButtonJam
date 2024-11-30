using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int NextLevel = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        Debug.Log(collision.GetContact(0).normal);
        Vector3 firstCollision = collision.GetContact(0).normal;
        if (collision.transform.CompareTag("Player") && firstCollision == Vector3.down)
        {
            SceneManager.LoadScene(NextLevel);
            Debug.Log("You've Finished the level :)");
        }
    }
}