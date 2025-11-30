using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject barPrefab;
    public float spawnY = 0f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minSpacing = 2f; // minimum horizontal spacing between bars

    [HideInInspector] public float spawnRate = 2f;
    float spawnDuration = 99999f;

    private bool spawning = false;
    private List<GameObject> activeBars = new List<GameObject>();
    public static BarSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BeginSpawning()
    {
        if (spawning) return;
        spawning = true;
        StartCoroutine(SpawnBars());
    }

    private IEnumerator SpawnBars()
    {
        float timer = 0f;

        while (spawning)
        {
            Vector3 spawnPos;
            int attempts = 0;
            const int maxAttempts = 10;

            // Try to find a non-overlapping X position
            do
            {
                float randomX = Random.Range(minX, maxX);
                spawnPos = new Vector3(randomX, spawnY, 0f);
                attempts++;
            }
            while (IsOverlapping(spawnPos.x) && attempts < maxAttempts);

            // Spawn bar if position is valid
            if (attempts < maxAttempts)
            {
                GameObject bar = Instantiate(barPrefab, spawnPos, barPrefab.transform.rotation);
                activeBars.Add(bar);

                // Optionally, remove bar from list when destroyed
                StartCoroutine(RemoveWhenDestroyed(bar));
            }

            yield return new WaitForSeconds(spawnRate);

            if (spawnDuration > 0f)
            {
                timer += spawnRate;
                if (timer >= spawnDuration)
                    spawning = false;
            }
        }
    }

    private bool IsOverlapping(float x)
    {
        foreach (GameObject bar in activeBars)
        {
            if (bar == null) continue;
            float barX = bar.transform.position.x;
            if (Mathf.Abs(barX - x) < minSpacing)
                return true;
        }
        return false;
    }

    private IEnumerator RemoveWhenDestroyed(GameObject bar)
    {
        while (bar != null)
        {
            yield return null;
        }
        activeBars.Remove(bar);
    }
}
