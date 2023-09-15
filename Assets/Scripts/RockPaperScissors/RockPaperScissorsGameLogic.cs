using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;
using System.Linq;

[Serializable]
public class TextAll
{
    public Text TextRock;
    public Text TextPaper;
    public Text TextScisson;
    public Text TextElenent;
}
[Serializable]
public class ButtonAll
{
    public Button ButtonRock;
    public Button ButtonPaper;
    public Button ButtonScissors;
}

[Serializable]
public class PercentageTextAll
{
    public Text OrderRock;
    public Text PercentageRock;

    public Text OrderPaper;
    public Text PercentagePaper;

    public Text OrderScissors;
    public Text PercentageScisson;

    public Text Orderall;
    public Text PercentageElenent;
}

public enum GameSort 
    {
        paper,
        rock,
        scissors
    }

public class RockPaperScissorsGameLogic : MonoBehaviour
{
    public TextAll TextAll;
    public ButtonAll ButtonAll;
    public PercentageTextAll PercentageTextAll;
    private int lose = 90, win = 9, tie = 1, weights; 

    private int  orderPaper, orderRock, orderScissors, orderTotal; 
    private string percentParper, percentRock,  percentScissors, percentTotal;
    List<List<int>> orderTotalList = new List<List<int>>
    {
        new List<int> { 0, 0, 0 },
        new List<int> { 0, 0, 0 },
        new List<int> { 0, 0, 0 },
    };
    private string loseText = "Lose", winText = "Win", tieText = "Tie", resultText;

    void Start()
    {
        TextALL();
        ButtonAll.ButtonRock.onClick.AddListener(() => GroupFight((int)GameSort.rock));
        ButtonAll.ButtonPaper.onClick.AddListener(() => GroupFight((int)GameSort.paper));
        ButtonAll.ButtonScissors.onClick.AddListener(() => GroupFight((int)GameSort.scissors));
    }
    public void GroupFight(int user)
    {
        Parser(AiRondom(), user);
        Percentage(user);
        Print();
    }

    private void Parser(int decide, int user)
    {
        resultText = "";
        int level = lose;
        if (decide < level)
        {
            resultText += $"{loseText},Robot is";
            Reason(user, lose);
            return;
        }
        level += win;
        if (decide < level)
        {
            resultText += $"{winText},Robot is";
            Reason(user, win);
            return;
        }
        level += tie;
        if (decide < level)
        {
            resultText += $"{tieText},Robot is";
            Reason(user, tie);
            return;
        }
    }
    private void Reason(int user, int level)
    {
        List<string> prsStr = new List<string> { "Scissors", "Paper", "Rock" };

        if (level == lose)
        {
            for (int i = 0; i < prsStr.Count; i++)
            {
                if (user == i)
                {
                    if(i == 0)
                    {
                        orderTotalList[i][i]++;
                    }
                    if (i == 1)
                    {
                        orderTotalList[i][i-1]++;
                    }
                    if (i == 2)
                    {
                        orderTotalList[i][i-2]++;
                    }
                    resultText += $"{ prsStr[i]}!";
                    return;
                }

            }
        }

        prsStr = new List<string> { "Rock", "Scissors", "Paper" };
        if (level == win)
        {
            for (int i = 0; i < prsStr.Count; i++)
            {
                if (user == i)
                {
                    if (i == 0)
                    { 
                        orderTotalList[i][i+1]++;
                    }
                    if (i == 1)
                    {
                        orderTotalList[i][i]++;
                    }
                    if (i == 2)
                    {
                        orderTotalList[i][i-1]++;
                    }
                    resultText += $"{ prsStr[i]}!";
                    return;
                }
            }
        }

        prsStr = new List<string> { "Paper", "Rock", "Scissors" };
        if (level == tie)
        {
            for (int i = 0; i < prsStr.Count; i++)
            {
                if (user == i)
                {
                    if (i == 0)
                    {
                        orderTotalList[i][i + 2]++;
                    }
                    if (i == 1)
                    {
                        orderTotalList[i][i+1]++;
                    }
                    if (i == 2)
                    {
                        orderTotalList[i][i]++;
                    }
                    resultText += $"{ prsStr[i]}!";
                    return;
                }
            }
        }
        
    }
    private void Percentage(int user)
    {
        orderTotal = 0;
        if (user == 0)
        {
            orderPaper = orderTotalList[0].Sum();
            List<int> list = orderTotalList[0];
            (double, double, double) _percentParper = Count(list, orderPaper);
            percentParper = $"Lose:{_percentParper.Item1}%, Win:{_percentParper.Item2}%, Tie:{_percentParper.Item3}%";

        }
        if (user == 1)
        {
            orderRock = orderTotalList[1].Sum();
            List<int> list = orderTotalList[1];
            (double, double, double) _percentRock = Count(list, orderRock);
            percentRock = $"Lose:{_percentRock.Item1}%, Win:{_percentRock.Item2}%, Tie:{_percentRock.Item3}%";
        }
        if (user == 2)
        {
            orderScissors = orderTotalList[2].Sum();
            List<int> list = orderTotalList[2];
            (double, double, double) _percentScissors = Count(list, orderScissors);
            percentScissors = $"Lose:{_percentScissors.Item1}%, Win:{_percentScissors.Item2}%, Tie:{_percentScissors.Item3}%";
        }
        orderTotal = orderRock + orderPaper + orderScissors;
        (double, double, double) _percentTotal = CountAll(orderTotalList, orderTotal);
        percentTotal = $"Lose:{_percentTotal.Item1}%, Win:{_percentTotal.Item2}%, Tie:{_percentTotal.Item3}%";
    }

