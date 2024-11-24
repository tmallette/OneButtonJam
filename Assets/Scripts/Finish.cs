using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Player")){
            Debug.Log("You've Finished the level :)");
        }
    }
    private void Start() { }

    private void Update() {


    }
}
