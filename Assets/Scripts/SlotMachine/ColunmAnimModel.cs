using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//ColunmAnimModel

public class ColunmAnimModel
{
    public float LoopTime;
    public int TextNumber;
    public bool LogOff;
    public float AnimaClipLength;
    public int rowNumber;

    public List<int> resultRef = new() { 1, 2, 3, 4, 5, 7 };
    public List<int> resultList;

    public bool sutureLoopOff;

    public bool monitorOneRoundLoopTimeOff;
    public List<int> LuckyNumberList;

    public float keepCurrentTime;
    public const int cleanTime = 0;

    public int resultIndex;
    public const int cleanResultIndex = 0;

    public void Init(float LoopTime, bool LogOff, int TextNumber, float AnimaClipLength)
    {
        this.LoopTime = LoopTime;
        this.TextNumber = TextNumber;
        this.LogOff = LogOff;
        this.AnimaClipLength = AnimaClipLength;
        rowNumber = TextNumber - 1;
    }

    public bool CheckButtonPlayHandState()
    {
        if (GameScore.SlotMachineUnlockOneRound) return false;
        return true;
    }

    public bool CheckButtonPlayAutoState()
    {
        if (GameScore.SlotMachineSetAutoNumber <= 0) return false;

        else if (GameScore.SlotMachineAutoPlay) return false;

        else if (GameScore.SlotMachineUnlockOneRound) return false;

        return true;
    }

    public void CreateRandomResult()
    {
        resultList = null;
        resultList = new List<int>();
        for (int i = 0; i < rowNumber; i++)
        {
            int RandomIndex = Random.Range(0, TextNumber);
            resultList.Add(resultRef[RandomIndex]);
        }

        GameScore.SlotMachineColunmList.Add(resultList);
        GameScore.LogDef(LogOff, string.Join(",", resultList));
    }

    public void CheckLuckyNumber(int luckyNumber)
    {
        LuckyNumberList = null;
        LuckyNumberList = new List<int>();
        for (int i = 0; i < resultList.Count; i++) if (resultList[i] == luckyNumber) LuckyNumberList.Add(i);
        GameScore.LogDef(LogOff, string.Join(",", LuckyNumberList));

        if (LuckyNumberList.Count == 0) LuckyNumberList = null;
        else GameScore.SlotMachineKeepSevenNumber += LuckyNumberList.Count;
    }

    public void MonoitorRunOneOff()
    {
        if (monitorOneRoundLoopTimeOff)
        {
            keepCurrentTime += Time.deltaTime;
            if (keepCurrentTime > LoopTime)
            {
                keepCurrentTime = cleanTime;
                monitorOneRoundLoopTimeOff = false;
            }
        }
    }

    public bool AnalyzeToTextRed()
    {
        return LuckyNumberList != null && GameScore.SlotMachineOnSevenColorRed;
    }

    public void ResetLoopInfo()
    {
        resultIndex = cleanResultIndex;
        sutureLoopOff = true;
    }
}
