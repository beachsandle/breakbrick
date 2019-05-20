using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollision : MonoBehaviour
{
    private bool isDestroy = false;
    private BallCollision ballCollision;
    public GameObject Player;
    public float HP = 2;
    private void Start()
    {
        ballCollision = Player.GetComponent<BallCollision>();
    }
    private void LateUpdate()
    {
        if (isDestroy)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            HP -= ballCollision.Attak;
            if (HP <= 0)
                isDestroy = true;
        }
    }
}
