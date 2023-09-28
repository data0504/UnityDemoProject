using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//ColunmAnimModel

public class ColunmAnimLogic
{
    public float AnimaTime;
    public int PerfabTextNumber;
    public bool LogOff;

    public List<int> resultRef = new() { 1, 2, 3, 4, 5, 7 };
    public List<int> resultList;
    public int rowNumber;

    public bool sutureLoopOff;
    public bool autoPlayOff;

    public bool monitorOneRoundLoopTimeOff;
    public List<int> LuckyNumberList;

    public float AnimaClipLenght;
    public float keepCurrentTime;
    public const int cleanTime = 0;

    public int resultIndex;
    public const int cleanResultIndex = 0;

    public void CreateRandomResult()
    {
        resultList = null;
        resultList = new List<int>();
        for (int i = 0; i < rowNumber; i++)
        {
            int RandomIndex = Random.Range(0, PerfabTextNumber);
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
    public void MonoitorRunOne()
    {
        if (monitorOneRoundLoopTimeOff)
        {
            keepCurrentTime += Time.deltaTime;
            if (keepCurrentTime > AnimaTime)
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

    public bool CheckPlayAuto()
    {
        if (GameScore.SlotMachineSetAutoNumber > 0)
        {
            if (!GameScore.SlotMachineAutoPlay)
            {
                if (!GameScore.SlotMachineUnlockOneRound)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void InitAllInfo()
    {
        resultIndex = cleanResultIndex;
        sutureLoopOff = true;

    }
}

