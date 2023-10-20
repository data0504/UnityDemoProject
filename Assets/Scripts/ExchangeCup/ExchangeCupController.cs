using System.Collections;
using UnityEngine;

public class ExchangeCupController : MonoBehaviour
{
    public ExchangeCupModel exchangeCupModel = new ExchangeCupModel();
    public ExchangeCupView exchangeCupView = new ExchangeCupView();
    void Start()
    {
        exchangeCupModel.SetAnimClipHeadTailNameList(exchangeCupView.Up.name, exchangeCupView.Down.name);
        exchangeCupModel.SetAnimClipHeadTailTimeList(exchangeCupView.Up.length, exchangeCupView.Down.length);

        exchangeCupModel.SetAnimClipProcessNameList(exchangeCupView.Switch12.name, exchangeCupView.Switch23.name, exchangeCupView.Switch123.name);
        exchangeCupModel.SetAnimClipProcessTimeList(exchangeCupView.Switch12.length, exchangeCupView.Switch23.length, exchangeCupView.Switch123.length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (exchangeCupModel.CheckFirstPlaying()) StartCoroutine(PlayingStartFirstAnim());
             
            if (exchangeCupModel.CheckPlaying()) StartCoroutine(PlayingStartOtherAnim());
        }
    }

    private IEnumerator PlayingStartFirstAnim()
    {
        exchangeCupModel.SetPlayState();

        exchangeCupView.PlayUpAnim();
        yield return new WaitForSeconds(exchangeCupView.Up.length);
        exchangeCupView.PlayDownAnim();
        yield return new WaitForSeconds(exchangeCupView.Down.length);
        exchangeCupView.SetCircleActive(exchangeCupModel.SetCircleActive(exchangeCupView.Circle.activeSelf));

        StartCoroutine(exchangeCupView.PlayingProcessAnim(exchangeCupModel.GetAnimPlayLists()));
        exchangeCupView.MoveCirclePos(exchangeCupModel.CheckCircleListOne());
        yield return new WaitForSeconds(exchangeCupModel.GetAllProecssAnimTime());
        exchangeCupModel.SetInitAllProecssAnimTime();

        exchangeCupView.SetCircleActive(exchangeCupModel.SetCircleActive(exchangeCupView.Circle.activeSelf));
        exchangeCupView.PlayUpAnim();
        yield return new WaitForSeconds(exchangeCupView.Up.length);

        exchangeCupModel.SetPlayState();
    }
    private IEnumerator PlayingStartOtherAnim()
    {
        exchangeCupModel.SetPlayState();

        exchangeCupView.PlayDownAnim();
        yield return new WaitForSeconds(exchangeCupView.Down.length);
        exchangeCupView.SetCircleActive(exchangeCupModel.SetCircleActive(exchangeCupView.Circle.activeSelf));

        StartCoroutine(exchangeCupView.PlayingProcessAnim(exchangeCupModel.GetAnimPlayLists()));
        exchangeCupView.MoveCirclePos(exchangeCupModel.CheckCircleListOne());
        yield return new WaitForSeconds(exchangeCupModel.GetAllProecssAnimTime());
        exchangeCupModel.SetInitAllProecssAnimTime();

        exchangeCupView.SetCircleActive(exchangeCupModel.SetCircleActive(exchangeCupView.Circle.activeSelf));
        exchangeCupView.PlayUpAnim();
        yield return new WaitForSeconds(exchangeCupView.Up.length);

        exchangeCupModel.SetPlayState();
    }
    private static void LearnIndex()
    {
        int[] arr = new int[50];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i * 2;
        }

        int arrLastNumber = arr[arr.Length - 1];
        int arrLastIndex = arr.Length - 1;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            arr[arrLastIndex - i] = arr[arrLastIndex - (i + 1)];
        }
        arr[0] = arrLastNumber;
        Debug.Log($"{arr}");
        string result = string.Empty;
        for (int i = 0; i < arr.Length; i++)
        {
            result += arr[i] + ", ";
        }
        Debug.Log(result);
    }
}
