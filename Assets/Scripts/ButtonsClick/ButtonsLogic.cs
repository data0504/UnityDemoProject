using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class ButtonsInfo
{
    public Button ButtonOneObj; 
    public Button ButtonTwoObj; 

    public float MaxRowMovePosition = 550f;
    public float MaxColunmMovePosition = 345f;

    public void CreateMonitorButtonEvent()
    {
        ButtonOneObj.onClick.AddListener(ClickButtonOneObj); 
        ButtonTwoObj.onClick.AddListener(ClickButtonTwoObj); 
    }

    private Vector2 RandomRectPosition()
    {
        Vector2 newRectTansform = new()
        {
            x = Random.Range(-MaxRowMovePosition, MaxRowMovePosition),
            y = Random.Range(-MaxColunmMovePosition, MaxColunmMovePosition)
        };
        return newRectTansform;
    }

    private void ClickButtonOneObj()
    {
        ButtonOneObj.gameObject.SetActive(false);

        ButtonOneObj.GetComponent<RectTransform>().anchoredPosition = RandomRectPosition();

        ButtonTwoObj.gameObject.SetActive(true);
    }

    private void ClickButtonTwoObj()
    {
        ButtonOneObj.gameObject.SetActive(true);

        ButtonTwoObj.GetComponent<RectTransform>().anchoredPosition = RandomRectPosition();

        ButtonTwoObj.gameObject.SetActive(false);
    }
}


public class ButtonsLogic : MonoBehaviour
{
    public ButtonsInfo buttonsInfo = new();

    void Start() 
    {
        buttonsInfo.CreateMonitorButtonEvent();
    }

    void Update() 
    {
        
    }
}
