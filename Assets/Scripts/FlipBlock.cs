using UnityEngine;

public class FlipBlock : MonoBehaviour
{
    public FlipDirections direction = FlipDirections.Left;

    public enum FlipDirections
    {
        Left,
        Right
    }

    public FlipDirections GetDirection()
    {
        return direction;
    }
}
