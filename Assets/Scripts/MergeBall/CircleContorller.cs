using System;
using UnityEngine;

public class CircleContorller : MonoBehaviour
{
    private CircleModel CircleModel = new CircleModel();
    [SerializeField]private CircleView CircleView;

    private void Start()
    {
        CircleView.RegisterCollisionOtherBall(RegisterCollisionOtherBall);
    }

    private void RegisterCollisionOtherBall(Collision2D collision)
    {
        if (CircleModel.CheckSameCollsionName(collision.gameObject.name, CircleView.gameObject.name, CircleView.PerfabList))
        {
            CircleView.DestroySelf();
            CircleModel.CreateNewBall(collision);
        }
    }
}
