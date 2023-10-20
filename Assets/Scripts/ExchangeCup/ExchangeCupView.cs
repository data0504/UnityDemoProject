using System.Collections;
using UnityEngine;

public class ExchangeCupView : MonoBehaviour
{
    public Animator AnimaObj;

    public AnimationClip Up;
    public AnimationClip Down;

    public AnimationClip Switch12;
    public AnimationClip Switch23;
    public AnimationClip Switch123;

    public GameObject Circle;

    public void PlayUpAnim()
    { 
        AnimaObj.Play(Up.name, 0, 0);
    }
    public void PlayDownAnim()
    {
        AnimaObj.Play(Down.name, 0, 0);
    }

    public IEnumerator PlayingProcessAnim((string[], float[], int[]) animPlayingList)
    {
        for (int i = 0; i < animPlayingList.Item3.Length; i++)
        {
            int playNumber = animPlayingList.Item3[i];

            string playName = animPlayingList.Item1[playNumber];
            float playTime = animPlayingList.Item2[playNumber];

            AnimaObj.Play(playName, 0, 0);
            Debug.Log(i);

            yield return new WaitForSeconds(playTime);
        }
    }

    public void SetCircleActive(bool value)
    {
        Circle.SetActive(value);
    }
    public void MoveCirclePos(Vector3 newPos)
    {
        Circle.transform.position = newPos;
    }
}
