using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSetBuffle : MonoBehaviour
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
        if (currentTriangleHeight > initHeight+15)
        {
            Destroy(gameObject.transform.gameObject);
        }   
    }
}
