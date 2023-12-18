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
        Board("����", PostChineseMessage);
        Board("XDXD", PostEnglishMessage);

        InvokeDelegate();
    }

    private  void InvokeDelegate()
    {
        PostDelegate a = PostEnglishMessage;
        a.Invoke("����");
    }

    public  void Board(string message, PostDelegate postMessage)
    {
        //  �b�o�i�H�[�W�@��postMessage�@�Ϊ��{���X 
        message += "!";     //  �b�Ҧ�message�᭱�[�W��ĸ�
        postMessage(message);
    }

    public void SetDecreaseTimes(DecreaseDelegate decreaseTimes)
    {
        b = decreaseTimes;
    }

    public  void PostChineseMessage(string message)
    {
        //TODO Translate to Chinese
        Debug.Log(message + "(����)");
    }

    public  void PostEnglishMessage(string message)
    {
        //TODO Translate to English
        Debug.Log(message + "(English)");
    }
}
