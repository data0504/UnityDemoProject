using UnityEngine;

public class ExchangeCupModel
{
    private int[] circle = { 1, 0, 0 };
    public int[] CircleList
    {
        get { return circle; }
        set { CircleList = value; }
    }

    private string[] animClipHeadTailName = new string[2];
    public string[] AnimClipHeadTailNameList
    {
        get { return animClipHeadTailName; }
        set { animClipHeadTailName = value; }
    }
    private float[] animClipHeadTailTime = new float[2];
    public float[] AnimClipHeadTailTimeList
    {
        get { return animClipHeadTailTime; }
        set { animClipHeadTailTime = value; }
    }

    private string[] animClipProcessName = new string[3];
    public string[] AnimClipProcessNameList
    {
        get { return animClipProcessName; }
        set { animClipProcessName = value; }
    }

    private float[] animClipProcessTime = new float[3];
    public float[] AnimClipProcessTimeList
    {
        get { return animClipProcessTime; }
        set { animClipProcessTime = value; }
    }

    private bool isPlaying;
    private bool isfirstPlaying;
    private int[] AnimPlayingIndexList = new int[10];
    private float AllProecssAnimTime;

    public void SetAnimClipHeadTailNameList(string str0, string str1)
    {
        AnimClipHeadTailNameList[0] = str0;
        AnimClipHeadTailNameList[1] = str1;
    }
    public void SetAnimClipHeadTailTimeList(float str0, float str1)
    {
        AnimClipHeadTailTimeList[0] = str0;
        AnimClipHeadTailTimeList[1] = str1;
    }
    public void SetAnimClipProcessNameList(string str0, string str1, string str2)
    {
        AnimClipProcessNameList[0] = str0;
        AnimClipProcessNameList[1] = str1;
        AnimClipProcessNameList[2] = str2;
    }
    public void SetAnimClipProcessTimeList(float str0, float str1, float str2)
    {
        AnimClipProcessTimeList[0] = str0;
        AnimClipProcessTimeList[1] = str1;
        AnimClipProcessTimeList[2] = str2;
    }

    public bool SetCircleActive(bool isActive)
    {
        if (isActive == true) return false;
        else return true;
    }
    public (string[], float[], int[]) GetAnimPlayLists()
    {
        for (int i = 0; i < AnimPlayingIndexList.Length; i++)
        {
            int randomNumberIndex = Random.Range(0, animClipProcessName.Length);
            AnimPlayingIndexList[i] = randomNumberIndex;

            if (randomNumberIndex == 0)
            {
                int prefabNumer = circle[0];
                circle[0] = circle[1];
                circle[1] = prefabNumer;
            }
            if (randomNumberIndex == 1)
            {
                int prefabNumer = circle[1];
                circle[1] = circle[2];
                circle[2] = prefabNumer;
            }
            if (randomNumberIndex == 2)
            {
                int prefabNumer = circle[2];
                circle[2] = circle[1];
                circle[1] = circle[0];
                circle[0] = prefabNumer;
            }
        }
        return (AnimClipProcessNameList, AnimClipProcessTimeList, AnimPlayingIndexList);
    }
    public float GetAllProecssAnimTime()
    {
        for (int i = 0; i < AnimPlayingIndexList.Length; i++)
        {
            AllProecssAnimTime += AnimClipProcessTimeList[AnimPlayingIndexList[i]];
        }
        return AllProecssAnimTime;
    }
    public void SetInitAllProecssAnimTime()
    {
        AllProecssAnimTime = 0;
    }

    public Vector3 CheckCircleListOne()
    {
        if (circle[0] == 1) return new Vector3(-5f, 0, 0.1f);
        if (circle[1] == 1) return new Vector3(0f, 0, 0.1f);
        if (circle[2] == 1) return new Vector3(5f, 0, 0.1f);
        return  new Vector3(7f, 0, 0.1f);
    }

    public bool CheckFirstPlaying()
    {
        if (!isPlaying && !isfirstPlaying)
        {
            isfirstPlaying = true;
            return true;
        }
        return false;
    }
    public bool CheckPlaying()
    {
        if (!isPlaying) return true;
        return false;
    }
    public void SetPlayState()
    {
        if (isPlaying) isPlaying = false;
        else isPlaying = true;
    }
}
