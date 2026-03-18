using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxColor 
{
    Yellow,
    Blue,
    Green,
}

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject[] totesToSpawn;
    [SerializeField] private GameObject parentOfPoolObj;
    [SerializeField] private bool IsCycled;

    public float conveyorSpeed;
    public float conveyorSpawnDeley;

    private Dictionary<BoxColor, List<ToteMovement>> boxPooler;

    public event Action<float> OnScoreEarned;
    public event Action<float, Vector3> OnScoreEarnedFX;

    private void Start()
    {
        boxPooler = new Dictionary<BoxColor, List<ToteMovement>>();
        SpawnTote(BoxColor.Yellow,20);
        SpawnTote(BoxColor.Blue, 20);
        SpawnTote(BoxColor.Green, 15);

        if (IsCycled) 
            StartCoroutine(StartCycle());
    }

    private IEnumerator StartCycle() 
    {
        yield return new WaitForSeconds(3f);

        while (IsCycled)
        {
            LaunchRandomToteFromPool();

            yield return new WaitForSeconds(conveyorSpawnDeley);
        }
    }

    public void LaunchRandomToteFromPool()
    {
        int number = UnityEngine.Random.Range(0, spawnPoints.Length);
        int colorsCount = Enum.GetValues(typeof(BoxColor)).Length;
        BoxColor randomColor = (BoxColor)UnityEngine.Random.Range(0, colorsCount);

        ToteMovement tote = GetAvailableTote(randomColor);

        if (tote != null)
        {
            tote.gameObject.transform.position = spawnPoints[number].position;
            tote.gameObject.SetActive(true);
        }
    }

    private ToteMovement GetAvailableTote(BoxColor color)
    {
        if(boxPooler.TryGetValue(color, out List<ToteMovement> pool)) 
        {
            foreach(var tote in pool) 
            {
                if (!tote.gameObject.activeInHierarchy) 
                {
                    return tote;
                }
            }
        }
        return null;
    }

    private void SpawnTote(BoxColor boxColor, int amount)
    {
        if (!boxPooler.ContainsKey(boxColor))
        {
            boxPooler.Add(boxColor, new List<ToteMovement>());
        }

        GameObject prefabToSpawn = GetPrefabByColor(boxColor);

        if (prefabToSpawn == null)
        {
            Debug.LogError($"Не знайдено префаб для кольору {boxColor}!");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            ToteMovement spawnedTote = Instantiate(prefabToSpawn, parentOfPoolObj.transform).GetComponent<ToteMovement>();

            spawnedTote.SetValues(destination.transform, conveyorSpeed);
            spawnedTote.OnReachedDestination += ToteReachDestination;
            spawnedTote.gameObject.SetActive(false);

            spawnedTote.OnToteProcessed += RelayToteScore;

            boxPooler[boxColor].Add(spawnedTote);
        }
    }

    private GameObject GetPrefabByColor(BoxColor color)
    {
        return color switch
        {
            BoxColor.Yellow => totesToSpawn[0],
            BoxColor.Blue => totesToSpawn[1],
            BoxColor.Green => totesToSpawn[2],
            _ => null,
        };
    }

    private void ToteReachDestination(ToteMovement tote)
    {
        tote.gameObject.transform.position = Vector3.zero;
        tote.gameObject.SetActive(false);
    }

    private void RelayToteScore(float score, Vector3 position)
    {
        OnScoreEarned?.Invoke(score);
        OnScoreEarnedFX?.Invoke(score, position);
    }

    public bool IsSomeTotesActiveOnScene() 
    {
        foreach (List<ToteMovement> toteList in boxPooler.Values)
        {
            foreach (ToteMovement tote in toteList)
            {
                if (tote != null && tote.gameObject.activeInHierarchy)
                {
                    return true;
                }
            }
        }
        return false;
    }
}