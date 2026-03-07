using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public event Action<bool> IsTimerEnd;

    [Header("GameText")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI left;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Refs")]
    [SerializeField] private bool isWaveEnd = false;
    [SerializeField] private float timeBeforeNewWave;
    [SerializeField] private GameObject timerBeforeNewWave;

    public void UpdateTextOnCanvas(string Score = "", string Day = "", string Money = "", string Left = "")
    {
        if (!string.IsNullOrEmpty(Score))
            score.text = "Score: " + Score;

        if (!string.IsNullOrEmpty(Day))
            day.text = "Day: " + Day;

        if (!string.IsNullOrEmpty(Money))
            money.text = "Money: " + Money;

        if (!string.IsNullOrEmpty(Left))
            left.text = "Lefts: " + Left;
    }

    private void Update()
    {
        if (!isWaveEnd) return;

        StartCoroutine(WaveTimer(timeBeforeNewWave));
    }

    private IEnumerator WaveTimer(float duration)
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            timer.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        isWaveEnd = false;
        timerBeforeNewWave.SetActive(false);
        IsTimerEnd.Invoke(false);
    }

    public void OnWaveEnd(bool isEnd) 
    {
        isWaveEnd = isEnd;
        timerBeforeNewWave.SetActive(true);
    }

    public void BackToMenuBtn() 
    {
        SceneManager.LoadScene(0);
    }
}
