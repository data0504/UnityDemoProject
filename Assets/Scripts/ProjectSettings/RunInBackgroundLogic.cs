using UnityEngine;

public class RunInBackgroundLogic : MonoBehaviour
{
    public bool RunOn;
    void Start()
    {
        Application.runInBackground = RunOn;
    }
}
