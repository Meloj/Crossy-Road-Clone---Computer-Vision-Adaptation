using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject[] lanes;

    GameObject tempLane;

    int zPosition = 2;

    // Start is called before the first frame update
    void Start()
    {
        CreateLanes();
    }

    public void CreateLanes()
    {
        int i;
        for(i = zPosition; i < zPosition + 20; i++) 
        {
            tempLane = Instantiate(lanes[Random.Range(0, lanes.Length)], new Vector3(0, 0, i), Quaternion.identity) as GameObject;
            tempLane.transform.SetParent(gameObject.transform);
            i += tempLane.GetComponent<Lanes>().numberOfLanes - 1;
        }

        zPosition = i;
    }
}
