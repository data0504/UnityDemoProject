using UnityEngine;
using UnityEngine.UI;

public class BubbbleData
{
    public GameObject isBubble;
    public string message;
    public bool leftRight;

    public float stepVertical; 
    public float stepHorizontal; 
    public float widthTotalCount; 
    public float maxTextWidth; 

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

        float hpos = leftRight ? stepHorizontal / 2 : -stepHorizontal / 2;
        float vPos = stepVertical - isBubble.GetComponent<RectTransform>().sizeDelta.y - lastPos;
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

        for (int i = 0; i < widthTotalCount; i++)
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
        if (isBubbleText.preferredWidth >= maxTextWidth)
        {
            isBubbleText.GetComponent<LayoutElement>().preferredWidth = maxTextWidth;
        }

        float singleHeight = 16f;
        float currentHeight = isBubbleText.preferredHeight;

        float heightTotalCount = Mathf.RoundToInt(currentHeight / (singleHeight * widthTotalCount));
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
    public GameObject rigthChat;
    public GameObject leftChat;
    public GameObject chatpanel;
    public float stepVertical;
    public float stepHorizontal;
    public float widthTotalCount;
    public float maxTextWidth; 

    private float lastPos; 

    public void Init()
    {
        lastPos = 0.0f;
    }

    public void AddBubble(string content, bool isMy)
    {
        BubbbleData bubbbleData = new()
        {
            isBubble = NewContent(isMy),
            message = content,
            leftRight = isMy,

            stepVertical = stepVertical,
            stepHorizontal = stepHorizontal,
            widthTotalCount = widthTotalCount,
            maxTextWidth = maxTextWidth,

            lastPos = lastPos
        };
        ContentData contentData = new()
        {
            contentRectTransform = chatpanel.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>(),
            currentContentChildNumber = chatpanel.transform.Find("ViewPort").Find("Content").childCount
        };
        ScrollbarData scrollbarData = new()
        {
            isScrollbar = chatpanel.transform.Find("Scrollbar").GetComponent<Scrollbar>(),
            currentContentChildNumber = contentData.currentContentChildNumber,
            childNumberLimit = 7
        };


        bubbbleData.Start();
        lastPos = bubbbleData.lastPos;
        scrollbarData.Start();
        contentData.ContentChatY(scrollbarData.onOff);
    }
    public void DragScrollbar()
    {
        Scrollbar isScrollbar = chatpanel.transform.Find("Scrollbar").GetComponent<Scrollbar>();
        RectTransform contentRectTransform = chatpanel.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>();
        float x = contentRectTransform.localPosition.x;
        float y = contentRectTransform.localPosition.y;
        float reslut = 0;

        if (lastPos < 1050) return;
        if (lastPos > 1050) reslut = lastPos - 1050;
        if (reslut < 0) reslut = 0;

        float cooperate = isScrollbar.value * reslut;
        contentRectTransform.localPosition = new Vector2(x, cooperate);
    }
    private GameObject NewContent(bool isMy)
    {
        RectTransform contentRectTransform = chatpanel.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>();
        return isMy ? Instantiate(rigthChat, contentRectTransform) : Instantiate(leftChat, contentRectTransform);
    }
}
