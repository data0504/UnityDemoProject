using UnityEngine;

public class ParentLogic : MonoBehaviour
{
    public Transform ObjTransform;
    public float MoveSpace = 0.1f;

    public void GetParentPosition(out Vector3 parentPosition)
    {
        Vector3 newObjPosition = new()
        {
            x = ObjTransform.position.x,
            y = ObjTransform.position.y,
            z = ObjTransform.position.z
        };
        parentPosition = newObjPosition;
    }
    public void LeftAndRight()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) ObjTransform.position += new Vector3(-MoveSpace, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow)) ObjTransform.position += new Vector3(0, -MoveSpace, 0);
        if (Input.GetKey(KeyCode.RightArrow)) ObjTransform.position += new Vector3(MoveSpace, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow)) ObjTransform.position += new Vector3(0, MoveSpace, 0);
    }
}
