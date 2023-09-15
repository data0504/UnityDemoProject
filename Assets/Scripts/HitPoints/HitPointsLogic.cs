using System;
using UnityEngine;

public class HpSetInfo
{
    public float hpHurt;
    public float hpRecovery;
    public bool recoverOff;
}

public class HpObjectInfo
{
    public GameObject Object;
    public RectTransform RectTansform;
}

[Serializable]
public class HitPointsInfo
{ 
    public RectTransform importObj;
    public GameObject HPBarFrame;
    public GameObject HPBar;
    public GameObject Cover;
    public GameObject Hurt;
    public GameObject LifePoints;

    public float DynamicSleep = 0.05f;
    public float HurtPCT = 0.1f;
    public float RecoverytPCT = 0.01f;

    private HpObjectInfo hurtInfo;
    private HpObjectInfo lifePointsInfo;
    private HpObjectInfo coverInfo;
    private HpSetInfo hpSetInfo = new();

    public void Init()
    {
        CreatePerfab();
        SetHurtValue();
    }
    private void CreatePerfab()
    {
        GameObject.Instantiate(HPBarFrame, importObj);
        GameObject.Instantiate(HPBar, importObj);

        GameObject cover = GameObject.Instantiate(Cover, importObj);
        coverInfo = new()
        {
            Object = cover,
            RectTansform = cover.GetComponent<RectTransform>()
        };

        GameObject hurt = GameObject.Instantiate(Hurt, importObj);
        hurtInfo = new()
        {
            Object = hurt,
            RectTansform = hurt.GetComponent<RectTransform>()
        };

        GameObject lifePoints = GameObject.Instantiate(LifePoints, importObj);
        lifePointsInfo = new()
        {
            Object = lifePoints,
            RectTansform = lifePoints.GetComponent<RectTransform>()
        };
    }
    private void SetHurtValue()
    {
        hpSetInfo.hpHurt = Mathf.Lerp(0, lifePointsInfo.RectTansform.sizeDelta.x, HurtPCT);
        hpSetInfo.hpRecovery = hpSetInfo.hpHurt * RecoverytPCT;
    }

    public void Monitor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReduceHpFast(lifePointsInfo.RectTansform, hpSetInfo.hpHurt);
            ReduceHpFast(coverInfo.RectTansform, hpSetInfo.hpRecovery);
        }
        if (Input.GetMouseButtonDown(2))
        {
            hpSetInfo.recoverOff = true;
        }

        ReduceHurtSlow(hurtInfo.RectTansform, lifePointsInfo.RectTansform);
        AddRecoverySlow(coverInfo.RectTansform, hurtInfo.RectTansform, lifePointsInfo.RectTansform);
    }
    private void ReduceHpFast(RectTransform hpLenght, float hurtValue)
    {
        Vector2 newHpLenght = new()
        {
            x = hpLenght.sizeDelta.x - hurtValue,
            y = hpLenght.sizeDelta.y
        };
        hpLenght.sizeDelta = newHpLenght;
    }
    private void ReduceHurtSlow(RectTransform hurt, RectTransform lifePoints)
    {
        if (hurt.sizeDelta.x != lifePoints.sizeDelta.x)
        {
            CountHpBetweenLenght(hurt, lifePoints);
        }
    }
    private void AddRecoverySlow(RectTransform recovery, RectTransform hurt, RectTransform lifePoints)
    {
        if (hpSetInfo.recoverOff)
        {
            bool correct = hurt.sizeDelta.x < recovery.sizeDelta.x && lifePoints.sizeDelta.x < recovery.sizeDelta.x;
            if (correct)
            {
                CountHpBetweenLenght(hurt, recovery);
                CountHpBetweenLenght(lifePoints, recovery);
            }
            else hpSetInfo.recoverOff = false;
        }
    }
    private void CountHpBetweenLenght(RectTransform hpLenghtA, RectTransform hpLenghtB)
    {
        Vector2 newHpLenght = new()
        {
            x = Mathf.Lerp(hpLenghtA.sizeDelta.x, hpLenghtB.sizeDelta.x, DynamicSleep),
            y = hpLenghtA.sizeDelta.y
        };
        hpLenghtA.sizeDelta = newHpLenght;
    }
}

public class HitPointsLogic : MonoBehaviour
{
    public HitPointsInfo hitPointsInfo;
    void Start()
    {
        hitPointsInfo.Init();
    }
    void Update()
    {
        hitPointsInfo.Monitor();
    }
}
