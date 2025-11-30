using UnityEngine;
using System.Collections;

public class BlackholeSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject blackHolePrefab;
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnInterval = 2f;

    [Header("Black Hole Movement")]
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    private Camera mainCamera;
    private float halfWidth;
    private float halfHeight;
    public static BlackholeSpawner Instance;

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

    void Start()
    {
        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * mainCamera.aspect;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnBlackHole();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBlackHole()
    {
        if (blackHolePrefab == null) return;

        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, yPos, 0f);

        GameObject bh = Instantiate(blackHolePrefab, spawnPos, Quaternion.identity);

        // Add controller component dynamically
        Blackhole bhController = bh.AddComponent<Blackhole>();
        bhController.minSpeed = minSpeed;
        bhController.maxSpeed = maxSpeed;
        bhController.mainCamera = mainCamera;
        bhController.halfWidth = halfWidth;
        bhController.halfHeight = halfHeight;
    }
}
