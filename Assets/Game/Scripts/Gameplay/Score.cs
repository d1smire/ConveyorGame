using System;
using UnityEngine;

public class Score : MonoBehaviour 
{
    [SerializeField] private ScoreSO scoreSO;

    private float Scores;

    public event Action<float> OnScoreChange;
    public event Action<ScoreSO> OnScoreUpdate;

    private void Start()
    {
        if(scoreSO == null)
        {
            scoreSO = Resources.Load<ScoreSO>("Saves/PScores");
        }
    }

    public void SaveScoreToSO()
    {
        if (scoreSO != null)
        {
            float money = Scores / 10;
            scoreSO.AddRemoveMoney(money, 1);
            Scores = 0;
            OnScoreChange?.Invoke(Scores);
            OnScoreUpdate?.Invoke(scoreSO);
        }
    }

    public void AddRemoveScore(float amount) 
    {
        Scores += amount;
        OnScoreChange?.Invoke(Scores);
    }
}