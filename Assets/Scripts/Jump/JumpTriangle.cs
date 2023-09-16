using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriangle : MonoBehaviour
{
    public new  Rigidbody2D rigidbody2D;
    public Collider2D Triangle2D;

    private float targetHeight;
    private bool gameOver = false;
    private bool speedUping = false;



    public void Init()
    {
        transform.position = Vector3.zero;
        rigidbody2D.velocity = Vector2.zero;
        targetHeight = 0;
        gameOver = false;
    }

    public float GetCurrentPosY() { return transform.position.y; }
    public float GetTargetHeight() { return targetHeight; }
    public void SetTatgetHeight() { if (GetCurrentPosY() > targetHeight) targetHeight = GetCurrentPosY(); }
    public bool GetGameOver() { return gameOver; }

    public void LeftAndRight()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) gameObject.transform.position += new Vector3(-0.075f, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow)) gameObject.transform.position += new Vector3(0.075f, 0, 0);

        if (gameObject.transform.position.x < -8.3f) gameObject.transform.position += new Vector3(0.075f, 0, 0);
        if (gameObject.transform.position.x > 8.3f) gameObject.transform.position += new Vector3(-0.075f, 0, 0);
    }
    public void Flying()
    {
        if (speedUping)
        {
            Vector3 fiyingPoint = new()
            {
                x = gameObject.transform.position.x,
                y = gameObject.transform.position.y + 10 * Time.deltaTime,
                z = gameObject.transform.position.z
            };
            gameObject.transform.position = fiyingPoint;
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        SelfJumping(collision);

        MonitorGameOver(collision);

        MobitorSpeedUp(collision);

    }


    private void PrintDebug()
    {
        rigidbody2D.AddForce(new Vector2(0, 300));
    }
    private void SelfJumping(Collider2D collision)
    {
        if (rigidbody2D.velocity.y < 0)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Invoke(nameof(PrintDebug), Time.deltaTime);
                rigidbody2D.velocity = new(rigidbody2D.velocity.x, 0);
            }
        }
    }

    private void MonitorGameOver(Collider2D collision)
    {
        if (collision.transform.CompareTag("GameOver")) gameOver = true;
    }

    private IEnumerator SetFly()
    {
        rigidbody2D.simulated = false;
        Triangle2D.enabled = false;

        speedUping = true;
        yield return new WaitForSeconds(3);
        speedUping = false;

        rigidbody2D.simulated = true;
        Triangle2D.enabled = true;
    }
    private void MobitorSpeedUp(Collider2D collision)
    {
        if (collision.transform.CompareTag("SpeedUp"))
        {
            StartCoroutine(SetFly());
        }
    }


    public bool MonitorGameOver_old(float viewAllHeight, float currentCameraCenterPoint)
    {
        #region 第一版本
        /*
         
        if (GetCurrentPosY()>= targetHeight) targetHeight = GetCurrentPosY();
        float currentTargetGameOverHeight = targetHeight - viewAllHeight / 2;

        if (GetCurrentPosY() <= currentTargetGameOverHeight) return true;
        else return false;
         * 邏輯
         * 當前主角高 是否碰觸 (當前主角高 - (攝影機高度/2邊界之外))。
         * 
         * 延伸問題
         * Update Canera Follow Point 會導致 偵測當前住高度有落差
         * 
         * 例如
         * Camera Follow 當前主角高度+3 = 主角呈現 Camera中 偏低
         * 偵測點 =  當前主角高度 - 攝影機高度/2邊界(寫死)
         * 偵測點 < Current Camera Bottom Point
         * 偵測點 + 3 才會貼合Camera Bottom Point
         * 
         * * Camera Follow 當前主角高度-3 = 主角呈現 Camera中 偏高
         * 當前主角高度 - 攝影機高度/2邊界(寫死)
         * 偵測點 > Current Camera Bottom Point
         * 偵測點 -3 才會貼合Camera Bottom Point 
        
        修正邏輯
        if (GetCurrentPosY()-3 >= targetHeight) targetHeight = GetCurrentPosY()-3;
        float currentTargetGameOverHeight = targetHeight - viewAllHeight / 2;

        if (GetCurrentPosY() <= currentTargetGameOverHeight) return true;
        else return false;

        完成呈現邏輯
        float currentCameraCenterPonit = GetCurrentPosY() - 3;
        if (currentCameraCenterPonit >= targetHeight) targetHeight = currentCameraCenterPonit;
        float currentTargetGameOverBottomPonit = targetHeight - viewAllHeight / 2;

        if (GetCurrentPosY() <= currentTargetGameOverButtonPonit) return true;
        else return false;
         */
        #endregion

        #region 第二版本
        /*
        if (GetCurrentPosY() <= (currentCameraCenterPoint - viewAllHeight / 2)) return true;
        else return false;
        * 邏輯
        * 當前角色高度 <= 攝影機中心點 - 畫面高的一半
        * 代表遊戲結束

        完成呈現邏輯
        float currentTargetGameOverButtonPoint = currentCameraCenterPoint - viewAllHeight / 2;
        if (GetCurrentPosY() <= currentTargetGameOverButtonPoint) return true;
        else return false;
         
         */
        #endregion
        return false;
    }

}
