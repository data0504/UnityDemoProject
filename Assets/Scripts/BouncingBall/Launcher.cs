using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject PerfabAttackCircle;
    public List<GameObject> BallShell = new();
    public float launchSpeed = 1f;
    public float SpaceSpeed = 0.5f;
    public int BallNumber = 3;

    private Vector3 mousePosion;
    private float fireAngle;
    private bool launcherFuse;
    private bool spaceFuse;
    private int number = 0;

    void Update()
    {
        if (launcherFuse == false && number == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosion = Input.mousePosition;
                mousePosion = Camera.main.ScreenToWorldPoint(mousePosion);

                fireAngle = Vector2.Angle(mousePosion - this.transform.position, Vector2.up);
                if (mousePosion.x > this.transform.position.x) fireAngle = -fireAngle;

                launcherFuse = true;
                spaceFuse = true;
            }
        }

        if (spaceFuse)
        {
            if (number < BallNumber)
            {
                Invoke("Pop", SpaceSpeed);
                spaceFuse = false;
                number++;
            }
            else
            {
                launcherFuse = false;
                spaceFuse = false;
            }
        }
    }

    private void Pop()
    {
        GameObject prefabObj = Instantiate(PerfabAttackCircle, transform.position, Quaternion.identity, this.transform);
        prefabObj.GetComponent<Rigidbody2D>().velocity = (mousePosion - transform.position) * launchSpeed;
        prefabObj.transform.eulerAngles = new Vector3(0, 0, fireAngle);
        BallShell.Add(prefabObj);
        spaceFuse = true;
    }
}
