using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DSCameraLogic : MonoBehaviour
{
    public Transform ObjCamera;
    public GameObject PerfabFloor;
    public Transform ObjFloorRange;

    private bool speedLock;
    private float continuedDowmSpeed = 0.01f;
    private float addSpeed = 0.005f;

    private float floorsAllHight = 0f;
    private List<GameObject> floorsList = new List<GameObject>();

    void Start()
    {
        ContinuedSetDowm();
        ContinuedCreateFloor();
        MonitorDestoryFloor();
    }
    private void ContinuedSetDowm()
    {
        if (!speedLock)
        {
            Invoke(nameof(DelayMethod), 5f);
            speedLock = true;
        }
        else
        {
            SetDowm(0f);
        }
        Invoke("ContinuedSetDowm", Time.deltaTime);
    }
    private void DelayMethod()
    {
        SetDowm(addSpeed);
        speedLock = false;
    }
    private void SetDowm(float value)
    {
        continuedDowmSpeed += value;
        Vector3 newCameraPos = new Vector3()
        {
            x = ObjCamera.position.x,
            y = ObjCamera.position.y - continuedDowmSpeed
        };
        ObjCamera.position = newCameraPos;
    }

    private void ContinuedCreateFloor()
    {
        for (int i = 0; i < 999; i++)
        {
            if (floorsList.Count < 10)
            {
                GameObject floor = Instantiate(PerfabFloor, ObjFloorRange);
                Vector3 newPos = new()
                {
                    x = Random.Range(-8, 8),
                    y = floor.transform.position.y - floorsAllHight,
                    z = floor.transform.position.z,
                };
                floor.transform.position = newPos;
                floor.transform.name = "floor";

                float floorsSpace = Random.Range(1, 3);
                floorsAllHight += floorsSpace;

                floorsList.Add(floor);
            }
        }
        Invoke("ContinuedCreateFloor", Time.deltaTime);
    }

    private void MonitorDestoryFloor()
    {
        for (int i = 0; i < floorsList.Count; i++)
        {
            if (floorsList[i].GetComponent<Transform>().position.y > ObjCamera.position.y + 5)
            {
                Destroy(floorsList[i]);
                floorsList.Remove(floorsList[i]);
            }
        }
        Invoke("MonitorDestoryFloor", Time.deltaTime);
    }


}
