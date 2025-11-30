using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Names")]
    [SerializeField] private string titleSceneName   = "Title";
    [SerializeField] private string introSceneName   = "Intro";
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private string endingSceneName   = "Ending";

    [Header("Intro Timing")]
    [SerializeField] private float introDuration = 25f;   // seconds before auto-skip
    private float introTimer = 0f;

    [Header("Intro Settings")]
    public bool introSkippable = true;
    private bool introFinished = false;

    [Header("Gameplay Timing")]
    [SerializeField] private float gameplayDuration = 10f;   // seconds to survive to win
    private float gameplayTimer = 0f;
    private bool gameplayFinished = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (SceneManager.GetActiveScene().name != titleSceneName)
            {
                SceneManager.LoadScene(titleSceneName);
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        var currentScene = SceneManager.GetActiveScene().name;

        // INTRO LOGIC
        if (!introFinished && currentScene == introSceneName)
        {
            HandleIntroSkip();
            HandleIntroTimer();
        }

        // GAMEPLAY LOGIC
        if (!gameplayFinished && currentScene == gameplaySceneName)
        {
            HandleGameplayTimer();
        }
    }

    // -------------------- TITLE FLOW --------------------

    public void LoadIntroScene()
    {
        introFinished = false;
        introTimer = 0f;

        SceneManager.LoadScene(introSceneName);
    }

    // -------------------- INTRO LOGIC --------------------

    void HandleIntroSkip()
    {
        if (!introSkippable || introFinished) return;
        if (SceneManager.GetActiveScene().name != introSceneName) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    
    void HandleIntroTimer()
    {
        introTimer += Time.deltaTime;

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

    // -------------------- GAMEPLAY FLOW --------------------
    void HandleGameplayTimer()
    {
        gameplayTimer += Time.deltaTime;

        // Debug.Log($"Gameplay timer: {gameplayTimer}");

        if (gameplayTimer >= gameplayDuration)
        {
            GameplaySucceeded();
        }
    }

    private void GameplaySucceeded()
    {
        gameplayFinished = true;

        // Optional: small delay, or screen flash here if you want
        SceneManager.LoadScene(endingSceneName);
    }

    public void RestartGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
