using UnityEngine;

public class PutMachineView : MonoBehaviour
{
    public Transform PutMachineClip;
    public Transform TransPutMachine;
    public Transform TransPutRange;
    public GameObject[] PrefabList = new GameObject[3];

    private GameObject[] clip = new GameObject[2];
    public GameObject[] ClipList
    {
        get { return clip; }
        set { clip = value; }
    }

    public void CreateBalls(float[] ballScaleList)
    {
        for (int i = 0; i < clip.Length; i++)
        {
            AddclipBall(ballScaleList);
        }
    }
    public void AddclipBall(float[] ballScaleList)
    {
        int RandomBall = Random.Range(0, PrefabList.Length);

        GameObject columnObj = Instantiate(PrefabList[RandomBall], PutMachineClip);
        columnObj.GetComponent<Rigidbody2D>().simulated = false;
        columnObj.name = PrefabList[RandomBall].name;

        SetBallScale(columnObj, ballScaleList);

        Vector2 newPos = new Vector2()
        {
            x = PutMachineClip.position.x,
            y = PutMachineClip.position.y
        };
        columnObj.GetComponent<Transform>().position = newPos;

        for (int i = 0; i < clip.Length; i++)
        {
            if (clip[i] == null)
            {
                clip[i] = columnObj;
                break;
            }
        }
    }
    private void SetBallScale(GameObject columnObj, float[] ballScaleList)
    {
        for (int i = 0; i < PrefabList.Length; i++)
        {
            if (columnObj.name == PrefabList[i].name)
            {
                Vector2 newScale = new()
                {
                    x = columnObj.transform.localScale.x * ballScaleList[i],
                    y = columnObj.transform.localScale.y * ballScaleList[i]
                };
                columnObj.transform.localScale = newScale;
            }
        }
    }

    public void LoadeBallPutMachine()
    {
        Vector2 newPos = new Vector2()
        {
            x = TransPutMachine.position.x,
            y = TransPutMachine.position.y - 0.8f
        };
        clip[0].GetComponent<Transform>().position = newPos;
        clip[0].transform.SetParent(TransPutMachine);
    }
    public void CreateConditionBall(int value, float[] ballScaleList)
    {
        GameObject newBall = Instantiate(PrefabList[value], TransPutRange);
        newBall.transform.position = GameScore.MergeBallSameCreateBallPos;
        newBall.name = PrefabList[value].name;
        SetBallScale(newBall, ballScaleList);
    }
    public void PushOnClipBall()
    {
        clip[0].GetComponent<Transform>().SetParent(TransPutRange);
        clip[0].GetComponent<Rigidbody2D>().simulated = true;
        clip[0] = clip[1];
        clip[1] = null;
    }

    public void MoveLeft(float moveSpeed)
    { 
        TransPutMachine.position = new Vector2(TransPutMachine.position.x - moveSpeed, TransPutMachine.position.y);
    }
    public void MoveRight(float moveSpeed)
    {
        TransPutMachine.position = new Vector2(TransPutMachine.position.x + moveSpeed, TransPutMachine.position.y);
    }

    public void MoveMaxRight(float moveMax)
    {
        TransPutMachine.position = new Vector2(-moveMax, TransPutMachine.position.y);
    }
    public void MoveMaxLeft(float moveMax)
    {
        TransPutMachine.position = new Vector2(moveMax, TransPutMachine.position.y);
    }
}
