using Assets.scripts.AnimImg1;
using System.Collections.Generic;

public static class GameScore 
{
    public static int Score;
    public static bool  viewText;

    public static List<List<int>> SlotMachineColunmList = new();
    public static bool SlotMachineUnlockOneRound;
    public static bool SlotMachineOnSevenColorRed;
    public static int SlotMachineKeepSevenNumber;

    public static int SlotMachineSetAutoNumber;
    public static bool SlotMachineAutoPlay;
    public static ResultParser slotMachineResultParser;

    public static void LogDef(bool off, string printStr)
    {
        if (!off)
        {
            UnityEngine.Debug.Log(printStr);
        }
    }
    public static void SlotMachiineResult(int zere, int one, int two)
    {
        if (zere == 3) UnityEngine.Debug.Log("三獎");
        else if (one == 3) UnityEngine.Debug.Log("愛17");
        else if (two == 3) UnityEngine.Debug.Log("一獎");
        else UnityEngine.Debug.Log("無獎");
    }
    public static void SlotMachineShowResult(List<int> scoreboard, int columnNumber)
    {
        if (scoreboard[0] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 最大獎");
        else if (scoreboard[1] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 一獎");
        else if (scoreboard[2] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 二獎");
        else if (scoreboard[3] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 三獎");
        else if (scoreboard[4] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 四獎");
        else if (scoreboard[5] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 五獎");
        else UnityEngine.Debug.Log("恭喜獲得 : 無獎");
    }

    public static void SetSlotMachineAutoPlay(bool off)
    {
        SlotMachineAutoPlay = off;
    }
    public static void ShowResult1(bool resultOff, int rowNumber, int columnNumber)
    {
        //Lazy Initialization
        //if (slotMachineResultParser == null) slotMachineResultParser = new SlotMachineResultParser();

        //slotMachineResultParser.SlotMachineResult1(resultOff, rowNumber, columnNumber, ref resultList, ref OneRound, ref KeepSevenNumber, ref OnSevenColorRed);
    }
}
