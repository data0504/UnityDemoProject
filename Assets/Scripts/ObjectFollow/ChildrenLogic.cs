using UnityEngine;

public class ChildrenLogic : MonoBehaviour
{
    public Transform ObjTranform;
    public float FollowSleep = 5f;
    public void DistanceFollow(Vector3 currentTarget)
    {
        Vector3 currentPosition = ObjTranform.position;
        ObjTranform.position = Vector3.Lerp(currentPosition, currentTarget, FollowSleep * Time.deltaTime);
    }
}