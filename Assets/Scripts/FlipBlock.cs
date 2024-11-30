using UnityEngine;

public class FlipBlock : MonoBehaviour
{
    public FlipDirections direction = FlipDirections.Left;

    public enum FlipDirections
    {
        Left,
        Right
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();

        if (p != null)
        {
            if (direction == FlipDirections.Left)
            {
                p.FlipLeft();
            }
            else
            {
                p.FlipRight();
            }
        }
    }
}
