using System.Collections.Generic;

public static class GameScore 
{
    public static int Score;
    public static bool  viewText;

    public static List<List<int>> resultList = new();
    public static bool OneRound;
    public static bool OnSevenColorRed;
    public static int KeepSevenNumber;

    public static void LogDef(bool off, string printStr)
    {
        if (!off)
        {
            UnityEngine.Debug.Log(printStr);
        }
    }

    public static void SlotMachiineResult(int zere, int one, int two)
    {
        if (zere == 3) UnityEngine.Debug.Log("�T��");
        else if (one == 3) UnityEngine.Debug.Log("�R17");
        else if (two == 3) UnityEngine.Debug.Log("�@��");
        else UnityEngine.Debug.Log("�L��");
    }
    public static void SlotMachineResult1(int rowNumber, List<int> scoreboard, int columnNumber)
    {


            if (scoreboard[0] == columnNumber) UnityEngine.Debug.Log("������o : �̤j��");
        else if (scoreboard[1] == columnNumber) UnityEngine.Debug.Log("������o : �@��");
        else if(scoreboard[2] == columnNumber) UnityEngine.Debug.Log("������o : �G��");
        else if(scoreboard[3] == columnNumber) UnityEngine.Debug.Log("������o : �T��");
        else if(scoreboard[4] == columnNumber) UnityEngine.Debug.Log("������o : �|��");
        else if(scoreboard[5] == columnNumber) UnityEngine.Debug.Log("������o : ����");
            else UnityEngine.Debug.Log("������o : �L��");
        
    }
    public static void ShowResult1(bool resultOff, int rowNumber, int columnNumber)
    {
        //Lazy Initialization
        //if (slotMachineResultParser == null) slotMachineResultParser = new SlotMachineResultParser();

        //slotMachineResultParser.SlotMachineResult1(resultOff, rowNumber, columnNumber, ref resultList, ref OneRound, ref KeepSevenNumber, ref OnSevenColorRed);
    }

}
