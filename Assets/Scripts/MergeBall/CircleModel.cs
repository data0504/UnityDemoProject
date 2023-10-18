using UnityEngine;

public class CircleModel
{
    public int BallNumber {get;set;}

    public bool CheckSameCollsionName(string collisionName, string selfName, GameObject[] circleList)
    {
        BallNumber = 0;

        if (collisionName == circleList[0].name && selfName == circleList[0].name) 
        {
            BallNumber = 1;
            return true;
        }

        if (collisionName == circleList[1].name && selfName == circleList[1].name)
        {
            BallNumber = 2;
            return true;
        }

        return false;
    }
    public void CreateNewBall(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collisionPos = contact.point;
        GameScore.MergeBallSameCreateBallPos = collisionPos;
        GameScore.MergeBallSameCreateBallNumber = BallNumber;
    }
}
