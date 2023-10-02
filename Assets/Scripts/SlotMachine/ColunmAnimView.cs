using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//ColunmAnimView
//https://www.796t.com/content/1543803251.html
public class ColunmAnimView : MonoBehaviour
{
    public Animator AnimaObj;
    public AnimationClip AnimaClip; 
    public List<Text> PerfabTextList = new List<Text>();
    public float LoopTime;
    public bool LogOff;

    public void SetRandomText(ColunmAnimModel colunmAnimModel)
    {
        for (int i = 0; i < colunmAnimModel.TextNumber; i++)
        {
            int randomIndex = Random.Range(0, colunmAnimModel.TextNumber);
            PerfabTextList[i].text = colunmAnimModel.resultRef[randomIndex].ToString();
        }
    }
 
    public void SetTextBlack(ColunmAnimModel colunmAnimModel)
    {
        for (int i = 0; i < colunmAnimModel.TextNumber; i++)
        {
            PerfabTextList[i].color = Color.black;
        }
    }

    public void SetTextRed(ColunmAnimModel colunmAnimModel)
    {
        int sevenNumber = colunmAnimModel.LuckyNumberList.Count;
        for (int i = 0; i < sevenNumber; i++)
        {
            PerfabTextList[colunmAnimModel.LuckyNumberList[i] + 1].color = Color.red;
        }
        colunmAnimModel.LuckyNumberList.Clear();
    }

    public void ResetLoopSort(ColunmAnimModel colunmAnimModel)
    {
        for (int i = 0; i < colunmAnimModel.TextNumber; i++)
        {
            if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
            else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
        }
        colunmAnimModel.ResetLoopInfo();
    }

    public void ResultAnimation(ColunmAnimModel colunmAnimModel)
    {
        AnimaObj.Play("slowAnim", 0, 0);

        for (int resultIndex = 0; resultIndex < colunmAnimModel.TextNumber; resultIndex++)
        {
            if (resultIndex == colunmAnimModel.rowNumber)
            {
                PerfabTextList[resultIndex].text = colunmAnimModel.resultList[colunmAnimModel.resultIndex].ToString();
                colunmAnimModel.resultIndex++;
            }
            else PerfabTextList[resultIndex].text = PerfabTextList[resultIndex + 1].text.ToString();
        }
    }

    public void NormalAnimation(ColunmAnimModel colunmAnimModel)
    {
        AnimaObj.Play("slowAnim", 0, 0);
        for (int animaIndex = 0; animaIndex < colunmAnimModel.TextNumber; animaIndex++)
        {
            if (animaIndex == colunmAnimModel.rowNumber) PerfabTextList[animaIndex].text = Random.Range(0, PerfabTextList.Count).ToString();
            else PerfabTextList[animaIndex].text = PerfabTextList[animaIndex + 1].text.ToString();
        }
    }
}
