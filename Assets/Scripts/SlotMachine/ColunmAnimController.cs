using UnityEngine;
using UnityEngine.UI;

public class ColunmAnimController : MonoBehaviour
{
    public ColunmAnimModel colunmAnimModel = new ColunmAnimModel();
    public ColunmAnimView colunmAnimView = new ColunmAnimView();

    public void Init(Button objHandButton, Button objAutoButton)
    {
        colunmAnimModel.Init(colunmAnimView.LoopTime, colunmAnimView.LogOff, colunmAnimView.PerfabTextList.Count, colunmAnimView.AnimaClip.length);
        colunmAnimView.SetRandomText(colunmAnimModel);
        MonitorLogOff();
        MonitorAnimaTime();

        objHandButton.onClick.AddListener(PlayHand);
        objAutoButton.onClick.AddListener(PlayAuto);
    }
    public void MonitorLogOff()
    {
        colunmAnimModel.LogOff = colunmAnimView.LogOff;
        Invoke("MonitorLogOff", Time.deltaTime);
    }
    public void MonitorAnimaTime()
    {
        colunmAnimView.LoopTime = colunmAnimModel.LoopTime;
        Invoke("MonitorAnimaTime", Time.deltaTime);
    }

    public void PlayHand()
    {
        if (colunmAnimModel.CheckButtonPlayHandState())
        {
            RoundOneAnima();
        }
    }
    public void PlayAuto()
    {
        if (colunmAnimModel.CheckButtonPlayAutoState())
        {
            RoundOneAnima();
        }
    }
    public void RoundOneAnima()
    {
        InitRoumdOneAnima();

        if (!colunmAnimModel.sutureLoopOff) colunmAnimView.AnimaObj.Play("slowAnim", 0, 0);

        Invoke("LoopSlot", colunmAnimModel.AnimaClipLength);
    }
    public void InitRoumdOneAnima()
    {
        colunmAnimView.SetTextBlack(colunmAnimModel);
        colunmAnimModel.CreateRandomResult();
        colunmAnimModel.CheckLuckyNumber(7);
        colunmAnimModel.monitorOneRoundLoopTimeOff = true;
        GameScore.SlotMachineOnSevenColorRed = false;
    }
    void Update()
    {
        if (GameScore.SlotMachineAutoPlay)
        {
            RoundOneAnima();
            Invoke(nameof(OnAutoPlay), 0.001f);
        }

        colunmAnimModel.MonoitorRunOneOff();
    }
    public void OnAutoPlay()
    {
        GameScore.SetSlotMachineAutoPlay(false);
    }
    public void LoopSlot()
    {
        if (colunmAnimModel.monitorOneRoundLoopTimeOff)
        {
            GameScore.SlotMachineUnlockOneRound = true;
            colunmAnimView.NormalAnimation(colunmAnimModel);
            Invoke(nameof(LoopSlot), colunmAnimModel.AnimaClipLength);
        }
        else
        {
            if (colunmAnimModel.resultIndex != colunmAnimModel.rowNumber)
            {
                colunmAnimView.ResultAnimation(colunmAnimModel);
                Invoke(nameof(LoopSlot), colunmAnimModel.AnimaClipLength);
            }
            else
            {
                if (colunmAnimModel.AnalyzeToTextRed()) colunmAnimView.SetTextRed(colunmAnimModel);
                colunmAnimView.ResetLoopSort(colunmAnimModel);
            }
        }
    }
}
