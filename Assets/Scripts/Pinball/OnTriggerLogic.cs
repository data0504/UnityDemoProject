using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoresInfo
{
    public string GreenStr = "GreenScore : ";
    public int GreenAddNumber = 10;
    public int GreenKeep = 0;

    public string PurpleStr = "PurpleScore : ";
    public  int PurpleAddNumber = 20;
    public int PurpleKeep = 0;

    public string TotalScoreStr = "TotalScore : ";
}

public class OnTriggerLogic : MonoBehaviour
{
    public Transform ScoreMonitor;
    public Text GreenScore;
    public Text PurpleScore;
    public Text TotalScore;

    private DisplayScoresInfo scoresInfo =new();
    private List<GameObject> scoreList =new();

    void Start()
    {
        foreach (Transform child in ScoreMonitor)
        {
            scoreList.Add(child.gameObject);
        }

        GreenScore.text = $"{scoresInfo.GreenStr}{scoresInfo.GreenKeep}";
        PurpleScore.text = $"{scoresInfo.PurpleStr}{scoresInfo.PurpleKeep}";
        TotalScore.text = $"{scoresInfo.TotalScoreStr}{GameScore.Score}";
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.name == scoreList[0].name || 
            collider.name == scoreList[1].name)
        {
            scoresInfo.GreenKeep += scoresInfo.GreenAddNumber;
            GreenScore.text = $"{scoresInfo.GreenStr}{scoresInfo.GreenKeep}";
        }
        else
        {
            scoresInfo.PurpleKeep += scoresInfo.PurpleAddNumber;
            PurpleScore.text = $"{scoresInfo.PurpleStr}{scoresInfo.PurpleKeep}";
        }

        TotalScore.text = $"{scoresInfo.TotalScoreStr}{GameScore.Score = scoresInfo.GreenKeep + scoresInfo.PurpleKeep}";
    }
}
