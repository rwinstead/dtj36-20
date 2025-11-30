using System.Collections;
using UnityEngine;

public class DifficultyProgression : MonoBehaviour
{
    [Header("Phase 1 Settings")]
    [SerializeField] float phase1DifficultyTimeStep = 5f;
    [SerializeField] float phase1SpawnRateIncrease = .25f;

    [Header("Phase 2 Settings")]
    [SerializeField] float phase2DifficultyTimeStep = 5f;
    [SerializeField] float phase2SpawnRateIncrease = .25f;

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
            yield return new WaitForSeconds(phase1DifficultyTimeStep);
            BarSpawner.Instance.spawnRate = BarSpawner.Instance.spawnRate + phase1SpawnRateIncrease;

        }
    }

    // -----------------------------
    // Phase 2
    // -----------------------------
    private IEnumerator Phase2Routine()
    {
        Debug.Log("Phase 2 started");

        while (currentPhase == GamePhase.Phase2)
        {
            yield return new WaitForSeconds(phase2DifficultyTimeStep);
            BarSpawner.Instance.spawnRate = BarSpawner.Instance.spawnRate + phase2SpawnRateIncrease;
        }
    }
}
