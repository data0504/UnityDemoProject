using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSMonitorGameOver : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Triangle")
        {
            Debug.Log("GameOver");
        }
    }
}
