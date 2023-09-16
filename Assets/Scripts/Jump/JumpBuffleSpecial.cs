using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuffleSpecial : MonoBehaviour
{
    void DestroySelf() 
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("test")) Invoke(nameof(DestroySelf), 2f);
    }

}
