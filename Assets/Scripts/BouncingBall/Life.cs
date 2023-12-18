using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject movingObject;  // �A������
    public RectTransform uiElement;  // �A��UI�����A�o�̰��]�ϥ�RectTransform
    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ������󪺷�e��m
        Vector2 objectPosition = movingObject.transform.position;

        // �N���󪺦�m�ഫ���۾�����
        Vector2 uiPosition = mainCamera.WorldToScreenPoint(objectPosition);

        // �]�mUI�����������I��m�����󪺤����I��m
        uiElement.position = uiPosition;
    }
}
