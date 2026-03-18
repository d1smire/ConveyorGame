using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public event Action OnTimerEnd;

    [Header("Game Text")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI leftAmount;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Refs")]
    [SerializeField] private float timeBeforeNewWave;
    [SerializeField] private GameObject timerBeforeNewWave;

    public void UpdateStatsTextOnCanvas(ScoreSO scoreSO)
    {
        day.text = "Day: " + scoreSO.day;
        money.text = "Money: " + scoreSO.money;
    }

    public void UpdateScoreTextOnCanvas(float _score) 
    {
        score.text = "Score: " + _score;
    }

    public void UpdateAmountTextOnCanvas(int _leftAmount)
    {
        leftAmount.text = "Lefts: " + _leftAmount;
    }

    public void StartWaveTimer()
    {
        timerBeforeNewWave.SetActive(true);
        StartCoroutine(WaveTimerRoutine(timeBeforeNewWave));
    }

    private IEnumerator WaveTimerRoutine(float duration)
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            timer.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerBeforeNewWave.SetActive(false);
        OnTimerEnd?.Invoke();
    }

    public void BackToMenuBtn() 
    {
        SceneManager.LoadScene(0);
    }
}
