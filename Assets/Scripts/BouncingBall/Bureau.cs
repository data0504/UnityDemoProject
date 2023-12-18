using UnityEngine;

public class Bureau : MonoBehaviour
{
    public Transform GroupBaffle;
    public GameObject PrefabCircle;
    public GameObject PrefabSqare;
    public GameObject PerfabTrangle;
    public float width = 8.87f;
    public float high = 5f;
    public float MoveSpeed = 1f;

    private GameObject[] Objs = new GameObject[3];
    private float[] ObjsPointX = new float[3];
    Vector3 targetPos;
    private bool offMove;

    void Start()
    {
        // Array save
        Objs[0] = PrefabCircle;
        Objs[1] = PrefabSqare;
        Objs[2] = PerfabTrangle;

        // objsPointX
        for (int i = 0; i < Objs.Length; i++) ObjsPointX[i] = Objs[0].GetComponent<Renderer>().bounds.size.x;

        // Random Object
        float keepPosX = -width;
        for (int i = 0; i < 10; i++)
        {
            int zeroOne = Random.Range(0, 2);
            float randomX = Random.Range(1.5f, 2f);
            float randomY = Random.Range(-5f, 0);
            if (zeroOne == 1)
            {
                if (i != 0)
                {
                    //ÀË¬d¬O§_¶W¹L
                    int objNumber = Random.Range(0, 3);
                    float testKeepPosX = keepPosX + ObjsPointX[objNumber] + randomX;
                    if (testKeepPosX >= width)
                    {
                        break;
                    }
                    else
                    {
                        keepPosX += ObjsPointX[objNumber] + randomX;
                        Vector2 newPos = new Vector2()
                        {
                            x = keepPosX,
                            y = randomY
                        };
                        GameObject prefabObj = Instantiate(Objs[objNumber], newPos, Quaternion.identity, this.transform);
                    }
                }
                else
                {
                    GameObject obj = Objs[Random.Range(0, 3)];
                    GameObject prefabObj = Instantiate(obj, transform.position, Quaternion.identity, this.transform);
                    float objlong = prefabObj.GetComponent<Renderer>().bounds.size.x / 2;
                    keepPosX += objlong;
                    prefabObj.transform.position = new Vector2(keepPosX, randomY);
                }
            }
            else keepPosX += randomX;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offMove = true;
            targetPos = new()
            {
                x = GroupBaffle.position.x,
                y = GroupBaffle.position.y + 1,
                z = GroupBaffle.position.y
            };
        }

        if (offMove)
        {
            GroupBaffle.position = Vector3.Lerp(GroupBaffle.position, targetPos, MoveSpeed);
        }

        if (GroupBaffle.position == targetPos)
        {
            offMove = false;
        }
    }
}
