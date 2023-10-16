using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DsMonitorHp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Triangle")
        {
            Debug.Log("Reduce");
        }
    }
}
