using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpText : MonoBehaviour
{
    public Text viewCurrentHieghtText;
    public Text gameOverText;
    public GameObject gmaeOverReplayButton;


    public void ViewCurrentHeight(float targetHeight)
    {
        #region �|�ˤ��J
            //viewCurrentHieghtText.GetComponent<Text>().text = $"�ثe�̰� : {Mathf.Round(targetHeight)}";
        #endregion

        viewCurrentHieghtText.GetComponent<Text>().text = $"�ثe�̰� : {targetHeight:F1}";
    }
    public void ViewGameOver(bool onOff, float targetHeight)
    {
        if (onOff)
        {
            gameOverText.gameObject.SetActive(true);
            gmaeOverReplayButton.SetActive(true);

            gameOverText.text = $"�̰�����:{targetHeight:F1}\n�C���׵�";
            if (gameOverText.fontSize <= 73) gameOverText.fontSize++;
        }
        else
        {
            gameOverText.gameObject.SetActive(false);
            gmaeOverReplayButton.gameObject.SetActive(false);
        }
    }
}
