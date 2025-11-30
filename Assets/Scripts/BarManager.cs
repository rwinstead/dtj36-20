using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BarManager : MonoBehaviour
{
    [Header("Bar Sprites")]
    public Sprite[] possibleSprites; // assign 3 sprites in inspector

    [Header("Width Settings")]
    public float minWidth = 1f;       // starting width in world units
    public float maxWidth = 5f;       // target width in world units

    [Header("Phase Settings")]
    public float slowSpeed = 0.5f;    // units per second for slow phase
    public float fastSpeed = 3f;      // units per second for fast phase

    [Header("Phase Timing")]
    public float slowPhaseDuration = 2f; // optional, limits slow phase

    [Header("Optional Second Bar")]
    public SpriteRenderer secondBar;  // assign a second bar to scale along
    public float colorChangeInterval = 0.1f; // seconds between strobe color changes

    [Header("Collision Settings")]
    public LayerMask playerLayer;      // assign the "player" layer

    private SpriteRenderer sr;
    private BoxCollider2D secondBarCollider;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Ensure both bars use Tiled mode
        sr.drawMode = SpriteDrawMode.Tiled;
        
        // Assign a random sprite from the list
        if (possibleSprites != null && possibleSprites.Length > 0)
        {
            sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        }

        if (secondBar != null)
        {
            secondBar.drawMode = SpriteDrawMode.Tiled;

            // Add or get a BoxCollider2D on the second bar
            secondBarCollider = secondBar.GetComponent<BoxCollider2D>();
            if (secondBarCollider == null)
            {
                secondBarCollider = secondBar.gameObject.AddComponent<BoxCollider2D>();
            }
            secondBarCollider.isTrigger = true;

            // Start strobe coroutine
            StartCoroutine(StrobeSecondBar());
        }

        // Set initial widths
        // SetWidth(sr, minWidth);
        // if (secondBar != null)
        //     SetWidth(secondBar, minWidth);

        StartCoroutine(GrowBar());
    }

    private IEnumerator GrowBar()
    {
        // Phase 1: slow growth
        while (sr.size.x < maxWidth && slowPhaseDuration > 0f)
        {
            Vector2 size = sr.size;
            size.x += slowSpeed * Time.deltaTime;
            if (size.x > maxWidth) size.x = maxWidth;
            sr.size = size;

            if (secondBar != null)
            {
                SetWidth(secondBar, sr.size.x);
                UpdateSecondBarCollider();
            }

            slowPhaseDuration -= Time.deltaTime;
            yield return null;
        }

        // Phase 2: fast growth
        while (sr.size.x < maxWidth)
        {
            Vector2 size = sr.size;
            size.x += fastSpeed * Time.deltaTime;
            if (size.x > maxWidth) size.x = maxWidth;
            sr.size = size;

            if (secondBar != null)
            {
                SetWidth(secondBar, sr.size.x);
                UpdateSecondBarCollider();
            }

            yield return null;
        }
        yield return new WaitForSeconds(.10f);
        Destroy(gameObject);
    }

    private IEnumerator StrobeSecondBar()
    {
        while (true)
        {
            if (secondBar != null)
            {
                // Random bright colors for strobe
                Color randomColor = new Color(
                    Random.Range(0.5f, 1f),
                    Random.Range(0.5f, 1f),
                    Random.Range(0.5f, 1f)
                );
                secondBar.color = randomColor;
            }
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    private void SetWidth(SpriteRenderer bar, float width)
    {
        Vector2 size = bar.size;
        size.x = width;
        bar.size = size;
    }

private void UpdateSecondBarCollider()
{
    if (secondBarCollider == null || secondBar == null) return;

    // Set collider size to match sprite size
    secondBarCollider.size = secondBar.size;

    // Align collider so growth happens from pivot (left edge)
    // Assuming the sprite pivot is at left (x=0)
    float offsetX = secondBar.size.x / 2f; // half-width to move collider center to the right
    float offsetY = secondBar.size.y / -2f; // vertical center

    secondBarCollider.offset = new Vector2(offsetX, offsetY);
}


    public void ChildTriggerEnter(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            KillPlayer(other.gameObject);
        }
    }


    private void KillPlayer(GameObject player)
    {
        Debug.Log("Player ded");
        PlayerMovement.Instance.FlipSpriteOnDeath();
        DeathAndReset.Instance.TriggerDeath();
    }
}
