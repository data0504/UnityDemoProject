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
        #region 四捨五入
            //viewCurrentHieghtText.GetComponent<Text>().text = $"目前最高 : {Mathf.Round(targetHeight)}";
        #endregion

        viewCurrentHieghtText.GetComponent<Text>().text = $"目前最高 : {targetHeight:F1}";
    }
    public void ViewGameOver(bool onOff, float targetHeight)
    {
        if (onOff)
        {
            gameOverText.gameObject.SetActive(true);
            gmaeOverReplayButton.SetActive(true);

            gameOverText.text = $"最高紀錄:{targetHeight:F1}\n遊戲終結";
            if (gameOverText.fontSize <= 73) gameOverText.fontSize++;
        }
        else
        {
            gameOverText.gameObject.SetActive(false);
            gmaeOverReplayButton.gameObject.SetActive(false);
        }
    }
}
