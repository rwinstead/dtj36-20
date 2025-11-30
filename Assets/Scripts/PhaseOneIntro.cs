using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOneIntro : MonoBehaviour
{
    [Header("Objects to toggle (in order)")]
    public List<GameObject> objects;

    [Header("Seconds each object stays active (except last)")]
    public float switchInterval = 1f;

    void Start()
    {
        // Disable all to start
        foreach (var obj in objects)
            obj.SetActive(false);

        if (objects.Count > 0)
            StartCoroutine(ActivationSequence());
    }

    IEnumerator ActivationSequence()
    {
        // Activate all except the last with timing
        for (int i = 0; i < objects.Count; i++)
        {
            // Turn all off
            for (int j = 0; j < objects.Count; j++)
                objects[j].SetActive(false);

            // Activate current
            objects[i].SetActive(true);

            // If this is the last object — stop permanently
            if (i == objects.Count - 1)
                yield break;

            // Wait then continue
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
