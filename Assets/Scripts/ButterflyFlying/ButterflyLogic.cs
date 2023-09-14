using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class FlyLogic
{
    public GameObject ButterflyPresetObj;
    public RectTransform FlinghtRangeObj;

    public float RowFlyingSpace = 1f;
    public float DelayTime = 0.075f;
    public float MaxColunmMovePosition = 350f;

    private GameObject butterfly;
    private RectTransform butterflyRectTransform;


    private float currentTime;

    public void Init()
    {
        butterfly = GameObject.Instantiate(ButterflyPresetObj, FlinghtRangeObj);
        butterflyRectTransform = butterfly.GetComponent<RectTransform>();
        Vector2 presetPosition = new()
        {
            x = -FlinghtRangeObj.anchoredPosition.x,
            y = butterfly.GetComponent<RectTransform>().anchoredPosition.y
        };
        butterflyRectTransform.anchoredPosition = presetPosition;
    }
    private void Move(RectTransform butterflyRectTransform, float randomPosY)
    {
        Vector2 currentFlyingPosition = new()
        {
            x = butterflyRectTransform.anchoredPosition.x + RowFlyingSpace,
            y = randomPosY
        };
        butterflyRectTransform.anchoredPosition = currentFlyingPosition;
    }

    public void Flying()
    {
        currentTime += Time.deltaTime;
        if (currentTime > DelayTime)
        {
            Move(butterflyRectTransform, Random.Range(-MaxColunmMovePosition, MaxColunmMovePosition));

            currentTime = DelayTime - currentTime;
        }
    }
}


public class ButterflyLogic : MonoBehaviour
{
    public FlyLogic flyLogic = new();

    void Start()
    {
        flyLogic.Init();
    }

    void Update()
    {
        flyLogic.Flying();
    }
}
