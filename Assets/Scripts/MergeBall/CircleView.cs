using UnityEngine;
public delegate void CollisionOtherBall(Collision2D collision);

public class CircleView : MonoBehaviour
{
    public GameObject[] perfabArrayList = new GameObject[3];
    public GameObject[] PerfabList
    {
        get { return perfabArrayList; }
    }
    CollisionOtherBall collisionOtherBall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionOtherBall.Invoke(collision);
    }

    public void DestroySelf()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
    }

    public void RegisterCollisionOtherBall(CollisionOtherBall action)
    {
        collisionOtherBall = action;
    }
}
