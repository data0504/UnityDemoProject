using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuffleData : MonoBehaviour
{
    public GameObject topTriangle;
    public GameObject baffleGroup;
    public GameObject buffle_1;
    public GameObject buffle_2;
    public GameObject buffle_3;

    public GameObject buffleSpeedUp;

    public JumpSetBuffle jumpSetBuffle;
    public JumpSetSpeedUp JumpSetSpeedUp;


    private GameObject isBuffle;
    private GameObject isSpeedUp;
    private GameObject isObj;

    private const float spaceHeight = 1.5f;

    private float keepHeight = 0f;
    private List<GameObject> buffleObjects = new();
    private List<GameObject> buffleSpeedUps = new();


    private const int limitBuffle = 10;
    private const int deleteNumber = 5;

    private float targetHeight = 0;


    internal void Init()
    {
        for (int i = 0; i < baffleGroup.transform.childCount; i++)
        {
            Destroy(baffleGroup.transform.GetChild(i).gameObject);
        }
        keepHeight = 0f;
    }

    public void CreateBuffle(float isTrianglePosY,  float viewAllHeight)
    {
        for (int i = 0; i < 999; i++)
        {
            int isNumber = (int)(isTrianglePosY + viewAllHeight - keepHeight);
            if (0 > isNumber)
            {
                targetHeight = (int)(isTrianglePosY );
                return;
            }
                
            if (keepHeight <= 50)
            { 
                Level1();
                break;
            }
            else if (keepHeight <= 100) 
            {
                Level2();
                break;

            }
            else if (keepHeight <= 150) 
            {
                Level3();
                break;
            }
        }

    }


    private void Level1()
    {
        isBuffle = Instantiate(buffle_1, baffleGroup.transform);
        Vector3 isVector3 = new()
        {
            x = Random.Range(-8, 8),
            y = isBuffle.transform.position.y + keepHeight,
            z = isBuffle.transform.position.z,
        };
        isBuffle.transform.position = isVector3;
        isBuffle.GetComponent<JumpSetBuffle>().SetInitHeight(isBuffle.transform.position.y);
        buffleObjects.Add(isBuffle);

        if (Random.Range(0, 4) == 1)
        {
            isSpeedUp = Instantiate(buffleSpeedUp, baffleGroup.transform);
            isVector3 = new()
            {
                x = isBuffle.transform.position.x,
                y = isSpeedUp.transform.position.y + keepHeight,
                z = isSpeedUp.transform.position.z
            };
            isSpeedUp.transform.position = isVector3;
            isSpeedUp.GetComponent<JumpSetSpeedUp>().SetInitHeight(isSpeedUp.transform.position.y);
            buffleSpeedUps.Add(isSpeedUp);

        }



        keepHeight += spaceHeight;
    }
    private void Level2()
    {
        isBuffle = Instantiate(buffle_2, baffleGroup.transform);
        Vector3 isVector3 = new()
        {
            x = Random.Range(-8, 8),
            y = isBuffle.transform.position.y + keepHeight,
            z = isBuffle.transform.position.z,
        };
        isBuffle.transform.position = isVector3;

        keepHeight += spaceHeight;
    }
    private void Level3()
    {
        isBuffle = Instantiate(buffle_3, baffleGroup.transform);
        Vector3 isVector3 = new()
        {
            x = Random.Range(-8, 8),
            y = isBuffle.transform.position.y + keepHeight,
            z = isBuffle.transform.position.z,
        };
        isBuffle.transform.position = isVector3;

        keepHeight += spaceHeight;
    }



    public void DeleteBuffle(float isTrianglePosY, float viewAllHeight)
    {
        for (int i = buffleObjects.Count - 1; i >= 0; i--)
        {
            if (buffleObjects[i] == null) break;
            isObj = buffleObjects[i];
            isObj.GetComponent<JumpSetBuffle>().SetCurrentHeight(isTrianglePosY);
        }

        for (int i = 0; i < buffleSpeedUps.Count; i++)
        {
            if (buffleSpeedUps[i] == null) continue;
            isObj = buffleSpeedUps[i];
            isObj.GetComponent<JumpSetSpeedUp>().SetCurrentHeight(isTrianglePosY);
        }






        //子物件 超出指定數量 刪除
        /*
        if (baffleGroup.transform.childCount > limitBuffle)
        {
            for (int i = 0; i < deleteNumber; i++) Destroy(baffleGroup.transform.GetChild(i).gameObject);
        }
         */

        // 利用Create 的方式刪除
        /*
        int isNumber = (int)(isTrianglePosY + (viewAllHeight) - keepHeight);
        if (0 > isNumber && baffleGroup.transform.childCount > 10)
        {
            Destroy(baffleGroup.transform.GetChild(0).gameObject);
        }
         */

        // 再簡化
        /*
        if (baffleGroup.transform.childCount > 10)
        {
            Destroy(baffleGroup.transform.GetChild(0).gameObject);
        }
        */













        // 利用預制物件獨立偵測
    }
}
