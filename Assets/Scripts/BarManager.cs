using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BarManager : MonoBehaviour
{
    [Header("Width Settings")]
    public float minWidth = 1f;       // starting width in world units
    public float maxWidth = 5f;       // target width in world units

    [Header("Phase Settings")]
    public float slowSpeed = 0.5f;    // units per second for slow phase
    public float fastSpeed = 3f;      // units per second for fast phase

    [Header("Phase Timing")]
    public float slowPhaseDuration = 2f; // optional, can limit slow phase to time

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Make sure sprite renderer is in Tiled mode
        sr.drawMode = SpriteDrawMode.Tiled;

        // Set initial width
        Vector2 size = sr.size;
        size.x = minWidth;
        sr.size = size;

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

            slowPhaseDuration -= Time.deltaTime; // limit slow phase by duration
            yield return null;
        }

        // Phase 2: fast growth until maxWidth
        while (sr.size.x < maxWidth)
        {
            Vector2 size = sr.size;
            size.x += fastSpeed * Time.deltaTime;
            if (size.x > maxWidth) size.x = maxWidth;
            sr.size = size;

            yield return null;
        }
    }
}
