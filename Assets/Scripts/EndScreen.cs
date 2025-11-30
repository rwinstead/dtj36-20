using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [Header("Objects to enable one by one")]
    public GameObject[] items; // Assign 3 objects

    [Header("Background image to fade out")]
    public Image background; // UI Image

    [Header("Timings")]
    public float enableDelay = 1f;
    public float holdDelay = 2f; // After all enabled
    public float fadeDuration = 1.5f;

    [SerializeField]
    GameObject finalScreen;

    private void Start()
    {
        StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        // Ensure they're all disabled first
        foreach (var obj in items)
            obj.SetActive(false);

        // Enable one by one
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(true);
            yield return new WaitForSeconds(enableDelay);
        }

        // Wait after all enabled
        yield return new WaitForSeconds(holdDelay);

        // Fade background to black
        yield return StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        Color start = background.color;
        Color target = new Color(0, 0, 0, 1); // pure black

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            background.color = Color.Lerp(start, target, t);
            yield return null;
        }
        // Disable all items
        foreach (var obj in items)
            obj.SetActive(false);

        yield return new WaitForSeconds(1f);
        finalScreen.SetActive(true);
    }
}
