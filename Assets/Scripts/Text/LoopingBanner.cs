using UnityEngine;

public class LoopingSingleBanner : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    [Header("Movement")]
    public Direction moveDirection = Direction.Left;
    public float speed = 200f;

    [Header("Loop Boundaries")]
    public float startX = 800f;    // Used when moving left/right
    public float endX = -800f;
    public float startY = 500f;    // Used when moving up/down
    public float endY = -500f;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        ResetToStart();
    }

    void Update()
    {
        Move();
        CheckLoop();
    }

    private void Move()
    {
        Vector2 dir = Vector2.zero;

        switch (moveDirection)
        {
            case Direction.Left:  dir = Vector2.left;  break;
            case Direction.Right: dir = Vector2.right; break;
            case Direction.Up:    dir = Vector2.up;    break;
            case Direction.Down:  dir = Vector2.down;  break;
        }

        rect.anchoredPosition += dir * speed * Time.deltaTime;
    }

    private void CheckLoop()
    {
        Vector2 pos = rect.anchoredPosition;

        switch (moveDirection)
        {
            case Direction.Left:
                if (pos.x <= endX)
                    rect.anchoredPosition = new Vector2(startX, pos.y);
                break;

            case Direction.Right:
                if (pos.x >= endX)
                    rect.anchoredPosition = new Vector2(startX, pos.y);
                break;

            case Direction.Up:
                if (pos.y >= endY)
                    rect.anchoredPosition = new Vector2(pos.x, startY);
                break;

            case Direction.Down:
                if (pos.y <= endY)
                    rect.anchoredPosition = new Vector2(pos.x, startY);
                break;
        }
    }

    private void ResetToStart()
    {
        Vector2 pos = rect.anchoredPosition;

        switch (moveDirection)
        {
            case Direction.Left:
            case Direction.Right:
                pos.x = startX;
                break;

            case Direction.Up:
            case Direction.Down:
                pos.y = startY;
                break;
        }

        rect.anchoredPosition = pos;
    }
}
