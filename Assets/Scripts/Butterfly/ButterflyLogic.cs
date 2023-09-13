using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class FlyLogic
{
    private GameObject butterfly;
    private RectTransform butterflyTransform;

    public float FlyingPosXSpace = 1f;
    public float delayTime = 0.075f;

    private float currentTime;

    public void Init(GameObject presetObj, RectTransform flinghtRangeObj)
    {
        butterfly = GameObject.Instantiate(presetObj, flinghtRangeObj);
        butterflyTransform = butterfly.GetComponent<RectTransform>();
        Vector2 presetPosition = new()
        {
            x = -flinghtRangeObj.anchoredPosition.x,
            y = butterfly.GetComponent<RectTransform>().anchoredPosition.y
        };
        butterflyTransform.anchoredPosition = presetPosition;
    }

    public void Flying()
    {
        currentTime += Time.deltaTime;
        if (currentTime > delayTime)
        {
            Move(butterflyTransform, Random.Range(-350f, 350f));

            currentTime = delayTime - currentTime;
        }
    }
    private void Move(RectTransform butterflyTransform, float randomPosY)
    {
        Vector2 currentFlyingPosition = new()
        {
            x = butterflyTransform.anchoredPosition.x + FlyingPosXSpace,
            y = randomPosY
        };
        butterflyTransform.anchoredPosition = currentFlyingPosition;
    }
}


public class ButterflyLogic : MonoBehaviour
{
    public GameObject ButterflyPresetObj; 
    public RectTransform FlinghtRangeObj;
    public FlyLogic flyLogic  = new();

    void Start()
    {
        flyLogic.Init(ButterflyPresetObj, FlinghtRangeObj);
    }

    void Update()
    {
        flyLogic.Flying();
    }
}
