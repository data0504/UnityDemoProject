using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Text = UnityEngine.UI.Text;

public class PascalLogic : MonoBehaviour
{
    public RectTransform DisplayRange;
    public GameObject TextPerfab;
    public float TextSpace = 100f;

    private List<int> oldList = new() { 0 };
    private List<int> newList = new() { 1 };
    private List<int> printList;
    private bool CountOff;
    private int clickNumber;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickNumber++;
            if (CountOff)
            {
                for (int i = 0; i < (oldList.Count) - 1; i++)
                {
                    int currentNumber = oldList[i] + oldList[i + 1];
                    newList.Insert(i + 1, currentNumber);
                }

                printList = newList.ToList();

                int currentListIndex = printList.Count;
                printList.RemoveAt(currentListIndex - currentListIndex);
                currentListIndex = printList.Count;
                printList.RemoveAt(currentListIndex - 1);

                CreateTextPerfab(printList);

                oldList = newList.ToList();
                newList = new List<int>() { 0, 0 };
            }
            else
            {
                CreateTextPerfab(newList);

                oldList = new List<int>() { 0, 1, 0 };
                newList = new List<int>() { 0, 0 };
                CountOff = true;
            }
        }
    }

    private void CreateTextPerfab(List<int> printList)
    {
        GameObject TextObj = Instantiate(TextPerfab, DisplayRange);
        TextObj.GetComponent<Text>().text = string.Join(", ", printList);
        Vector2 newTextPosition = new()
        {
            x = TextObj.GetComponent<RectTransform>().anchoredPosition.x,
            y = DisplayRange.anchoredPosition.y - TextSpace * clickNumber
        };
        TextObj.GetComponent<RectTransform>().anchoredPosition = newTextPosition;
    }
}
