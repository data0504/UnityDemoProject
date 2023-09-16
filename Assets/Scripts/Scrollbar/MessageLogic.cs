using TMPro;
using UnityEngine;

public class MessageLogic : MonoBehaviour
{
    public MessageInfo messageInfo;
    public ScrollbarInfo scrollbarInfo;
    public TMP_InputField inputtext;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
            messageInfo.AddBubble("LLLLLeft", false);

        if (Input.GetKeyDown(KeyCode.S)) 
            messageInfo.AddBubble("RRRRight", true);

        if (Input.GetMouseButton(0))
            scrollbarInfo.DragScrollbar(messageInfo.GetLastPosY());
    }
}
