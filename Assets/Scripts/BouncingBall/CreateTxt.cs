using UnityEngine;
using UnityEngine.UI;

public class CreateTxt : MonoBehaviour
{
    public GameObject SelfObj;  // �A������
    public GameObject Ui;  // �A��UI�����A�o�̰��]�ϥ�RectTransform
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
        // ������󪺷�e��m
        Vector2 objectPosition = SelfObj.transform.position;

        // �N���󪺦�m�ഫ���۾�����
        Vector2 uiPosition = mainCamera.WorldToScreenPoint(objectPosition);

        // �]�mUI�����������I��m�����󪺤����I��m
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
