using UnityEngine;
using Random = UnityEngine.Random;

public class DSTriangleLogic : MonoBehaviour
{
    public Rigidbody2D RigTranTriangle;
    public Transform TranTriangle;
    public SpriteRenderer spriteTriangle;
    public float MoveSpeed = 0.05f;
    public float MoveMaxSpace = 8.4f;

    private float currentTime;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "ReduceHp")
        {
            spriteTriangle.color = Color.red;
            Invoke("SetColor", 0.05f);
        }
    }
    private void SetColor()
    {
        spriteTriangle.color = Color.white;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "ReduceHp")
        {
            currentTime += Time.deltaTime;
            if (currentTime > 0.1f)
            {
                RigTranTriangle.GetComponent<Collider2D>().isTrigger = true;
                Invoke("SetVelcity", 0.01f);
                Invoke("SetTrigger", 0.25f);
                currentTime = 0;
            }
        }
    }
    private void SetVelcity()
    {
        Vector3 newPos = new Vector3()
        {
            x = Random.Range(-2, 2),
            y = -1 * 5,
            z = 1
        };
        RigTranTriangle.velocity = newPos;

    }
    private void SetTrigger()
    {
        RigTranTriangle.GetComponent<Collider2D>().isTrigger = false;
    }


    void Update()
    {
        SetMove();
        MonitorMoveMax();
    }
    private void SetMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TranTriangle.position = new Vector3(TranTriangle.position.x - MoveSpeed, TranTriangle.position.y, TranTriangle.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TranTriangle.position = new Vector3(TranTriangle.position.x + MoveSpeed, TranTriangle.position.y, TranTriangle.position.z);
        }
    }
    private void MonitorMoveMax()
    {
        if (TranTriangle.position.x < -MoveMaxSpace)
        {
            TranTriangle.position = new Vector3(-MoveMaxSpace, TranTriangle.position.y, TranTriangle.position.z);
        }
        if (TranTriangle.position.x > MoveMaxSpace)
        {
            TranTriangle.position = new Vector3(MoveMaxSpace, TranTriangle.position.y, TranTriangle.position.z);
        }
    }
}
