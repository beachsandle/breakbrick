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
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                Instantiate(brick, new Vector3(-2.64f + x * 0.75f, 2f- y * 0.35f, 0), Quaternion.identity);
            }
        }
    }
}