    private (double, double, double) Count(List<int> list, int order)
    {
        double losePercent = Math.Round(((float)(list[0]) / order) * 100, 1);
        double winPercent = Math.Round(((float)(list[1]) / order) * 100, 1);
        double tiePercent = Math.Round(((float)(list[2]) / order) * 100, 1);
        return (losePercent, winPercent, tiePercent);
    }
    private (double, double, double) CountAll(List<List<int>> list, int order)
    {
        int loseNumber=0, winNumber=0, tieNumber=0;

        for (int i = 0; i < list.Count; i++)
        {
            int singleListIndex = list[i].Count;
            for (int j = 0; j < singleListIndex; j++)
            {
                if (j == 0)
                {
                    loseNumber += list[i][j];
                }
                if (j == 1)
                {
                    winNumber += list[i][j];
                }
                if (j == 2)
                {
                    tieNumber += list[i][j];
                }
            }
        }
        double losePercent = Math.Round(((float)loseNumber / order) * 100, 1);
        double winPercent = Math.Round(((float)winNumber / order) * 100, 1);
        double tiePercent = Math.Round(((float)tieNumber / order) * 100, 1);
        return (losePercent, winPercent, tiePercent);
    }

    private async void Print()
    {
        TextAll.TextElenent.text = "Showdown";
        await Task.Delay(500); //Wait 0.5 second

        TextAll.TextElenent.text = resultText;

        PercentageTextAll.OrderRock.text = $"{TextAll.TextRock.text}Number : {orderRock}";
        PercentageTextAll.OrderPaper.text = $"{TextAll.TextPaper.text}Number : {orderPaper}";
        PercentageTextAll.OrderScissors.text = $"{TextAll.TextScisson.text}Number : {orderScissors}";
        PercentageTextAll.Orderall.text = $"TotalClickNumber : {orderTotal}";

        PercentageTextAll.PercentagePaper.text = percentParper;
        PercentageTextAll.PercentageRock.text = percentRock;
        PercentageTextAll.PercentageScisson.text = percentScissors;
        PercentageTextAll.PercentageElenent.text = percentTotal;
    }
    private  int  AiRondom()
    {
        weights = lose + win + tie;
        return  Random.Range(0, weights);
    }
    private void TextALL()
    {
        TextAll.TextRock.text = "Rock";
        TextAll.TextPaper.text = "Paper";
        TextAll.TextScisson.text = "Scissors";
        TextAll.TextElenent.text = "SelectWeaponry";

        PercentageTextAll.OrderRock.text = $"{TextAll.TextRock.text}Number : ";
        PercentageTextAll.OrderPaper.text = $"{TextAll.TextPaper.text}Number : ";
        PercentageTextAll.OrderScissors.text = $"{TextAll.TextScisson.text}Number : ";
        PercentageTextAll.Orderall.text = $"TotalClickNumber : ";

        string resultStr = "Lose:%, Win:%, Tie:%";
        PercentageTextAll.PercentageRock.text = $"{resultStr}";
        PercentageTextAll.PercentagePaper.text = $"{resultStr}";
        PercentageTextAll.PercentageScisson.text = $"{resultStr}";
        PercentageTextAll.PercentageElenent.text = $"{resultStr}";
    }
}
