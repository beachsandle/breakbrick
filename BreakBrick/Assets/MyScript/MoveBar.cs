using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float barSpeed = 10f;

    void FixedUpdate()
    {
        if((Input.GetKey(KeyCode.LeftArrow) == true) && (transform.position.x > -2))
        {
            transform.Translate(Vector3.left * barSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.RightArrow) == true) && (transform.position.x < 2))
        {
            transform.Translate(Vector3.right * barSpeed * Time.deltaTime);
        }
    }
}