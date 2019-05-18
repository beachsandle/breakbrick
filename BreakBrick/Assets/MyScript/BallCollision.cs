using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention
{
    public static float GetAngle(this Vector3 vec)
    {
        return Vector3.Angle(Vector3.up, vec);
    }
}
public class BallCollision : MonoBehaviour
{
    private Collision2D RecentCollision = null;
    private Vector3 Angle = new Vector3(0f, 1f);
    public float Speed = 5;


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.rotation * Angle * Speed * Time.deltaTime;
    }
    private void OnCollision(Collision2D coll)
    {
        RecentCollision = coll;
        var contactPoint = coll.contacts[0].point;
        var ToContact = new Vector3(contactPoint.x, contactPoint.y, 0) - transform.position;
        var contactAngle = ToContact.GetAngle();
        float moveAngle = transform.rotation.eulerAngles.z;
        var reflectAngle = 90 - moveAngle + contactAngle;
        moveAngle += reflectAngle * 2;
        moveAngle %= 360;
        transform.rotation = Quaternion.Euler(0, 0, moveAngle);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        OnCollision(coll);
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        OnCollision(coll);
    }
}
