using UnityEngine;

public class BarChildTrigger : MonoBehaviour
{
    public BarManager parentManager; // assign in inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (parentManager != null)
        {
            parentManager.ChildTriggerEnter(other);
        }
    }
}
