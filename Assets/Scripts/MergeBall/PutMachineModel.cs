using UnityEngine;

public class PutMachineModel
{
    public float MoveSpeed = 0.03f;
    public float MoveMax = 3.95f;
    public float[] BallScale = new float[3] { 1.5f, 2.0f, 2.5f };
    public float[] BallScaleList
    {
        get { return BallScale; }
    }

    public bool CheckClipListLenght(int clipListLenght)
    {
        if (clipListLenght == 2) return true;
        return false;
    }

    public bool CheckUpdateBall()
    {
        if (GameScore.MergeBallSameCreateBallNumber == 1 ||
            GameScore.MergeBallSameCreateBallNumber == 2)
        {
            return true;
        }
        return false;
    }
    public bool CheckMoveRightMax(Transform putMachinePos)
    {
        if (putMachinePos.position.x > MoveMax)
        {
            return true;
        }
        return false;
    }
    public bool CheckMoveLeftMax(Transform putMachinePos)
    {
        if (putMachinePos.position.x < -MoveMax)
        {
            return true;
        }
        return false;
    }
}
