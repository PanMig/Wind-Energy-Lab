using UnityEngine;

public class TurbineSpawnManager : MonoBehaviour
{

    [Header("Prefab")]
    public GameObject turbinePrefab;
    [Space]
    [Header("SpawnPoint")]
    public Transform spawnPoint;
    [Space]
    [Header("Counters")]
    public float posIncrement;
    public int numberOfTurbines = 0;
    public int numberOfTurbinesOperating = 0;
    private int maxNumberOfTurbines ; //public to be changed from the inspector


    void Awake()
    {
        maxNumberOfTurbines = TurbineSelector.numberOfTurbines;
        if (maxNumberOfTurbines == 0) maxNumberOfTurbines = 10;
    }

    void Update()
    {
        SpawnTurbines();
    }

    void SpawnTurbines()
    {
        if (numberOfTurbines < maxNumberOfTurbines)
        {
            for (int i = 0; i < maxNumberOfTurbines; i++)
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x +
                posIncrement, spawnPoint.position.y, spawnPoint.position.z);
                Instantiate(turbinePrefab, spawnPoint.position, spawnPoint.rotation); // adds turbines to the specified transform point (spawnPoint).
                numberOfTurbines++;
            }
        }

    }
}
