using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PositionAnalyze
{
    public Vector2 WorldPos;
    public Vector2 WorldSize;

    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

    public void CountPosition (RectTransform ObjRectTransform)
    {

        Vector2 WorldPos = ObjRectTransform.TransformPoint(ObjRectTransform.rect.center);
        Vector2 WorldSize = ObjRectTransform.TransformVector(ObjRectTransform.rect.size);

        Left = WorldPos.x - WorldSize.x / 2;
        Right = WorldPos.x + WorldSize.x / 2;
        Top = WorldPos.y + WorldSize.y / 2;
        Bottom = WorldPos.y - WorldSize.y / 2;
    }
}

public class ObjInfo
{
    public GameObject Object;
    public RectTransform RecTransform;
}

[Serializable]
public class TargetPracticeInfo
{ 
    public GameObject BulletPrefab;
    public GameObject BullseyePrefab;
    public RectTransform TargetRangeRectTansform;

    public int MinObjsNumber = 1;
    public int MaxObjsNumber = 7;
    public float MaxColunmPosition = 490;

    private ObjInfo newObjInfo;
    private List<ObjInfo> objBullerList;
    private List<ObjInfo> objBullseyeList;

    private PositionAnalyze positionAnalyzeBuller;
    private PositionAnalyze positionAnalyzeBullseys;


    public void CreatePrefab()
    {
        RandomBulletObjs(BulletPrefab,  TargetRangeRectTansform);
        RandomBullseyeObjs(BullseyePrefab, TargetRangeRectTansform);
    }
    private void RandomBulletObjs(GameObject prefab, RectTransform importObj)
    {
        int RandomObjsNumber = Random.Range(MinObjsNumber, MaxObjsNumber);

        objBullerList = new();  
        for (int i = 0; i <= RandomObjsNumber; i++)
        {
            GameObject newBulletPrefab = GameObject.Instantiate(prefab, importObj);
            Vector2 presetPosition = new()
            {
                x = -importObj.anchoredPosition.x,
                y = Random.Range(-MaxColunmPosition, MaxColunmPosition)
            };
            newBulletPrefab.GetComponent<RectTransform>().anchoredPosition = presetPosition;

            newObjInfo = new()
            {
                Object = newBulletPrefab,
                RecTransform = newBulletPrefab.GetComponent<RectTransform>()
            };
            objBullerList.Add(newObjInfo);
        }
    }
    private void RandomBullseyeObjs(GameObject prefab, RectTransform importObj)
    {
        int RandomObjsNumber = Random.Range(MinObjsNumber, MaxObjsNumber);

        objBullseyeList = new();        
        for (int i = 0; i <= RandomObjsNumber; i++)
        {
            GameObject newBullseyePrefab = GameObject.Instantiate(prefab, importObj);
            Vector2 presetPosition = new()
            {
                x = importObj.anchoredPosition.x,
                y = Random.Range(-MaxColunmPosition, MaxColunmPosition)
            };
            newBullseyePrefab.GetComponent<RectTransform>().anchoredPosition = presetPosition;

            newObjInfo = new()
            {
                Object = newBullseyePrefab,
                RecTransform = newBullseyePrefab.GetComponent<RectTransform>()
            };
            objBullseyeList.Add(newObjInfo);
        }
    }

    public void Comparison(out List<ObjInfo> overlapObjList)
    {
        overlapObjList = new();
        for (int i = objBullseyeList.Count - 1; i >= 0; i--)
        {
            for (int j = objBullerList.Count - 1; j >= 0; j--)
            {
                positionAnalyzeBuller = new();
                positionAnalyzeBuller.CountPosition(objBullerList[j].RecTransform);

                positionAnalyzeBullseys = new();
                positionAnalyzeBullseys.CountPosition(objBullseyeList[i].RecTransform);

                if (positionAnalyzeBuller.Right >= positionAnalyzeBullseys.Left)
                {
                    if (positionAnalyzeBuller.Top <= positionAnalyzeBullseys.Top && positionAnalyzeBuller.Top >= positionAnalyzeBullseys.Bottom ||
                        positionAnalyzeBuller.Bottom <= positionAnalyzeBullseys.Top && positionAnalyzeBuller.Top >= positionAnalyzeBullseys.Bottom)
                    {
                        overlapObjList.Add(objBullerList[j]);
                        overlapObjList.Add(objBullseyeList[i]);
                    }
                }
            }
        }
    }

    private void DeleteObjList(List<ObjInfo> overlapObjList)
    {
        foreach (var objInfo in overlapObjList)
        {
            GameObject.Destroy(objInfo.Object);
            objBullerList.Remove(objInfo);
            objBullseyeList.Remove(objInfo);
        }
    }

    public void MonitorOverlap()
    {
        Comparison(out List<ObjInfo> overlapObjList);
        DeleteObjList(overlapObjList);
    }
}

public class TargetRangeLogic : MonoBehaviour
{
    public TargetPracticeInfo targetPracticeInfo = new();
    void Start()
    {
        targetPracticeInfo.CreatePrefab();
    }
    void Update()
    {
        targetPracticeInfo.MonitorOverlap();
    }
}
