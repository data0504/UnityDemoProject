using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SitfListData
{
    public int spadesLimit = 12;
    public int redHeartLimit = 25;
    public int boxLimit = 38;
    public int parserLimit = 5;

    public  List<string> spadesList = new();
    public  List<string> RedHeartsList = new();
    public  List<string> BoxsList = new();
    public  List<string> plumFlowersList = new();
}
public class SmaeFlowerData
{
    public int Spades;
    public int RedHearts;
    public int Boxs;
    public int PlumFlowersk;

    public void Init() 
    {
        Spades = 5;
        RedHearts = 5;
        Boxs = 5;
        PlumFlowersk = 5;
    }
}
public class ShunZiData
{
    public int normalNumber;
    public int normalReslut;

    public bool royalCheck;
    public bool lastCheck;

    public int royalNumber;
    public int royalReslut;

    public int iterations;

    public void Init(List<int> sort)
    {
        normalNumber = sort[0];
        normalReslut = 0;

        royalCheck = false;
        lastCheck = false;

        royalNumber = sort[1];
        royalReslut = 0;

        iterations = 1;
    }

}
public class FourOfAkindData
{
    public List<int> cardGroupList;
    public int cardGroupANumber;
    public int sigleGroupNumber;

    public bool smallBig;


    public bool reslut;

    public void InitZero()
    {
        cardGroupANumber = 13;
        sigleGroupNumber = 4;
        reslut = false;

        cardGroupList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupList.Add(sigleGroupNumber);
        }
    }

    public void InitOne()
    {
        cardGroupANumber = 13;
        sigleGroupNumber = 4;
        reslut = false;

        cardGroupList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupList.Add(sigleGroupNumber);
        }
    }

    public void InitTwo()
    {
        reslut = false;
        smallBig = true;

    }
}
public class ThreeOfAkindData
{
    public List<int> cardGroupList;
    public int cardGroupANumber;
    public int sigleGroupNumber;

    public bool smallBig;


    public bool reslut;

    public void InitZero()
    {
        cardGroupANumber = 13;
        sigleGroupNumber = 3;
        reslut = false;

        cardGroupList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupList.Add(sigleGroupNumber);
        }
    }
}
public class OneTwoPairs
{
    public List<int> cardGroupList;
    public int cardGroupANumber;
    public int sigleGroupNumber;

    public int counting;

    public void InitOne()
    {
        cardGroupANumber = 13;
        sigleGroupNumber = 2;
        counting = 2;

        cardGroupList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupList.Add(sigleGroupNumber);
        }
    }
}
public class Gourd
{
    public List<int> cardGroupThreeOfAKindList;
    public List<int> cardGroupTwoPairsList;

    public int cardGroupANumber;
    public int sigleGroupPairsNumber;
    public int sigleGroupThreeOfAKindNumber;

    public int counting;
    public bool onOff;

    public void InitOne()
    {
        cardGroupANumber = 13;
        sigleGroupPairsNumber = 2;
        sigleGroupThreeOfAKindNumber = 3;
        counting = 2;
        onOff = true;

        cardGroupThreeOfAKindList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupThreeOfAKindList.Add(sigleGroupPairsNumber);
        }

        cardGroupTwoPairsList = new();
        for (int i = 0; i < cardGroupANumber; i++)
        {
            cardGroupTwoPairsList.Add(sigleGroupThreeOfAKindNumber);
        }
    }
}

public class HoldemInfo : MonoBehaviour
{
    public GameObject Objectprefab;
    public RectTransform loadGroups;
    public Sprite[] cardsListsPrefab;

    private Transform loadGroupsTransform;

    private readonly SitfListData sitfListData = new();
    private readonly SmaeFlowerData smaeFlower = new();
    private readonly ShunZiData shunZiData = new();
    private readonly ThreeOfAkindData threeOfAkindData = new();
    private readonly FourOfAkindData fourOfAkindData = new();
    private readonly OneTwoPairs onePairs = new();
    private readonly Gourd gourd = new();


    public void InitCardPrefab()
    {
        loadGroupsTransform = loadGroups.transform;
        IsObjectBuilder();
    }

