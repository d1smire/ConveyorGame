using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreSO;
    [SerializeField] private ConveyorManager conveyorManager;
    [SerializeField] private CanvasManager canvasManager;

    public float _Score;

    private void OnDestroy()
    {
        conveyorManager.IsWaveEnd -= IsWaveEnd;
        canvasManager.IsTimerEnd -= IsWaveEnd;
    }

    private void Awake()
    {
        if(scoreSO == null) 
        {
            scoreSO = Resources.Load<ScoreSO>("Resources/Saves/");
        }
        conveyorManager.IsWaveEnd += IsWaveEnd;
        canvasManager.IsTimerEnd += IsWaveEnd;
    }

    private void Start()
    {
        canvasManager.UpdateTextOnCanvas(Convert.ToString(_Score), Convert.ToString(scoreSO.day), Convert.ToString(scoreSO.money), "");
    }

    private void IsWaveEnd(bool isEnd) 
    {
        if (isEnd)
        {
            var money = _Score / 10;
            scoreSO.AddRemoveMoney(money, scoreSO.day + 1);
            _Score = 0;
            canvasManager.OnWaveEnd(true);
            conveyorManager.StartPauseGame(false);
            canvasManager.UpdateTextOnCanvas(Convert.ToString(_Score), Convert.ToString(scoreSO.day), Convert.ToString(scoreSO.money), "");
        }
        else if (!isEnd)
        {
            conveyorManager.StartPauseGame(true);
            conveyorManager.StartNewWave();
        }
    }

    public void AddRemoveScore(float amount) 
    {
        _Score += amount;
        canvasManager.UpdateTextOnCanvas(Convert.ToString(_Score), "", "", "");
    }
}