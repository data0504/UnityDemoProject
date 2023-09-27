using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class FlyLogic
{
    public GameObject PrefabButterfly;
    public RectTransform ObjFlinghtRange;

    public float RowFlyingSpace = 1f;
    public float DelayTime = 0.075f;
    public float MaxColunmMovePosition = 350f;

    private GameObject Objbutterfly;
    private RectTransform Rectbutterfly;

    private float currentTime;

    public void Init()
    {
        Objbutterfly = GameObject.Instantiate(PrefabButterfly, ObjFlinghtRange);
        Rectbutterfly = Objbutterfly.GetComponent<RectTransform>();
        Vector2 presetPosition = new()
        {
            x = -ObjFlinghtRange.anchoredPosition.x,
            y = Rectbutterfly.anchoredPosition.y
        };
        Rectbutterfly.anchoredPosition = presetPosition;
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
            Move(Rectbutterfly, Random.Range(-MaxColunmMovePosition, MaxColunmMovePosition));

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
