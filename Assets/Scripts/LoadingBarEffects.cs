using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingEffects : MonoBehaviour
{
    [Header("Loading Bar")]
    public Image loadingBar;
    public float loadDuration = 2f;

    [Header("Strobe Image")]
    public Image strobeImage;
    public float minStrobeInterval = 0.03f;
    public float maxStrobeInterval = 0.10f;

    private Coroutine fillRoutine;
    private Coroutine strobeRoutine;

    void Start()
    {
        fillRoutine = StartCoroutine(FillBarOnce());
        strobeRoutine = StartCoroutine(StrobeLoop());
    }

    IEnumerator FillBarOnce()
    {
        float t = 0f;
        loadingBar.fillAmount = 0f;

        while (t < loadDuration)
        {
            t += Time.unscaledDeltaTime;
            loadingBar.fillAmount = Mathf.Clamp01(t / loadDuration);
            yield return null;
        }

        // Safety clamp
        loadingBar.fillAmount = 1f;

        // Stop strobe and kill both coroutines
        if (strobeRoutine != null)
            StopCoroutine(strobeRoutine);

        // Optionally disable strobe completely
        // strobeImage.enabled = false;

        // End here — no loop
        gameObject.SetActive(false);
    }

    IEnumerator StrobeLoop()
    {
        while (true)
        {
            float v = Random.Range(0f, 1f); // grayscale
            strobeImage.color = new Color(v, v, v, 1f);

            yield return new WaitForSecondsRealtime(
                Random.Range(minStrobeInterval, maxStrobeInterval)
            );
        }
    }
}
