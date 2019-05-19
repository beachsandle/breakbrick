using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject brick = null;

    void Start()
    {
        SpawnBrick();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBrick()
    {
        for(int x = 0; x < 6; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                Instantiate(brick, new Vector3(-2.5f + x, 4.5f - y, 0), Quaternion.identity);
            }
        }
    }
}

