using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] objects;

    public float minSpawnTime, maxSpawnTime;
    public int spawnCount;

    public bool spawnMovingObjects = false;

    void Start()
    {
        if (spawnMovingObjects)
        {
            SpawnMovingObjects();
        }
        else
        {
            //SpawnStaticObjects();
        }
    }

    void SpawnMovingObjects()
    {
        Instantiate(objects[Random.Range(0, objects.Length)], transform);

        Invoke("SpawnMovingObjects", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
