using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotModel : MonoBehaviour
{
    public float OneRoundTime = 15f;
    public int ColumnNumber;

    public int WindowWidth;
    public float currentPosX;
    public float space;
    public int RowNumber;

    public float cleanTime = 0;
    public float currentTime;
    public bool HandReslutOn;
    public bool AutoResultVeiwOn;

    public bool animaSameOff;
    public List<GameObject> ObjColunm = new List<GameObject>();
    public List<GameObject> bingolinesList = new List<GameObject>();

    public bool CheckAnimaSaneOff()
    {
        if (!animaSameOff)
        {
            animaSameOff = true;
            return true;
        }
        animaSameOff = false;
        return false;
    }
    public bool CheckPlayHand()
    {
        if (!HandReslutOn)
        {
            HandReslutOn = true;
            return true;
        }
        return false;
    }
    public bool CheckPlayAuto()
    {
        if (GameScore.SlotMachineSetAutoNumber != 0 && !AutoResultVeiwOn)
        {
            AutoResultVeiwOn = true;
            return true;
        }
        return false;
    }

    public void AddAutoNumber()
    {
        GameScore.SlotMachineSetAutoNumber++;
    }
    public void ReduceAutoNumber()
    {
        GameScore.SlotMachineSetAutoNumber--;
    }

    public void SetColunmLoopTimeSame()
    {
        float timeNumber = 3f;
        for (int i = 0; i < ColumnNumber; i++)
        {
            ColunmAnimController colunm = ObjColunm[i].GetComponent<ColunmAnimController>();
            colunm.colunmAnimModel.LoopTime = timeNumber;
        }
    }
    public void SetColunmLoopTimeDiffer()
    {
        float timeNumber = 3f;
        for (int i = 0; i < ColumnNumber; i++)
        {
            ColunmAnimController colunm = ObjColunm[i].GetComponent<ColunmAnimController>();
            colunm.colunmAnimModel.LoopTime = timeNumber;
            timeNumber += 2f;
        }
    }
    public bool CheckAutoText()
    {
        if (GameScore.SlotMachineSetAutoNumber <= 0)
        {
            GameScore.SlotMachineSetAutoNumber = 0;
            return true;
        }
        return false;
    }
    public void GetObjColunmInfo(List<GameObject> objColunm, int rowNumber)
    {
        ObjColunm.AddRange(objColunm);
        RowNumber = rowNumber;
    }
    public void KeepColunmRange(out List<Vector3> spaceColunm)
    {
        WindowWidth = ColumnNumber * 150;
        currentPosX = -(WindowWidth / 2);
        space = WindowWidth / (ColumnNumber + 1);

        spaceColunm = new List<Vector3>();
        for (int i = 0; i < ColumnNumber; i++)
        {
            currentPosX += space;
            Vector3 currentPos = new()
            {
                x = currentPosX,
                y = 0,
                z = 0
            };
            spaceColunm.Add(currentPos);
        }
    }
  

    public void MonitorHandReslutOn()
    {
        if (HandReslutOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= OneRoundTime)
            {
                //GameScore.ShowResult1(resultVeiwOn, rowNumber, ColumnNumber);
                AnalyzeOneRowResult();
                InitGameScoreSlotMachineValue();

                currentTime = cleanTime;
                HandReslutOn = false;
            }
        }
    }
    public bool MonitorAutoResultVeiwOn()
    {
        if (AutoResultVeiwOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= OneRoundTime)
            {
                AnalyzeOneRowResult();

                PlayingAutoNumberZeroStop();

                InitGameScoreSlotMachineValue();

                currentTime = cleanTime;
                return true;
            }
        }
        return false;
    }

    private void AnalyzeOneRowResult()
    {
        for (int i = 0; i < RowNumber - 1; i++)
        {
            SortRowNumber(i, out List<int> resultRowList);
            AnalyzeResultList(resultRowList, out List<int> scoreList);

            SetScaleBingoLines(i, scoreList);
            GameScore.SlotMachineShowResult(scoreList, ColumnNumber);

            resultRowList.Clear();
            scoreList.Clear();
        }
    }
    public void SortRowNumber(int indexRow, out List<int> resultRowLists)
    {
        resultRowLists = new();
        for (int i = 0; i < GameScore.SlotMachineColunmList.Count; i++)
        {
            int colunmIndex = GameScore.SlotMachineColunmList[i][indexRow];
            resultRowLists.Add(colunmIndex);
        }
    }
    public void AnalyzeResultList(List<int> resultRowList, out List<int> scoreList)
    {
        scoreList = new() { 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < resultRowList.Count; i++)
        {
            if (resultRowList[i] == 7) scoreList[0]++;
            else if (resultRowList[i] == 1) scoreList[1]++;
            else if (resultRowList[i] == 2) scoreList[2]++;
            else if (resultRowList[i] == 3) scoreList[3]++;
            else if (resultRowList[i] == 4) scoreList[4]++;
            else if (resultRowList[i] == 5) scoreList[5]++;
        }
    }
    public void SetScaleBingoLines(int indexRow, List<int> scoreList)
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            if (scoreList[i] == ColumnNumber)
            {
                Vector2 newPos = new()
                {
                    x = WindowWidth - space,
                    y = bingolinesList[indexRow].GetComponent<RectTransform>().sizeDelta.y
                };
                bingolinesList[indexRow].GetComponent<RectTransform>().sizeDelta = newPos;
            }
        }
    }
    public void InitGameScoreSlotMachineValue()
    {
        GameScore.SlotMachineColunmList.Clear();
        GameScore.SlotMachineUnlockOneRound = false;

        if (GameScore.SlotMachineKeepSevenNumber >= 3) GameScore.SlotMachineOnSevenColorRed = true;
        GameScore.SlotMachineKeepSevenNumber = 0;
    }


    public void PlayingAutoNumberZeroStop()
    {
        ReduceAutoNumber();
        if (GameScore.SlotMachineSetAutoNumber == 0) AutoResultVeiwOn = false;
    }
    public bool CheckAutoResultVeiwOff()
    {
        if (AutoResultVeiwOn)
        {
            GameScore.SetSlotMachineAutoPlay(true);
            return true;
        }
        return false;
    }

    #region Pending Method
    private void PlayingAutoNumberZeroStopOld()
    {
        ReduceAutoNumber();
        if (GameScore.SlotMachineSetAutoNumber == 0) AutoResultVeiwOn = false;
        Invoke(nameof(RunAuto), 1.5f);
    }
    private void PlayingAutoPassStop()
    {
        for (int i = 0; i < bingolinesList.Count; i++)
        {
            if (bingolinesList[i].GetComponent<RectTransform>().sizeDelta.x > 10)
            {
                AutoResultVeiwOn = false;
                break;
            }
            Invoke(nameof(RunAuto), 2f);
        }
    }
    private void RunAuto()
    {
        if (AutoResultVeiwOn)
        {
            GameScore.SetSlotMachineAutoPlay(true);
            //SlotView.CleanBingoLinesWidht(bingolinesList);
        }
    }
    #endregion
}