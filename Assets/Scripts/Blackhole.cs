using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [HideInInspector] public float minSpeed;
    [HideInInspector] public float maxSpeed;
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public float halfWidth;
    [HideInInspector] public float halfHeight;

    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // Move right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy if out of horizontal bounds
        if (transform.position.x > mainCamera.transform.position.x + halfWidth + 1f ||
            transform.position.y > mainCamera.transform.position.y + halfHeight + 1f ||
            transform.position.y < mainCamera.transform.position.y - halfHeight - 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerMovement.Instance.FlipSpriteOnDeath();
            DeathAndReset.Instance.TriggerDeath();
        }
    }
}
