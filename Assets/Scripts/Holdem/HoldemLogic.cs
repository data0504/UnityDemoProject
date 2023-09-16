using UnityEngine;

public class HoldemLogic : MonoBehaviour
{
    public HoldemInfo  pokerShuffleAnalyzeInfo;
    void Start()
    {
        pokerShuffleAnalyzeInfo.InitCardPrefab();
    }

    void Update()
    {
        pokerShuffleAnalyzeInfo.RandomSort();
    }
}
