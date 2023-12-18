using UnityEngine;

public class TestDelegateController : MonoBehaviour
{
    public TestDelegateView testDelegateView;
    public int Max = 0;
    public void PostTaiMessage(string message)
    {
        //TODO Translate to Chinese
        Debug.Log(message + "(泰文)");
    }
    public void DecreaseTimes()
    {
        Max--;
        Debug.Log(Max);
    }


    private void Start()
    {
        Max = 100;
        testDelegateView.Board("卡", PostTaiMessage);
        testDelegateView.SetDecreaseTimes(DecreaseTimes);
    }
}
