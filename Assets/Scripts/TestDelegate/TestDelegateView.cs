using UnityEngine;

public delegate void PostDelegate(string message);
public delegate void DecreaseDelegate();

public class TestDelegateView : MonoBehaviour
{
    DecreaseDelegate b;
    private void Update()
    {
        b();
    }
    void Start()
    {
        Board("哈哈", PostChineseMessage);
        Board("XDXD", PostEnglishMessage);

        InvokeDelegate();
    }

    private  void InvokeDelegate()
    {
        PostDelegate a = PostEnglishMessage;
        a.Invoke("哈哈");
    }

    public  void Board(string message, PostDelegate postMessage)
    {
        //  在這可以加上一些postMessage共用的程式碼 
        message += "!";     //  在所有message後面加上驚嘆號
        postMessage(message);
    }

    public void SetDecreaseTimes(DecreaseDelegate decreaseTimes)
    {
        b = decreaseTimes;
    }

    public  void PostChineseMessage(string message)
    {
        //TODO Translate to Chinese
        Debug.Log(message + "(中文)");
    }

    public  void PostEnglishMessage(string message)
    {
        //TODO Translate to English
        Debug.Log(message + "(English)");
    }
}
