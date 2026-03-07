using UnityEngine;

public class ConveyorMenuMove : MonoBehaviour
{
    [SerializeField] private GameObject conveyor;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject[] totesToSpawn;
    
    public float SpawningDeley;
    public float ConveyorSpeed;

    private float timeDeley;

    private void Update()
    {
        timeDeley += Time.deltaTime;

        if (timeDeley >= SpawningDeley)
        {
            var randomTote = Random.Range(0, 2);
            var chanceOnGreen = Random.Range(0, 10);
            if (chanceOnGreen < 9)
                SpawnTote(totesToSpawn[randomTote], ConveyorSpeed);
            else
                SpawnTote(totesToSpawn[2], ConveyorSpeed);

            timeDeley = 0;
        }
    }

    private void SpawnTote(GameObject tote, float ToteSpeed) 
    {
        var SpawnedTote = Instantiate(tote).GetComponent<ToteMovement>();

        SpawnedTote.gameObject.transform.position = startPoint.transform.position;
        SpawnedTote.SetValues(endPoint.transform, ConveyorSpeed);
    }
}
