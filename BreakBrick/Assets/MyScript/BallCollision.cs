using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention
{
    public static Vector3 Projected(this Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }
    public static float GetAngle(this Vector3 vec)
    {
        return Vector3.Angle(Vector3.right, vec);
    }
}
public class BallCollision : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collision2D RecentCollision = null;
    private Vector3 Angle = new Vector3(0f, 1f);
    public float Speed = 5;
    public float Attak = 1;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = Quaternion.Euler(0, 0, rb2d.rotation) * Vector2.right * Speed;
        //transform.position += transform.rotation * Angle * Speed * Time.deltaTime;
    }
    private void Collosion2(Collision2D coll)
    {
        var ballColl = GetComponent<CircleCollider2D>();
        if (coll.gameObject.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        } else
        if (coll.gameObject.CompareTag("Player"))
        {
            var ballCenter = ballColl.bounds.center;
            var boxColl = coll.collider;
            var boxCenter = boxColl.bounds.center;
            var boxExtents = boxColl.bounds.extents;
            var moveAngle = transform.rotation.eulerAngles.z;
            var ballCenterToBoxCenter = boxCenter - ballCenter;
            var inLeft = ballCenter.x < boxCenter.x;
            var inTop = ballCenter.y > boxCenter.y;
            var inXSide = Math.Abs(ballCenterToBoxCenter.x) > boxExtents.x;
            var inYSide = Math.Abs(ballCenterToBoxCenter.y) > boxExtents.y;
            float contactAngle;
            var collidedToVertex = inXSide && inYSide;
            if (collidedToVertex)
            {
                var vertex = boxCenter;
                vertex.x += boxExtents.x * (inLeft ? -1 : 1);
                vertex.y += boxExtents.y * (inTop ? 1 : -1);
                contactAngle = (vertex - ballCenter).GetAngle();
                var reflectAngle = 90f - moveAngle + contactAngle;
                var lateAngle = (moveAngle + reflectAngle * 2) % 360;
                if (lateAngle > 170f)
                    lateAngle = 170f;
                else if (lateAngle < 10)
                    lateAngle = 10f;
                rb2d.rotation = lateAngle;
            }
            else
            {
                if (!inTop)
                    return;
                contactAngle = inXSide ? (inLeft ? 0f : 180f) : (inTop ? 90f : 270f);
                var reflectAngle = 90f - moveAngle + contactAngle;
                reflectAngle += (boxCenter.x - ballCenter.x) / boxExtents.x * 30f;
                var lateAngle = (moveAngle + reflectAngle * 2) % 360;
                if (lateAngle > 175f)
                    lateAngle = 175f;
                else if (lateAngle < 5)
                    lateAngle = 5f;
                rb2d.rotation = lateAngle;
            }
            
        }
        else
        if (coll.gameObject.CompareTag("Brick"))
        {

            var ballCenter = ballColl.bounds.center;
            var boxColl = coll.collider;
            var boxCenter = boxColl.bounds.center;
            var boxExtents = boxColl.bounds.extents;
            var moveAngle = transform.rotation.eulerAngles.z;
            var ballCenterToBoxCenter = boxCenter - ballCenter;
            var inLeft = ballCenter.x < boxCenter.x;
            var inTop = ballCenter.y > boxCenter.y;
            var inXSide = Math.Abs(ballCenterToBoxCenter.x) > boxExtents.x;
            var inYSide = Math.Abs(ballCenterToBoxCenter.y) > boxExtents.y;
            float contactAngle;
            var collidedToVertex = inXSide && inYSide;
            if (collidedToVertex)
            {
                var vertex = boxCenter;
                vertex.x += boxExtents.x * (inLeft ? -1 : 1);
                vertex.y += boxExtents.y * (inTop ? 1 : -1);
                contactAngle = (vertex - ballCenter).GetAngle();

            }
            else
            {
                contactAngle = inXSide ? (inLeft ? 0f : 180f) : (inTop ? 90f : 270f);
            }
            var reflectAngle = 90f - moveAngle + contactAngle;
            rb2d.rotation = (moveAngle + reflectAngle * 2) % 360;
        }
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
        rb2d.rotation = moveAngle;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        Collosion2(coll);
    }
}
