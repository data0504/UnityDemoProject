using UnityEngine;
using UnityEngine.UI;

public class ScrollbarInfo : MonoBehaviour
{
    public RectTransform ChatRange;
    public void DragScrollbar(float lastPos)
    {
        Scrollbar isScrollbar = ChatRange.transform.Find("Scrollbar").GetComponent<Scrollbar>();
        RectTransform contentRectTransform = ChatRange.transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>();
        float x = contentRectTransform.localPosition.x;
        float y = contentRectTransform.localPosition.y;
        float reslut = 0;

        if (lastPos < 1050) return;
        if (lastPos > 1050) reslut = lastPos - 1050;
        if (reslut < 0) reslut = 0;

        float cooperate = isScrollbar.value * reslut;
        contentRectTransform.localPosition = new Vector2(x, cooperate);
    }
}

