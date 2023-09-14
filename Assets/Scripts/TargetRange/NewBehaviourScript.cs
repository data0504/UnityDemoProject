using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Test", 1f);
    }
    private void Test()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        //gameObject.AddComponent<BoxCollider>();
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
