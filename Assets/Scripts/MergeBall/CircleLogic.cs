using UnityEngine;

public class CircleLogic : MonoBehaviour
{
    public GameObject[] perfabArrayList = new GameObject[3];
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == perfabArrayList[0].name && gameObject.name == perfabArrayList[0].name)
        {
            DestroySelf();
            CreateNewBall(collision,1);
        }

        if (collision.gameObject.name == perfabArrayList[1].name && gameObject.name == perfabArrayList[1].name)
        {
            DestroySelf();
            CreateNewBall(collision, 2);
        }
    }

    private static void CreateNewBall(Collision2D collision, int value)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 contactPoint = contact.point;
        GameScore.MergeBallSameCreateBallNumber = value;
        GameScore.MergeBallSameCreateBallPos = contactPoint;
    }

    private void DestroySelf()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
    }
}
