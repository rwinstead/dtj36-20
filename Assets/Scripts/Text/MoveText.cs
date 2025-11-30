using UnityEngine;

public class MoveText : MonoBehaviour
{
    public enum MoveDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    [Header("Movement Settings")]
    public MoveDirection direction = MoveDirection.Left;
    public float speed = 300f;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 dir = GetDirectionVector();
        rect.anchoredPosition += dir * speed * Time.deltaTime;
    }

    private Vector2 GetDirectionVector()
    {
        switch (direction)
        {
            case MoveDirection.Left:
                return Vector2.left;
            case MoveDirection.Right:
                return Vector2.right;
            case MoveDirection.Up:
                return Vector2.up;
            case MoveDirection.Down:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }

}
