using UnityEngine;
using UnityEngine.UI;

public class BubbleInfo
{
    public GameObject isBubble;
    public string message;
    public bool id;

    public float ChatHieght; 
    public float ChatWidht; 
    public float BubblePosY; 
    public float maxContentRowNumber; 

    private Text isBubbleText;
    public float lastPos; 

    public void Start()
    {
        IsBubbleText();
        IsBubbleDistance();
        IsBubblePosition();
    }

    private void IsBubblePosition()
    {
        float halfHeadLength = isBubble.GetComponent<RectTransform>().rect.height / 2;

        float hpos = id ? ChatWidht / 2 : -ChatWidht / 2;
        float vPos = ChatHieght - isBubble.GetComponent<RectTransform>().sizeDelta.y - lastPos;
        lastPos += isBubble.GetComponent<RectTransform>().sizeDelta.y + halfHeadLength;

        isBubble.GetComponent<RectTransform>().localPosition = new Vector2(hpos, vPos);
    }

    private void IsBubbleDistance()
    {
        RectTransform chat = isBubble.transform.Find("bubble").GetComponent<RectTransform>();
        float chatX = chat.anchoredPosition.x;
        float chatY = chat.anchoredPosition.y;

        float singleWidth = 14f;
        float totalWidth = singleWidth;
        int widthNumber = 0;
        float currentWidth = isBubbleText.preferredWidth;

        for (int i = 0; i < BubblePosY; i++)
        {
            if (currentWidth < totalWidth) break;
            totalWidth += singleWidth;
            widthNumber++;
        }

        for (int index = 1; index <= widthNumber; index++)
        {
            if (index == 1) chat.anchoredPosition = new(chatX += 5, chatY);
            if (index > 1) chat.anchoredPosition = new(chatX += 8, chatY);
        }

        Debug.Log($"{isBubbleText.preferredWidth}");
        if (isBubbleText.preferredWidth >= maxContentRowNumber)
        {
            isBubbleText.GetComponent<LayoutElement>().preferredWidth = maxContentRowNumber;
        }

        float singleHeight = 16f;
        float currentHeight = isBubbleText.preferredHeight;

        float heightTotalCount = Mathf.RoundToInt(currentHeight / (singleHeight * BubblePosY));
        if (heightTotalCount > 1)
        {
            for (int index = 1; index <= heightTotalCount; index++)
            {
                if (index == 2) chat.anchoredPosition = new(chatX, chatY -= 5);
                if (index > 2) chat.anchoredPosition = new(chatX, chatY -= 8);
            }
        }
    }

    private void IsBubbleText()
    {
        isBubbleText = isBubble.transform.Find("bubble").Find("Text").GetComponent<Text>();
        isBubbleText.text = message;
    }
}

public class ScrollbarData
{
    public Scrollbar isScrollbar;
    public int currentContentChildNumber;
    public int childNumberLimit = 7;

    private float currentEntityProportion;

    public float currentIncorporealProportion;
    public bool onOff;

    public void Start()
    {
        onOff = false;
        if (currentContentChildNumber <= childNumberLimit) isScrollbar.size = 1f;

        if (currentContentChildNumber > childNumberLimit)
        {
            currentEntityProportion = (float)childNumberLimit / currentContentChildNumber;
            currentIncorporealProportion = 1.0f - currentEntityProportion;

            isScrollbar.size = currentEntityProportion;
            isScrollbar.value = 1.0f;
            onOff = true;
        }
    }
}

public class ContentData
{
    public RectTransform contentRectTransform;
    public int currentContentChildNumber;
    public void ContentChatY(bool onOff)
    {
        if (onOff)
        {
            float chatX = contentRectTransform.anchoredPosition.x;
            float chatY = contentRectTransform.anchoredPosition.y;
            contentRectTransform.anchoredPosition = new(chatX, chatY += 50 * 3);
        }
    }
}

public class MessageInfo : MonoBehaviour
{
    public GameObject ChatRigth;
    public GameObject ChatLeft;
    public GameObject ChatRange;
    public float BubblePosY;
    public float ChatWidht = 1000f;
    public float ChatHieght = 570f;

    private float maxContentRowNumber = 140f;
    private float lastPos = 0.0f;


    public float GetLastPosY()
    {
        return lastPos;
    }

    public void AddBubble(string content, bool id)
    {
        BubbleInfo bubbbleData = new()
        {
            isBubble = NewContent(id),
            message = content,
            id = id,

            ChatHieght = ChatHieght,
            ChatWidht = ChatWidht,
            BubblePosY = BubblePosY,
            maxContentRowNumber = maxContentRowNumber,

            lastPos = lastPos
        };
        ContentData contentData = new()
        {
            contentRectTransform = ChatRange.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>(),
            currentContentChildNumber = ChatRange.transform.Find("ViewPort").Find("Content").childCount
        };
        ScrollbarData scrollbarData = new()
        {
            isScrollbar = ChatRange.transform.Find("Scrollbar").GetComponent<Scrollbar>(),
            currentContentChildNumber = contentData.currentContentChildNumber,
            childNumberLimit = 7
        };


        bubbbleData.Start();
        lastPos = bubbbleData.lastPos;
        scrollbarData.Start();
        contentData.ContentChatY(scrollbarData.onOff);
    }
    private GameObject NewContent(bool isMy)
    {
        RectTransform contentRectTransform = ChatRange.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>();
        return isMy ? Instantiate(ChatRigth, contentRectTransform) : Instantiate(ChatLeft, contentRectTransform);
    }
}
