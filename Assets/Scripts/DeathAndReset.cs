using UnityEngine;

public class DeathAndReset : MonoBehaviour
{
    [SerializeField] GameObject deathUI;

    public static DeathAndReset Instance;

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
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
