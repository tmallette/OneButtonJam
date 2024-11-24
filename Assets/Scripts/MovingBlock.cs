using UnityEngine;

public class MovingBlock : MonoBehaviour {

    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;
    private  void Start()
    {
        Debug.Log("Let the games begin");
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if( i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
