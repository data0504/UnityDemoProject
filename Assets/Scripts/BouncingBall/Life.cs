using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject movingObject;  // 你的物件
    public RectTransform uiElement;  // 你的UI元素，這裡假設使用RectTransform
    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 獲取物件的當前位置
        Vector2 objectPosition = movingObject.transform.position;

        // 將物件的位置轉換為相機坐標
        Vector2 uiPosition = mainCamera.WorldToScreenPoint(objectPosition);

        // 設置UI元素的中心點位置為物件的中心點位置
        uiElement.position = uiPosition;
    }
}
