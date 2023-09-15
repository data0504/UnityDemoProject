using TMPro;
using UnityEngine;

public class MessageLogic : MonoBehaviour
{
    public MessageInfo messageInfo;  
    public TMP_InputField inputtext;
    void Start()
    {
        messageInfo.Init();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            messageInfo.AddBubble("LLLLLeft", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            messageInfo.AddBubble("RRRRight", true);
        }

         bool isEnterPressed = false;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isEnterPressed)
            {
                string a = inputtext.text;
                messageInfo.AddBubble($"{a}", true);
                inputtext.text = ""; 
                isEnterPressed = true;
            }
        }
        else
        {
            isEnterPressed = false;
        }

        if (Input.GetMouseButton(0))
        {
            messageInfo.DragScrollbar();
        }
    }
}
