using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    public GameObject PerfabColumn;
    public GameObject PerfabBingoLine;
    public GameObject ObjColunmRenge;
    public Button ObjAnimSame;
    public Button ObjHandButton;
    public Button ObjAutoButton;
    public Button ObjAddButton;
    public Button ObjReduceButton;
    public Text ObjAutoText;

    public void SetAnimSameRed()
    {
        ObjAnimSame.GetComponent<Image>().color = Color.red;
    }
    public void SetAnimSameWhite()
    {
        ObjAnimSame.GetComponent<Image>().color = Color.white;
    }
    public void CleanBingoLinesWidht(List<GameObject> bingolinesList)
    {
        for (int i = 0; i < bingolinesList.Count; i++)
        {
            Vector2 initWidht = new()
            {
                x = 10,
                y = bingolinesList[i].GetComponent<RectTransform>().sizeDelta.y
            };
            bingolinesList[i].GetComponent<RectTransform>().sizeDelta = initWidht;
        }
    }

    public void SetAutoTextNull()
    {
        ObjAutoText.text = "Auto : Null";
    }
    public void SetAutoTextNumber()
    {
        ObjAutoText.text = $"Auto : {GameScore.SlotMachineSetAutoNumber}";
    }

    public void CreateColunm(int ColumnNumber, out List<GameObject> objColunm, out int rowNumber)
    {
        objColunm = new List<GameObject>();
        rowNumber = 0;
        for (int i = 0; i < ColumnNumber; i++)
        {
            GameObject columnObj = Instantiate(PerfabColumn, ObjColunmRenge.GetComponent<Transform>());
            ColunmAnimController colunm = columnObj.GetComponent<ColunmAnimController>();

            colunm.Init(ObjHandButton, ObjAutoButton);
            columnObj.name = $"column{i}";
            rowNumber = colunm.colunmAnimView.PerfabTextList.Count;
            objColunm.Add(columnObj);
        }
    }
    public void CreateBingoLine(out List<GameObject> bingolinesList)
    {
        bingolinesList = new List<GameObject>();
        for (int i = 1; i < 10; i += 2)
        {
            Vector2 newPos = new()
            {
                x = 0,
                y = -50 * i
            };

            GameObject newBingoLine = Instantiate(PerfabBingoLine, ObjColunmRenge.GetComponent<Transform>());
            newBingoLine.GetComponent<RectTransform>().anchoredPosition = newPos;
            bingolinesList.Add(newBingoLine);
        }
    }

    public void SetColunmPosition(List<GameObject> objColunm, List<Vector3> spaceColunm, int columnNumber)
    {
        for (int i = 0; i < columnNumber; i++)
        {
            objColunm[i].GetComponent<RectTransform>().anchoredPosition = spaceColunm[i];
        }
    }
    public void SetColunmRange(float windowWidth, float space)
    {
        RectTransform windowSlotRectObj = ObjColunmRenge.GetComponent<RectTransform>();
        windowSlotRectObj.sizeDelta = new Vector2(windowWidth - space, windowSlotRectObj.sizeDelta.y);
    }
}
