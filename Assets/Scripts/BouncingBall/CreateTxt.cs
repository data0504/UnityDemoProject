using UnityEngine;
using UnityEngine.UI;

public class CreateTxt : MonoBehaviour
{
    public GameObject SelfObj;  // 你的物件
    public GameObject Ui;  // 你的UI元素，這裡假設使用RectTransform
    public int HpNumber;

    private Camera mainCamera;
    private Transform canvasTrans;
    private GameObject textObj;
    private Transform uiTrans;
    private Text uiText;

    void Start()
    {
        mainCamera = Camera.main;

        canvasTrans = GameObject.Find("Canvas").transform;
        textObj = Instantiate(Ui, transform.position, Quaternion.identity, canvasTrans);
        uiTrans = textObj.transform;
        uiText = textObj.GetComponent<Text>();
        HpNumber = Random.Range(1, 3);

        uiText.text = "" + HpNumber;
    }

    void Update()
    {
        // 獲取物件的當前位置
        Vector2 objectPosition = SelfObj.transform.position;

        // 將物件的位置轉換為相機坐標
        Vector2 uiPosition = mainCamera.WorldToScreenPoint(objectPosition);

        // 設置UI元素的中心點位置為物件的中心點位置
        uiTrans.position = uiPosition;

        if (HpNumber == 0)
        {
            Destroy(SelfObj.gameObject);
            Destroy(textObj.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.name == "PreFabAttackCircle(Clone)")
        {
            HpNumber--;
            uiText.text = "" + HpNumber;
        }
    }
}
