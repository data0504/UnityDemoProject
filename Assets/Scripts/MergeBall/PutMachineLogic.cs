using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutMachineLogic : MonoBehaviour
{
    public Transform PutMachineClip;
    public Transform TransPutMachine;
    public Transform TransPutRange;
    public List<GameObject> PrefabList = new List<GameObject>();
    public float MoveSpeed = 0.03f;
    public float MoveMax = 3.95f;

    private List<GameObject> clipList = new List<GameObject>();

    void Start()
    {
        CreateBalls(2);
        MonitorLoadedPutMachine();
        MonitorBallSame();
    }
    private void CreateBalls(int value)
    {
        for (int i = 0; i < value; i++)
        {
            AddclipBall();
        }
    }
    private void MonitorLoadedPutMachine()
    {
        if (clipList.Count == 2)
        {
            Vector2 newPos = new Vector2()
            {
                x = TransPutMachine.position.x,
                y = TransPutMachine.position.y - 0.8f
            };
            clipList[0].GetComponent<Transform>().position = newPos;
            clipList[0].transform.SetParent(TransPutMachine);
        }
        Invoke(nameof(MonitorLoadedPutMachine), Time.deltaTime);
    }

    private void MonitorBallSame()
    {
        if (GameScore.MergeBallSameCreateBallNumber == 1)
        {
            CreateConditionBall(1);
        }
        if (GameScore.MergeBallSameCreateBallNumber == 2)
        {
            CreateConditionBall(2);
        }
        Invoke(nameof(MonitorBallSame), Time.deltaTime);
    }
    private void CreateConditionBall(int value)
    {
        GameObject newBall = Instantiate(PrefabList[value], TransPutRange);
        newBall.GetComponent<CircleLogic>().PutRange = TransPutRange;
        newBall.transform.position = GameScore.MergeBallSameCreateBallPos;

        GameScore.ResetMergeBallSameInfo();
    }
    private void AddclipBall()
    {
        int RandomBall = Random.Range(0, PrefabList.Count);

        GameObject columnObj = Instantiate(PrefabList[RandomBall], PutMachineClip);
        columnObj.GetComponent<CircleLogic>().PutRange = TransPutRange;
        columnObj.GetComponent<Rigidbody2D>().simulated = false;

        Vector2 newPos = new Vector2()
        {
            x = PutMachineClip.position.x,
            y = PutMachineClip.position.y
        };
        columnObj.GetComponent<Transform>().position = newPos;

        clipList.Add(columnObj);
    }

    void Update()
    {
        PutPrefabObj();
        MovePutMachine();
    }
    public void  PutPrefabObj()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddclipBall();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            clipList[0].GetComponent<Transform>().SetParent(TransPutRange);
            clipList[0].GetComponent<Rigidbody2D>().simulated = true; 
            clipList.Remove(clipList[0]);
        }
    }
    private void MovePutMachine()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TransPutMachine.position = new Vector2(TransPutMachine.position.x - MoveSpeed, TransPutMachine.position.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TransPutMachine.position = new Vector2(TransPutMachine.position.x + MoveSpeed, TransPutMachine.position.y);
        }

        if (TransPutMachine.position.x > MoveMax)
        {
            TransPutMachine.position = new Vector2(-MoveMax, TransPutMachine.position.y);
        }
        if (TransPutMachine.position.x < -MoveMax)
        {
            TransPutMachine.position = new Vector2(MoveMax, TransPutMachine.position.y);
        }
    }
}
