using UnityEngine;

public class CircleView : MonoBehaviour
{
    public GameObject[] perfabArrayList = new GameObject[3];
    public GameObject[] PerfabList
    {
        get { return perfabArrayList; }
    }
    public void DestroySelf()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
    }
}
