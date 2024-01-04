using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] handPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obter os dados do UDPReceive
        string data = udpReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);

        string[] points = data.Split(',');
        //print(data);
    }
    // Atualizar as posições dos pontos da mão com base nos dados recebidos
    void UpdateHandPoints(string[] points)
    {
        for (int i = 0; i < 21; i++)
        {
            float x = 7 - float.Parse(points[i * 3]) / 175;
            float y = float.Parse(points[i * 3 + 1]) / 175;
            float z = float.Parse(points[i * 3 + 2]) / 175;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}