    private void IsObjectBuilder()
    {
        for (int index = 0; index < cardsListsPrefab.Length; index++)
        {
            GameObject currentObject = CreateObject(); //創建物件

            AddImage(index, currentObject); //添加圖片
            UpdateName(currentObject); // 更改物件名稱
            SitfConditionList(index, currentObject); //同花物件過篩
        }
    }
    private GameObject CreateObject()
    {
        return GameObject.Instantiate(Objectprefab, loadGroups);
    }
    private void AddImage(int i, GameObject currentObject)
    {
        Image spriteRenderer = currentObject.GetComponent<Image>();
        spriteRenderer.sprite = cardsListsPrefab[i];
    }
    private void UpdateName(GameObject currentObject)
    {
        string currentObjectFileName = currentObject.GetComponent<Image>().sprite.texture.name;
        currentObject.name = $"{currentObjectFileName}";
    }
    private void SitfConditionList(int index, GameObject currentObject)
    {
        SameflowerUse(index, currentObject.name);
    }
    private void SameflowerUse(int index, string currnetObjectName)
    {
        if (index <= sitfListData.spadesLimit) sitfListData.spadesList.Add(currnetObjectName);
        else if (index > sitfListData.spadesLimit && index <= sitfListData.redHeartLimit) sitfListData.RedHeartsList.Add(currnetObjectName);
        else if (index > sitfListData.redHeartLimit && index <= sitfListData.boxLimit) sitfListData.BoxsList.Add(currnetObjectName);
        else sitfListData.plumFlowersList.Add(currnetObjectName);
    }

    public void RandomSort()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShuffleCards();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SitfSameFlower();
            ShunZi();

            FourOfAkindZero();
            //FourOfAkindOne();
            //FourOfAKindTwo();

            TheerOfAkindZero();

            Pair();

