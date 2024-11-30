using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int NextLevel = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(NextLevel);
            Debug.Log("You've Finished the level :)");
        }
    }
}