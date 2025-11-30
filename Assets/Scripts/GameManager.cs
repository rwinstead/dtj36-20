using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Names")]
    [SerializeField] private string introSceneName = "Intro";
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private string endingSceneName = "Ending";

    [Header("Intro Timing")]
    [SerializeField] private float introDuration = 25f;   // number of seconds before auto-skip
    private float introTimer = 0f;

    [Header("Intro Settings")]
    public bool introSkippable = true;
    public KeyCode skipKey = KeyCode.Tab;
    private bool introFinished = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (!introFinished)
        {
            HandleIntroSkip();
            HandleIntroTimer();
        }
    }

    // --------------------------------------------------------------------
    // INTRO LOGIC
    // --------------------------------------------------------------------
    void HandleIntroSkip()
    {
        if (!introSkippable || introFinished) return;

        if (SceneManager.GetActiveScene().name != introSceneName)
            return;

        if (Input.GetKeyDown(skipKey))
        {
            StartGame();
        }
    }
    
    void HandleIntroTimer()
    {
        introTimer += Time.deltaTime;

        Debug.Log(introTimer);

        if (introTimer > introDuration)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (ScreenFlash.Instance != null)
        {
            ScreenFlash.Instance.FlashWhite();
        }

        introFinished = true;
        SceneManager.LoadScene(gameplaySceneName);
    }

    // --------------------------------------------------------------------
    // GAMEPLAY FLOW
    // --------------------------------------------------------------------
    public void PlayerDied()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void PlayerSucceeded()
    {
        SceneManager.LoadScene(endingSceneName);
    }

    public void RestartGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
