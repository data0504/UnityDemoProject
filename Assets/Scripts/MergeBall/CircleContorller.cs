using UnityEngine;

public class CircleContorller : MonoBehaviour
{
    public CircleModel CircleModel = new CircleModel();
    public CircleView CircleView = new CircleView();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CircleModel.CheckSameCollsionName(collision.gameObject.name, CircleView.gameObject.name, CircleView.PerfabList))
        {
            CircleView.DestroySelf();
            CircleModel.CreateNewBall(collision);
        }
    }
}
