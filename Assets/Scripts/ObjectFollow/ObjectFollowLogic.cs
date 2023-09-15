using UnityEngine;

public class ObjectFollowLogic : MonoBehaviour
{
    public ParentLogic Parent;
    public ChildrenLogic Children;
    void Update()
    {
        Parent.LeftAndRight();
        Parent.GetParentPosition(out Vector3 parentPosition);
        Children.DistanceFollow(parentPosition);
    }
}
