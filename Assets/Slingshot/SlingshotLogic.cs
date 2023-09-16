using System;
using UnityEngine;

[Serializable]
public class MouseControlInfo
{
    private Rigidbody SelfRigid;
    private Transform SelfTrans;
    public  float AttackSpeed = 1000.0f;
    public  float RotationSpeed = 5.0f;

    public void Init(GameObject gameObject)
    {
        SelfTrans = gameObject.GetComponent<Transform >();
        SelfRigid = gameObject.GetComponent<Rigidbody>();
        KinematicOff();
    }
    public void FollowMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Down");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Hold on");
            RotaObj();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Up");
            RigiObj();
        }
    }
    public void RotaObj()
    {
        RotateObject(GetMouseXY());
    }
    public void RigiObj()
    {
        KinematicOn();
        ObjectAddForce();
    }
    private void KinematicOff()
    {
        SelfRigid.isKinematic = true;
    }
    private void KinematicOn()
    {
        SelfRigid.isKinematic = false;
    }
    public float[] GetMouseXY()
    {
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed;
        float[] mouseXY = { mouseX, mouseY };
        return mouseXY;
    }
    private void RotateObject(float[] mouseXY)
    {
        SelfTrans.rotation *= Quaternion.Euler(0, mouseXY[0] * -1, 0);
        SelfTrans.rotation *= Quaternion.Euler(mouseXY[1], 0, 0);
    }
    public void ObjectAddForce()
    {
        SelfRigid.AddForce(SelfTrans.forward * AttackSpeed);
    }
}

public class SlingshotLogic : MonoBehaviour
{
    public MouseControlInfo mouseControlInfo  = new();

    void Start()
    {
        mouseControlInfo.Init(gameObject);
    }

    void Update()
    {
        mouseControlInfo.FollowMouse();
    }
}