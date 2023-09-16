using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSetSpeedUp : MonoBehaviour
{
    private float initHeight;
    private float currentTriangleHeight;

    public void SetInitHeight(float ceateHeight)
    {
        initHeight = ceateHeight;
    }
    public void SetCurrentHeight(float triangleHeiglr)
    {
        currentTriangleHeight = triangleHeiglr;
    }
    private void Update()
    {
        if (currentTriangleHeight > initHeight + 15)
        {
            Destroy(gameObject.transform.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("test")) Destroy(gameObject.transform.gameObject);

    }
}
