using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WordCycler : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text centerText;

    [Header("Words")]
    public List<string> words = new List<string>();

    [Header("Settings")]
    public float displayDuration = 0.8f;

    void Start()
    {
        StartCoroutine(CycleWords());
    }

    IEnumerator CycleWords()
    {
        for (int index = 0; index < words.Count; index++)
        {
            centerText.text = words[index];
            yield return new WaitForSeconds(displayDuration);
        }

        centerText.text = "";
        gameObject.SetActive(false);
    }
}
