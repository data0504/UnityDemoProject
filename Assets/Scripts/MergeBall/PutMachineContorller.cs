using System;
using UnityEngine;

public class PutMachineContorller : MonoBehaviour
{
    public PutMachineModel putMachineModel = new PutMachineModel();
    public PutMachineView putMachineView = new PutMachineView();
    void Start()
    {
        putMachineView.CreateBalls(putMachineModel.BallScaleList);
        MonitorLoadedPutMachine();
        MonitorBallSame();
    }
    private void MonitorLoadedPutMachine()
    {
        if (putMachineModel.CheckClipListLenght(putMachineView.ClipList.Length))
        {
            putMachineView.LoadeBallPutMachine();
        }
        Invoke(nameof(MonitorLoadedPutMachine), Time.deltaTime);
    }

    private void MonitorBallSame()
    {
        if (putMachineModel.CheckUpdateBall())
        {
            putMachineView.CreateConditionBall(GameScore.MergeBallSameCreateBallNumber, putMachineModel.BallScaleList);
            GameScore.ResetMergeBallSameInfo();
        }
        Invoke(nameof(MonitorBallSame), Time.deltaTime);
    }

    void Update()
    {
        PutPrefabObj();
        MovePutMachine();
        MoveMaxPutMachine();
    }


    public void PutPrefabObj()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            putMachineView.RefullBall(putMachineModel.BallScaleList);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            putMachineView.PushOnClipBall();
        }
    }
    private void MovePutMachine()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            putMachineView.MoveLeft(putMachineModel.MoveSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            putMachineView.MoveRight(putMachineModel.MoveSpeed);
        }
    }
    private void MoveMaxPutMachine()
    {
        if (putMachineModel.CheckMoveLeftMax(putMachineView.gameObject.transform))
        {
            putMachineView.MoveMaxLeft(putMachineModel.MoveMax);
        }
        if (putMachineModel.CheckMoveRightMax(putMachineView.gameObject.transform))
        {
            putMachineView.MoveMaxRight(putMachineModel.MoveMax);
        }
    }
}
