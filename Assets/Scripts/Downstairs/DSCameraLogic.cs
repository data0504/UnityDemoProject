using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DSCameraLogic : MonoBehaviour
{
    public Transform ObjCamera;
    public GameObject PerfabNormalFloor;
    public GameObject PerfabShortFloor;
    public Transform ObjFloorRange;

    private bool speedLock;
    private float continuedDowmSpeed = 0.01f;
    private float addSpeed = 0.005f;

    private float floorsAllHight = 0f;
    private GameObject newfloor;
    private Transform[] floorsList = new Transform[10];

    void Start()
    {
        ContinuedSetDowm();
        ContinuedCreateFloor();
        MonitorDestoryFloor();
    }
    public void ContinuedSetDowm()
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

    public void ContinuedCreateFloor()
    {
        for (int i = 0; i < 999; i++)
        {
            if (ObjFloorRange.childCount < 10)
            {
                int Optionfloors = Random.Range(0, 2);

                if (Optionfloors == 0) CreateFloor(PerfabNormalFloor, "Normalfloor", ref floorsList);
                if (Optionfloors == 1) CreateFloor(PerfabShortFloor, "Shortfloor", ref floorsList);
            }
        }
        Invoke("ContinuedCreateFloor", Time.deltaTime);
    }
    private void CreateFloor(GameObject perfabFloor, String name, ref Transform[] floorsList)
    {
        newfloor = Instantiate(perfabFloor, ObjFloorRange);
        Vector3 newPos = new()
        {
            x = Random.Range(-8, 8),
            y = newfloor.transform.position.y - floorsAllHight,
            z = newfloor.transform.position.z,
        };
        newfloor.transform.position = newPos;
        newfloor.transform.name = name;

        float floorsSpace = Random.Range(1, 3);
        floorsAllHight += floorsSpace;

        for (int i = 0; i < floorsList.Length; i++)
        {
            if (floorsList[i] == null)
            {
                floorsList[i] = newfloor.transform;
                break;
            }
        }
    }
    public void MonitorDestoryFloor()
    {
        for (int i = 0; i < floorsList.Length; i++)
        {
            if (floorsList[i] != null && floorsList[i].position.y > ObjCamera.position.y + 5)
            { 
                Destroy(floorsList[i].gameObject);
            } 
        }
        Invoke("MonitorDestoryFloor", Time.deltaTime);
    }
}
