using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public SlotModel SlotModel = new SlotModel();
    public SlotView SlotView = new SlotView();

    private void Start()
    {
        SlotView.ObjAnimSame.onClick.AddListener(SetAnimSame);
        SlotView.ObjHandButton.onClick.AddListener(PlayHand);
        SlotView.ObjAutoButton.onClick.AddListener(PlayAuto);
        SlotView.ObjAddButton.onClick.AddListener(SlotModel.AddAutoNumber);
        SlotView.ObjReduceButton.onClick.AddListener(SlotModel.ReduceAutoNumber);

        MonitorSetAutoText();
        CreateRange();
        CreateBingoLine();
    }
    private void SetAnimSame()
    {
        if (SlotModel.CheckAnimaSaneOff())
        {
            SlotModel.SetColunmLoopTimeSame();
            SlotView.SetAnimSameRed();
        }
        else
        {
            SlotModel.SetColunmLoopTimeDiffer();
            SlotView.SetAnimSameWhite();
        }
    }
    private void PlayHand()
    {
        if (SlotModel.CheckPlayHand())
        {
            SlotView.CleanBingoLinesWidht(SlotModel.bingolinesList);
        }
    }
    private void PlayAuto()
    {
        if (SlotModel.CheckPlayAuto())
        {
            SlotView.CleanBingoLinesWidht(SlotModel.bingolinesList);
        }
    }

    private void MonitorSetAutoText()
    {
        if (SlotModel.CheckAutoText())
        {
            SlotView.SetAutoTextNull();
        }
        else SlotView.SetAutoTextNumber();
        Invoke(nameof(MonitorSetAutoText), Time.deltaTime);
    }
    private void CreateRange()
    {
        SlotView.CreateColunm(SlotModel.ColumnNumber, out List<GameObject> objColunm, out int rowNumber);
        SlotModel.GetObjColunmInfo(objColunm, rowNumber);
        SlotModel.SetColunmLoopTimeDiffer();

        SlotModel.KeepColunmRange(out List<Vector3> spaceColunm);
        SlotView.SetColunmPosition(SlotModel.ObjColunm, spaceColunm, SlotModel.ColumnNumber);
        SlotView.SetColunmRange(SlotModel.WindowWidth, SlotModel.space);
    }
    private void CreateBingoLine()
    {
        SlotView.CreateBingoLine(out List<GameObject> bingolinesList);
        SlotModel.bingolinesList.AddRange(bingolinesList);
    }

    void Update()
    {
        SlotModel.MonitorHandReslutOn();

        if (SlotModel.MonitorAutoResultVeiwOn())
        {
            if (SlotModel.CheckAutoResultVeiwOff())
            {
                SlotView.CleanBingoLinesWidht(SlotModel.bingolinesList);
            }
        }
    }
}
