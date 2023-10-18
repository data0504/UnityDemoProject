using UnityEngine;

public class PutMachineLogic : MonoBehaviour
{
    public Transform PutMachineClip;
    public Transform TransPutMachine;
    public Transform TransPutRange;
    public GameObject[] PrefabList = new GameObject[3];
    public float MoveSpeed = 0.03f;
    public float MoveMax = 3.95f;
    public float[] ballScale = new float[3] { 1.5f, 2.0f, 2.5f };

    private GameObject[] clipList = new GameObject[2];
    void Start()
    {
        CreateBalls();
        MonitorLoadedPutMachine();
        MonitorBallSame();
    }
    private void CreateBalls()
    {
        for (int i = 0; i < clipList.Length; i++)
        {
            AddclipBall();
        }
    }
    private void MonitorLoadedPutMachine()
    {
        if (clipList.Length == 2)
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
        newBall.transform.position = GameScore.MergeBallSameCreateBallPos;
        newBall.name = PrefabList[value].name;
        SetBallScale(newBall);

        GameScore.ResetMergeBallSameInfo();
    }
    private void AddclipBall()
    {
        int RandomBall = Random.Range(0, PrefabList.Length);

        GameObject columnObj = Instantiate(PrefabList[RandomBall], PutMachineClip);
        columnObj.GetComponent<Rigidbody2D>().simulated = false;
        columnObj.name = PrefabList[RandomBall].name;

        SetBallScale(columnObj);

        Vector2 newPos = new Vector2()
        {
            x = PutMachineClip.position.x,
            y = PutMachineClip.position.y
        };
        columnObj.GetComponent<Transform>().position = newPos;

        for (int i = 0; i < clipList.Length; i++)
        {
            if (clipList[i] == null) 
            {
                clipList[i] = columnObj;
                break;
            }
        }
    }

    private void SetBallScale(GameObject columnObj)
    {
        if (columnObj.name == PrefabList[0].name)
        {
            Vector2 newScale = new()
            {
                x = columnObj.transform.localScale.x * ballScale[0],
                y = columnObj.transform.localScale.y * ballScale[0]
            };
            columnObj.transform.localScale = newScale;
        }
        if (columnObj.name == PrefabList[1].name)
        {
            Vector2 newScale = new()
            {
                x = columnObj.transform.localScale.x * ballScale[1],
                y = columnObj.transform.localScale.y * ballScale[1]
            };
            columnObj.transform.localScale = newScale;
        }
        if (columnObj.name == PrefabList[2].name)
        {
            Vector2 newScale = new()
            {
                x = columnObj.transform.localScale.x * ballScale[2],
                y = columnObj.transform.localScale.y * ballScale[2]
            };
            columnObj.transform.localScale = newScale;
        }
    }

    void Update()
    {
        PutPrefabObj();
        MovePutMachine();
    }
    public void PutPrefabObj()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddclipBall();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            clipList[0].GetComponent<Transform>().SetParent(TransPutRange);
            clipList[0].GetComponent<Rigidbody2D>().simulated = true;
            clipList[0] = clipList[1];
            clipList[1] = null;
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
