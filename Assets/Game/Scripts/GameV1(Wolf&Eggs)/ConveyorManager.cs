using System;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject[] totesToSpawn;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private List<GameObject> totes;

    public float conveyorSpeed;
    public float conveyorSpawnDeley;
    public event Action<bool> IsWaveEnd;

    private float timeDeley;
    private bool isGameStarted;
    private int waveAmount;
    private bool timerWorks;


    private void Start()
    {
        isGameStarted = true;
        timerWorks = true;
        StartNewWave();
    }

    private void Update()
    {
        if (isGameStarted && waveAmount > 0)
        {
            timeDeley += Time.deltaTime;
            if (timeDeley >= conveyorSpawnDeley)
            {
                var randSP = UnityEngine.Random.Range(0, spawnPoints.Length - 1);
                var randomTote = UnityEngine.Random.Range(0, 2);
                var chanceOnGreen = UnityEngine.Random.Range(0, 10);
                if (chanceOnGreen < 9)
                {
                    SpawnTote(totesToSpawn[randomTote], randSP, conveyorSpeed);
                }
                else
                {
                    SpawnTote(totesToSpawn[2], randSP, conveyorSpeed);
                }

                timeDeley = 0;
                waveAmount--;

                canvasManager.UpdateTextOnCanvas("", "", "", Convert.ToString(waveAmount));
            }
        }
        else if (totes.Count == 0)
        {
            if (!timerWorks)
            {
                timerWorks = true;
                IsWaveEnd.Invoke(true);
            }
        }
    }

    private void SpawnTote(GameObject tote, int randomSpawnPoint, float toteSpeed) 
    {
        var SpawnedTote = Instantiate(tote).GetComponent<ToteMovement>();

        SpawnedTote.gameObject.transform.position = spawnPoints[randomSpawnPoint].transform.position;
        SpawnedTote.SetValues(endPoint.transform, toteSpeed);
        SpawnedTote.destroyed += ToteDestroyed;
        totes.Add(SpawnedTote.gameObject);
    }

    private void ToteDestroyed(GameObject tote)
    {
        tote.GetComponent<ToteMovement>().destroyed -= ToteDestroyed;
        totes.Remove(tote);
    }

    public void StartPauseGame(bool isIt) 
    {
        isGameStarted = isIt;
    }

    public void StartNewWave()
    {
        if (timerWorks) 
        { 
            timerWorks = false;
            waveAmount = UnityEngine.Random.Range(7, 50);
            canvasManager.UpdateTextOnCanvas("", "", "", Convert.ToString(waveAmount)); 
        }
    }
}