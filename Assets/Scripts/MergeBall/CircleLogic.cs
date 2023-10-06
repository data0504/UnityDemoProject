using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLogic : MonoBehaviour
{
    public Transform PutRange;
    public GameObject PrefabCircleR;
    public GameObject PrefabCircleG;
    public GameObject PrefabCircleB;
    private bool offLock = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == PrefabCircleR.name)
        {
            DestroySelf();
            CreateNewBall(collision,1);
        }

        if (collision.gameObject.name == PrefabCircleG.name)
        {
            DestroySelf();
            CreateNewBall(collision, 2);
        }
    }

    private static void CreateNewBall(Collision2D collision, int value)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 contactPoint = contact.point;
        GameScore.MergeBallSameCreateBallNumber = value;
        GameScore.MergeBallSameCreateBallPos = contactPoint;
    }

    private void DestroySelf()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
    }
}
