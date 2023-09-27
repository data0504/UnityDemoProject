using UnityEngine;

public class TimeScaleLogice : MonoBehaviour
{
    public float TimeScaleSpeed = 3.0f;
    void Start()
    {
        Time.timeScale = TimeScaleSpeed;
    }
}