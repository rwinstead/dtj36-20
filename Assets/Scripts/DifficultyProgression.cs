using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyProgression : MonoBehaviour
{
    [Header("Phase 1 Settings")]
    [SerializeField] float phase1DifficultyTimeStep = 5f;
    [SerializeField] float phase1SpawnRateIncrease = .25f;

    [Header("Phase 2 Settings")]
    [SerializeField] float phase2DifficultyTimeStep = 5f;
    [SerializeField] float phase2SpawnRateIncrease = .25f;

    [Header("Phase Transition")]
    [SerializeField] float timeToTriggerPhase2 = 58f;

    [SerializeField] GameObject blackholeSpawner;

    public enum GamePhase
    {
        Intro,
        Phase1,
        Phase2,
        End
    }

    public GamePhase currentPhase = GamePhase.Intro;

    private Coroutine phaseRoutine;

    void Start()
    {
        StartPhase(currentPhase);
        StartCoroutine(Phase2Timer()); // Start countdown immediately
        StartCoroutine(EndScreenTimer());
    }

    /// <summary>
    /// Automatically move to Phase2 after X seconds
    /// </summary>
    private IEnumerator Phase2Timer()
    {
        yield return new WaitForSeconds(timeToTriggerPhase2);

        // Only switch if we haven't hit Phase2 or End already
        if (currentPhase != GamePhase.Phase2 &&
            currentPhase != GamePhase.End)
        {
            SetPhase(GamePhase.Phase2);
        }
    }

    private IEnumerator EndScreenTimer()
    {
        yield return new WaitForSeconds(118f);

        SetPhase(GamePhase.End);
        SceneManager.LoadSceneAsync("Ending");
    }

    /// <summary>
    /// Call this when you want to move to another phase.
    /// </summary>
    public void SetPhase(GamePhase newPhase)
    {
        if (newPhase == currentPhase) return;

        currentPhase = newPhase;

        if (phaseRoutine != null)
        {
            StopCoroutine(phaseRoutine);
            phaseRoutine = null;
        }

        StartPhase(newPhase);
    }

    private void StartPhase(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.Phase1:
                phaseRoutine = StartCoroutine(Phase1Routine());
                break;

            case GamePhase.Phase2:
                phaseRoutine = StartCoroutine(Phase2Routine());
                break;
        }
    }

    // -----------------------------
    // Phase 1
    // -----------------------------
    private IEnumerator Phase1Routine()
    {
        Debug.Log("Phase 1 started");

        while (currentPhase == GamePhase.Phase1)
        {
            if( BarSpawner.Instance.spawnRate> 0.375f)
            {
                yield return new WaitForSeconds(phase1DifficultyTimeStep);
                BarSpawner.Instance.spawnRate = BarSpawner.Instance.spawnRate * 0.85f; // Increase difficulty by reducing spawn rate
            }
            else
            {
                yield return null;
            }
        }
    }

    // -----------------------------
    // Phase 2
    // -----------------------------
    private IEnumerator Phase2Routine()
    {
        Debug.Log("Phase 2 started");
        blackholeSpawner.SetActive(true);

        while (currentPhase == GamePhase.Phase2)
        {
            if( BarSpawner.Instance.spawnRate> 0.3f)
            {
                yield return new WaitForSeconds(phase2DifficultyTimeStep);
                BarSpawner.Instance.spawnRate = BarSpawner.Instance.spawnRate * 0.95f;
            }
            else
            {
                yield return null;
            }
        }
    }
}
