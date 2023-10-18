using UnityEngine;

public class DSMonitorShortFloor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Triangle")
        {
            Invoke("DestroyObj", 0.3f);
        }
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
