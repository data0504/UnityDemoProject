using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLogic : MonoBehaviour
{
    public JumpOpenGame ScenesData;

    public JumpTriangle TriangleData;

    public JumpBuffleData BuffleData;

    public JumpCamera CameraData;
    private const float maxSpace = 3f;
    private const float lowSpace = 2f;
    private const float viewAllHeight = 10.0f;

    public JumpText ViewButtonAndTextData;

    void Update()
    {
        TriangleData.LeftAndRight();
        TriangleData.SetTatgetHeight();
        TriangleData.Flying();// 特殊物件 飛翔

        BuffleData.CreateBuffle(TriangleData.GetCurrentPosY(),  viewAllHeight);
        
        BuffleData.DeleteBuffle(TriangleData.GetCurrentPosY(), viewAllHeight);//寫一個 各Buffle 偵測自身高度是否小於當前Triangle PosY


        CameraData.FollowObject(TriangleData.GetCurrentPosY(), maxSpace, lowSpace); //有Lerp 還沒寫完!! YN

        
        ViewButtonAndTextData.ViewCurrentHeight(TriangleData.GetTargetHeight());//用偵測的方式做 會比較穩定 Y
        ViewButtonAndTextData.ViewGameOver(TriangleData.GetGameOver(), TriangleData.GetTargetHeight());
    }
}
