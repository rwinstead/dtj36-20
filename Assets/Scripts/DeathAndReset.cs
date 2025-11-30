using UnityEngine;

public class DeathAndReset : MonoBehaviour
{
    [SerializeField] GameObject deathUI;

    public static DeathAndReset Instance;
    bool isDead = false;

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

    public void TriggerDeath()
    {
        isDead = true;
        deathUI.SetActive(true);
        MusicManager.Instance.StopMusic();
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDead)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;

        // Use the GameManager instead of manual scene reload
        GameManager.Instance.RestartGameplay();
    }
}
