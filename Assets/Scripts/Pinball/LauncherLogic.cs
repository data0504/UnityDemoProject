using System;
using UnityEngine;

[Serializable]
public class LauncherInof
{
    public Transform LauncherObjTrans;
    public float moveSpeed = 0.1f;

    public void ControlLauncher()
    {
        if (Input.GetMouseButton(0))
        {
            MoveLauncher(GetMouseXY());
        }
    }
    private float[] GetMouseXY()
    {
        float mouseX = Input.GetAxis("Mouse X") * moveSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * moveSpeed;
        float[] mouseXY = { mouseX, mouseY };
        Debug.Log($"{mouseX}, {mouseX}");
        return mouseXY;
    }
    private void MoveLauncher(float[] mouseXY)
    {
        LauncherObjTrans.localPosition -= new Vector3(0f, 0f, mouseXY[1]);
    }
}

public class LauncherLogic : MonoBehaviour
{
    public LauncherInof launcherInof = new();

    void Update()
    {
        launcherInof.ControlLauncher();
    }
}
