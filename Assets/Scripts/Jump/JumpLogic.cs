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
        TriangleData.Flying();// �S���� ����

        BuffleData.CreateBuffle(TriangleData.GetCurrentPosY(),  viewAllHeight);
        
        BuffleData.DeleteBuffle(TriangleData.GetCurrentPosY(), viewAllHeight);//�g�@�� �UBuffle �����ۨ����׬O�_�p���eTriangle PosY


        CameraData.FollowObject(TriangleData.GetCurrentPosY(), maxSpace, lowSpace); //��Lerp �٨S�g��!! YN

        
        ViewButtonAndTextData.ViewCurrentHeight(TriangleData.GetTargetHeight());//�ΰ������覡�� �|���í�w Y
        ViewButtonAndTextData.ViewGameOver(TriangleData.GetGameOver(), TriangleData.GetTargetHeight());
    }
}
