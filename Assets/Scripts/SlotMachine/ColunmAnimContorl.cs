using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//ColunmAnimView
//https://www.796t.com/content/1543803251.html
public class ColunmAnimContorl : MonoBehaviour
{
    public Animator AnimaObj;
    public AnimationClip AnimaClip;
    public List<Text> PerfabTextList = new List<Text>();
    public float AnimaTime = 3f;
    public bool LogOff;

    public ColunmAnimLogic colunmAnimInfo;

    public void Init(Button objHandButton, Button objAutoButton)
    {
        colunmAnimInfo = new ColunmAnimLogic()
        {
            AnimaTime = AnimaTime,
            PerfabTextNumber = PerfabTextList.Count,
            LogOff = LogOff,
            AnimaClipLenght = AnimaClip.length,
            rowNumber = PerfabTextList.Count - 1
        };

        Button ObjHandButton = objHandButton;
        ObjHandButton.onClick.AddListener(PlayHand);

        Button ObjAutoButton = objAutoButton;
        ObjAutoButton.onClick.AddListener(PlayAuto);

        SetRandomText();
        MonitorLogOff();
        MonitorAnimaTime();
    }
    private void SetRandomText()
    {
        for (int i = 0; i < colunmAnimInfo.PerfabTextNumber; i++)
        {
            int randomIndex = Random.Range(0, colunmAnimInfo.PerfabTextNumber);
            PerfabTextList[i].text = colunmAnimInfo.resultRef[randomIndex].ToString();
        }
    }
    private void MonitorLogOff()
    {
        colunmAnimInfo.LogOff = LogOff;
        Invoke("MonitorLogOff", Time.deltaTime);
    }
    private void MonitorAnimaTime()
    {
        AnimaTime = colunmAnimInfo.AnimaTime;
        Invoke("MonitorAnimaTime", Time.deltaTime);
    }

    private void PlayHand()
    {
        if (!GameScore.SlotMachineUnlockOneRound)
        {
            RoundOneAnima();
        }
    }
    private void PlayAuto()
    {
        if (colunmAnimInfo.CheckPlayAuto())
        {
            RoundOneAnima();
        }
    }
    private void RoundOneAnima()
    {
        InitRoumdOneAnima();

        if (!colunmAnimInfo.sutureLoopOff) AnimaObj.Play("slowAnim", 0, 0);
        Invoke("LoopSlot", colunmAnimInfo.AnimaClipLenght);
    }

    private void InitRoumdOneAnima()
    {
        SetTextBlack();
        colunmAnimInfo.CreateRandomResult();
        colunmAnimInfo.CheckLuckyNumber(7);
        colunmAnimInfo.monitorOneRoundLoopTimeOff = true;
        GameScore.SlotMachineOnSevenColorRed = false;
    }
    private void SetTextBlack()
    {
        for (int i = 0; i < colunmAnimInfo.PerfabTextNumber; i++)
        {
            PerfabTextList[i].color = Color.black;
        }
    }


    void Update()
    {
        if (GameScore.SlotMachineAutoPlay)
        {
            RoundOneAnima();
            Invoke("OnAutoPlay", Time.deltaTime);
        }

        colunmAnimInfo.MonoitorRunOne();
        if (colunmAnimInfo.AnalyzeToTextRed()) SetTextRed();
    }
    private void OnAutoPlay()
    {
        GameScore.SetSlotMachineAutoPlay(false);
    }
    private void SetTextRed()
    {
        int sevenNumber = colunmAnimInfo.LuckyNumberList.Count;
        for (int i = 0; i < sevenNumber; i++)
        {
            PerfabTextList[colunmAnimInfo.LuckyNumberList[i] + 1].color = Color.red;
        }
        colunmAnimInfo.LuckyNumberList.Clear();
    }

    private void LoopSlot()
    {
        if (colunmAnimInfo.monitorOneRoundLoopTimeOff)
        {
            GameScore.SlotMachineUnlockOneRound = true;
            NormalAnimation();
            Invoke(nameof(LoopSlot), colunmAnimInfo.AnimaClipLenght);
        }
        else
        {
            if (colunmAnimInfo.resultIndex != colunmAnimInfo.rowNumber)
            {
                ResultAnimation();
                Invoke(nameof(LoopSlot), colunmAnimInfo.AnimaClipLenght);
            }
            else ResetLoopSort();
        }
    }

    private void ResetLoopSort()
    {
        for (int i = 0; i < colunmAnimInfo.PerfabTextNumber; i++)
        {
            if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
            else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
        }
        colunmAnimInfo.InitAllInfo();
    }
    private void ResultAnimation()
    {
        AnimaObj.Play("slowAnim", 0, 0);

        for (int resultIndex = 0; resultIndex < colunmAnimInfo.PerfabTextNumber; resultIndex++)
        {
            if (resultIndex == colunmAnimInfo.rowNumber)
            {
                PerfabTextList[resultIndex].text = colunmAnimInfo.resultList[this.colunmAnimInfo.resultIndex].ToString();
                this.colunmAnimInfo.resultIndex++;
            }
            else PerfabTextList[resultIndex].text = PerfabTextList[resultIndex + 1].text.ToString();
        }
    }
    private void NormalAnimation()
    {
        AnimaObj.Play("slowAnim", 0, 0);
        for (int animaIndex = 0; animaIndex < colunmAnimInfo.PerfabTextNumber; animaIndex++)
        {
            if (animaIndex == colunmAnimInfo.rowNumber) PerfabTextList[animaIndex].text = Random.Range(0, PerfabTextList.Count).ToString();
            else PerfabTextList[animaIndex].text = PerfabTextList[animaIndex + 1].text.ToString();
        }
    }
}
