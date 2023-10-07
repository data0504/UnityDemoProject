using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCupLogic : MonoBehaviour
{
    public AnimationClip Down;
    public AnimationClip Up;
    public AnimationClip Switch12;
    public AnimationClip Switch23;
    public AnimationClip Switch123;
    public Animator AnimaObj;
    public GameObject Circle;
    private int[] circle = {1,0,0};
    private bool isplaying;

    void Start()
    {
        //StartCoroutine(WaitAndPrint());
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

    private IEnumerator AnimaCup()
    {
        isplaying = true;

        AnimaObj.Play("Up");
        yield return new WaitForSeconds(Up.length);
        AnimaObj.Play("Down");
        yield return new WaitForSeconds(Down.length);
        Circle.SetActive(false);

        string[] animClipName = { "Switch12", "Switch23", "Switch123" };
        float[] animClip = { Switch12.length, Switch23.length, Switch123.length };

        for (int i = 0; i < 10; i++)
        {
            int randomNumber = Random.Range(0, animClipName.Length);
            AnimaObj.Play($"{animClipName[randomNumber]}", 0, 0);
            Debug.Log(i);

            if (animClipName[randomNumber] == animClipName[0])
            {
                int prefabNumer = circle[0];
                circle[0] = circle[1];
                circle[1] = prefabNumer;
            }

            if (animClipName[randomNumber] == animClipName[1])
            {
                int prefabNumer = circle[1];
                circle[1] = circle[2];
                circle[2] = prefabNumer;
            }
            if (animClipName[randomNumber] == animClipName[2])
            {
                int prefabNumer = circle[2];
                circle[2] = circle[1];
                circle[1] = circle[0];
                circle[0] = prefabNumer;
            }
            yield return new WaitForSeconds(animClip[randomNumber]);
        }

        if (circle[0] == 1)
        {
            Circle.transform.position = new Vector3(-5f, 0,0.1f);
        }
        if (circle[1] == 1)
        {
            Circle.transform.position = new Vector3(0f, 0, 0.1f);
        }
        if (circle[2] == 1)
        {
            Circle.transform.position = new Vector3(5f, 0, 0.1f);
        }
    }
    private void UpCup()
    {
        AnimaObj.Play("Up");
        Circle.SetActive(true);
        isplaying = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isplaying)
            {
                StartCoroutine(AnimaCup());
            }
            else
            {
                UpCup();
            }
        }
    }

}
