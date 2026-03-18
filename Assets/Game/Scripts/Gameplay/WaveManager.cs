using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private Score score;
    [SerializeField] private ConveyorManager conveyorManager;
    [SerializeField] private FXManager fxManager;

    [Header("Wave Settings")]
    [SerializeField] private float spawnDelay = 2f;

    private int waveAmount = 0;
    private float timeDelay = 0f;
    private bool isWaveActive = false;

    public event Action<int> OnWaveAmountChanged;

    private void Start()
    {
        EndWave();
    }

    private void OnEnable()
    {
        score.OnScoreChange += canvasManager.UpdateScoreTextOnCanvas;
        conveyorManager.OnScoreEarned += score.AddRemoveScore;
        conveyorManager.OnScoreEarnedFX += fxManager.PlayFeedback;
        canvasManager.OnTimerEnd += StartNewWave;
        score.OnScoreUpdate += canvasManager.UpdateStatsTextOnCanvas;
        this.OnWaveAmountChanged += canvasManager.UpdateAmountTextOnCanvas;
    }

    private void OnDisable()
    {
        score.OnScoreChange -= canvasManager.UpdateScoreTextOnCanvas;
        conveyorManager.OnScoreEarned -= score.AddRemoveScore;
        conveyorManager.OnScoreEarnedFX -= fxManager.PlayFeedback;
        canvasManager.OnTimerEnd -= StartNewWave;
        score.OnScoreUpdate -= canvasManager.UpdateStatsTextOnCanvas;
        this.OnWaveAmountChanged -= canvasManager.UpdateAmountTextOnCanvas;
    }

    private void Update()
    {
        if (!isWaveActive) return;

        if (waveAmount > 0)
        {
            timeDelay += Time.deltaTime;

            if (timeDelay >= spawnDelay)
            {
                conveyorManager.LaunchRandomToteFromPool();
                waveAmount--;
                timeDelay = 0f;

                OnWaveAmountChanged?.Invoke(waveAmount);
            }
        }
        else if (!conveyorManager.IsSomeTotesActiveOnScene())
        {
            EndWave();
        }
    }

    public void StartNewWave()
    {
        waveAmount = UnityEngine.Random.Range(7, 50);
        OnWaveAmountChanged?.Invoke(waveAmount);
        timeDelay = 0f;
        isWaveActive = true;
    }

    private void EndWave()
    {
        isWaveActive = false;
        score.SaveScoreToSO();
        canvasManager.StartWaveTimer();
    }
}
