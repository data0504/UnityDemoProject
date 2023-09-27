using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpOpenGame : MonoBehaviour
{
    public JumpTriangle TriangleData;

    public JumpBuffleData BuffleData;

    public JumpCamera CameraData;

    public JumpText ViewButtonAndTextData;
    #region OLD
    /*
    private void IsJumpGame() { SceneManager.LoadScene("Jump_01", LoadSceneMode.Single); }
    public void LoadingScenes(bool onOff)
    {
        //if (onOff) Invoke(nameof(IsJumpGame), 1.5f);
    }
     
     */
    #endregion
    public void GameReplay()
    {
        CameraData.SetCameraCurrentCenterPonit(0);
        int randonRepalyMethod = Random.Range(0, 1);
        randonRepalyMethod = 1;
        if (randonRepalyMethod == 0) Replay();
        else if (randonRepalyMethod == 1) Reducer();
    }

    private static void Replay()
    {
        SceneManager.LoadScene("Jump_01", LoadSceneMode.Single);
    }

    private void Reducer()
    {
        TriangleData.Init();
        CameraData.Init();
        BuffleData.Init();
    }

}