            Gourd();
        }
    }

    private void ShuffleCards()
    {
        for (int index = 0; index < loadGroupsTransform.childCount; index++)
        {
            int currentRandomNumber = UnityEngine.Random.Range(0, 52);
            GameObject currentGameObject = loadGroupsTransform.GetChild(index).gameObject;
            currentGameObject.transform.SetSiblingIndex(currentRandomNumber);
        }
    }
    private string GetObjectName(int index)
    {
        GameObject currentGameObject = loadGroupsTransform.GetChild(index).gameObject;
        return currentGameObject.transform.name;
    }
    private int Regular(string isGameObjectName)
    {
        string pattern = @"(\d+)$"; // 匹配以数字结尾的模式
        Match match = Regex.Match(isGameObjectName, pattern);
        string tail = match.Groups[1].Value;
        int lastNumber = Convert.ToInt32(tail);
        return lastNumber;
    }

    private void SitfSameFlower()
    {
        smaeFlower.Init();

        for (int index = 0; index < sitfListData.parserLimit; index++)
        {
            string currentGameObjectName = GetObjectName(index);

            if (sitfListData.spadesList.Contains(currentGameObjectName)) smaeFlower.Spades -= 1;
            if (sitfListData.RedHeartsList.Contains(currentGameObjectName)) smaeFlower.RedHearts -= 1;
            if (sitfListData.BoxsList.Contains(currentGameObjectName)) smaeFlower.Boxs -= 1;
            if (sitfListData.plumFlowersList.Contains(currentGameObjectName)) smaeFlower.PlumFlowersk -= 1;
        }

        ResultSameFlower();
    }
    private void ResultSameFlower()
    {
        if (smaeFlower.Spades == 0) Debug.Log($"黑桃同花");
        if (smaeFlower.RedHearts == 0) Debug.Log($"紅心同花");
        if (smaeFlower.Boxs == 0) Debug.Log($"方塊同花");
        if (smaeFlower.PlumFlowersk == 0) Debug.Log($"梅花同花");
    }

    private void ShunZi()
    {
        List<int> sort = Sort(0);
        Parser(sort);
    }
    private List<int> Sort(int v)
    {
        List<int> notSort = UnSort();
        if (v == 0)
        {
            return SmallSorted(notSort);
        }
        if (v == 1)
        {
            return BigSorted(notSort);
        }
        throw new NotImplementedException();
    }
    private List<int> UnSort()
    {
        List<int> notSort = new();
        for (int index = 0; index < sitfListData.parserLimit; index++)
        {
            string currentGameObjectName = GetObjectName(index);
            int currentCardNumber = Regular(currentGameObjectName);
            notSort.Add(currentCardNumber);
        }
        return notSort;
    }
    private List<int> SmallSorted(List<int> notSort)
    {
        List<int> smallSort = new();
        for (int index = 0; index < notSort.Count; index++)
        {
            bool inserted = false;
            for (int _index = 0; _index < smallSort.Count; _index++)
            {
                if (smallSort[_index] >= notSort[index])
                {
                    smallSort.Insert(_index, notSort[index]);
                    inserted = true;
                    break;
                }
            }
            if (!inserted)
            {
                smallSort.Add(notSort[index]);
            }
        }
        return smallSort;
    }
    private List<int> BigSorted(List<int> notSort)
    {
        List<int> bigSort = new();
        for (int index = 0; index < notSort.Count; index++)
        {
            bool inserted = false;
            for (int _index = 0; _index < bigSort.Count; _index++)
            {
                if (bigSort[_index] <= notSort[index])
                {
                    bigSort.Insert(_index, notSort[index]);
                    inserted = true;
                    break;
                }
            }
            if (!inserted)
            {
                bigSort.Add(notSort[index]);
            }
        }
        return bigSort;
    }
    private void Parser(List<int> sort)
    {
        shunZiData.Init(sort);
        for (int index = 0; index < sort.Count; index++)
        {
            SpecialShunZi(sort, index);

            NormalShunZi(sort, index);
        }
        ReslutShunZi(sort);
    }
    private void NormalShunZi(List<int> sort, int index)
    {
        if (sort[index] == shunZiData.normalNumber)
        {
            shunZiData.normalNumber += shunZiData.iterations;
            shunZiData.normalReslut += shunZiData.iterations;
            shunZiData.royalCheck = true;
        }
    }
    private void SpecialShunZi(List<int> sort, int index)
    {
        if (shunZiData.royalCheck)
        {
            if (sort[index] == shunZiData.royalNumber)
            {
                shunZiData.royalNumber += shunZiData.iterations;
                shunZiData.royalReslut += shunZiData.iterations;
            }

            if (shunZiData.royalReslut == (sort.Count - shunZiData.iterations))
            {
                int firstIndex = shunZiData.royalReslut - (sort.Count - shunZiData.iterations);
                // pending 寫死
                if (sort[firstIndex] == shunZiData.iterations && sort[4]==13)
                {
                    shunZiData.royalReslut += shunZiData.iterations;
                }
            }
        }
    }
    private void ReslutShunZi(List<int> sort)
    {
        if (shunZiData.royalReslut == sort.Count)
        {
            Debug.Log("特殊順子");
        }
        else if (shunZiData.normalReslut == sort.Count)
        {
            Debug.Log("順子");
        }
    }


    private void ReslutFourOfAKind()
    {
        if (fourOfAkindData.reslut)
        {
            Debug.Log("鐵支,四條");
        }
    }
    private void FourOfAkindZero()
    {
        fourOfAkindData.InitZero();
        List<int> currentCardList = UnSort();

        FourOfAKindZeroAlign(currentCardList);

        FourOfAKindZeroCheck();

        ReslutFourOfAKind();
    }
    private void FourOfAKindZeroAlign(List<int> currentCardList)
    {
        for (int i = 0; i < currentCardList.Count; i++)
        {
            int cardsTypeListIndex = currentCardList[i] - 1;
            fourOfAkindData.cardGroupList[cardsTypeListIndex]--;
        }
    }
    private void FourOfAKindZeroCheck()
    {
        for (int i = 0; i < fourOfAkindData.cardGroupList.Count; i++)
        {
            if (fourOfAkindData.cardGroupList[i] == 0)
            {
                fourOfAkindData.reslut = true;
            }
        }
    }

    private void FourOfAkindOne()
    {
        fourOfAkindData.InitOne();

        List<int> unSort = UnSort();

        FourOfAKindCount(unSort);
        FourOfAKindParser();

        ReslutFourOfAKind();
    }
    private void FourOfAKindCount(List<int> unSort)
    {
        for (int index = 0; index < fourOfAkindData.cardGroupList.Count; index++)
        {
            int cuurentCard = index + 1;

            if (cuurentCard == unSort[0]) fourOfAkindData.cardGroupList[index] -= 1;
            if (cuurentCard == unSort[1]) fourOfAkindData.cardGroupList[index] -= 1;
            if (cuurentCard == unSort[2]) fourOfAkindData.cardGroupList[index] -= 1;
            if (cuurentCard == unSort[3]) fourOfAkindData.cardGroupList[index] -= 1;
            if (cuurentCard == unSort[4]) fourOfAkindData.cardGroupList[index] -= 1;
        }
    }
    private void FourOfAKindParser()
    {
        for (int index = 0; index < fourOfAkindData.cardGroupList.Count; index++)
        {
            if (fourOfAkindData.cardGroupList[index] == 0)
            {
                fourOfAkindData.reslut = true;
                break;
            }
        }
    }

    private void FourOfAKindTwo()
    {
        fourOfAkindData.InitTwo();

        FourOfAKindSmallSort();
        FourOfAKindBigSort();

        ReslutFourOfAKind();
    }
    private void FourOfAKindSmallSort()
    {
        List<int> samllSort = Sort(0);

        int firstCard = samllSort[0];
        bool correct =
            (firstCard == samllSort[1]) && (firstCard == samllSort[2]) && (firstCard == samllSort[3]);

        if (correct) fourOfAkindData.reslut = true;
    }
    private void FourOfAKindBigSort()
    {

        List<int> bigSort = Sort(1);

        int firstCard = bigSort[0];
        bool correct =
            (firstCard == bigSort[1]) && (firstCard == bigSort[2]) && (firstCard == bigSort[3]);

        if (correct) fourOfAkindData.reslut = true;

    }



    private void ReslutThreeOfAKind()
    {
        if (threeOfAkindData.reslut)
        {
            Debug.Log("三條");
        }
    }
    private void TheerOfAkindZero()
    {
        threeOfAkindData.InitZero();
        List<int> unSort = UnSort();

        ThreeOfAKindAlign(unSort);

        ThreeOfAKindCheck();

        ReslutThreeOfAKind();
    }
    private void ThreeOfAKindAlign(List<int> unSort)
    {
        for (int i = 0; i < unSort.Count; i++)
        {
            int cardGroupANumberIndex = unSort[i] - 1;
            threeOfAkindData.cardGroupList[cardGroupANumberIndex]--;
        }
    }
    private void ThreeOfAKindCheck()
    {
        for (int i = 0; i < threeOfAkindData.cardGroupList.Count; i++)
        {
            if (threeOfAkindData.cardGroupList[i] == 0)
            {
                threeOfAkindData.reslut = true;
            }
        }
    }


    private void ReslutPair()
    {
        if (onePairs.counting == 1)
        {
            Debug.Log("一對");
        }
        if (onePairs.counting == 0)
        {
            Debug.Log("兩對");
        }
    }
    private void Pair()
    {
        onePairs.InitOne();
        List<int> unSort = UnSort();

        for (int i = 0; i < unSort.Count; i++)
        {
            int cardGroupListIndex = unSort[i] - 1;
            onePairs.cardGroupList[cardGroupListIndex]--;
        }

        for (int i = 0; i < onePairs.cardGroupList.Count; i++)
        {
            if (onePairs.cardGroupList[i] == 0)
            {
                onePairs.counting -- ;
            }
        }

        ReslutPair();
    }

    private void ReslutGourd()
    {
        if (gourd.counting == 0)
        {
            Debug.Log("葫蘆");
        }
    }
    private void Gourd()
    {
        gourd.InitOne();
        List<int> unSort = UnSort();

        for (int i = 0; i < unSort.Count; i++)
        {
            int cardGroupThreeOfAKindListIndex = unSort[i] - 1;
            gourd.cardGroupThreeOfAKindList[cardGroupThreeOfAKindListIndex]--;

            int cardGroupTwoPairsListIndex = unSort[i] - 1;
            gourd.cardGroupTwoPairsList[cardGroupTwoPairsListIndex]--;
        }

        for (int i = 0; i < gourd.cardGroupANumber; i++)
        {
            if (gourd.cardGroupThreeOfAKindList[i] == 0 && gourd.onOff)
            {
                gourd.counting--;
                gourd.onOff = false;
            }
            if (gourd.cardGroupTwoPairsList[i] == 0)
            {
                gourd.counting--;
            }
        }
        ReslutGourd();
    }
}
