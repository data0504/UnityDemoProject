using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCamera : MonoBehaviour
{
    public Transform isCamera;

    private float targetHeight = 0;
    private float speed = 2.0f;

    //private float targetLowHeight = 0;

    public void Init()
    {
        targetHeight = 0; 
    }
    public float GetCameraCurrentCenterPonit()
    {
        return isCamera.position.y;
    }
    public void SetCameraCurrentCenterPonit(float Pos)
    {
        Vector3 newPos = new()
        {
            x = isCamera.position.x,
            y = Pos,
            z = isCamera.position.z
        };
        isCamera.position = newPos;
    }
    public void FollowObject(float isTrianglePosY, float maxSpace, float lowSpace)
    {
        // level_01 ALL Follow
        //float newPosY = Mathf.Lerp(isCamera.position.y, isTrianglePosY, speed * Time.deltaTime);
        //isCamera.position = new(isCamera.position.x, newPosY, isCamera.position.z);

        //level_02 height Follo
        //攝影機 看著 目標(三角形屁股 目前高度)
        float cameraPoint = isTrianglePosY - maxSpace;

        if (cameraPoint > targetHeight) { targetHeight = cameraPoint; }

        float newPosY = Mathf.Lerp(isCamera.position.y, targetHeight, speed * Time.deltaTime);

        if (targetHeight - 1 > newPosY) { newPosY = targetHeight - 1; }


        isCamera.position = new(isCamera.position.x, newPosY, isCamera.position.z);







        // 攝影機 要開始追 新目標(三角形屁股 新的高度的過程)
        /*
         * Lerp 之後再回來寫
        CheckTargetHeightAndLow(isTrianglePosY, maxSpace, lowSpace);

        float newPosY = CheckNewPosY(isTrianglePosY);

        CallBackIsCameraVector3(newPosY);
        */
    }

    /*
    private void CheckTargetHeightAndLow(float isTrianglePosY, float maxSpace, float lowSpace)
    {
        float currentMaxPoint = isTrianglePosY + maxSpace;
        if (currentMaxPoint > targetHeight) targetHeight = currentMaxPoint;

        targetLowHeight = targetHeight - lowSpace;
    }
    private float CheckNewPosY(float isTrianglePosY)
    {
        float newPosY = Mathf.Lerp(isTrianglePosY, targetHeight, Time.deltaTime);
        if (targetLowHeight > newPosY) newPosY = targetLowHeight;
        return newPosY;
    }
    private void CallBackIsCameraVector3(float newPosY)
    {
        isCamera.position = new(isCamera.position.x, newPosY, isCamera.position.z);
    }
    */
}
