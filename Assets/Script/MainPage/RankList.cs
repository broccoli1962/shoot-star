using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankList : MonoBehaviour
{
    public Transform RankListContent;
    public GameObject RankListPrefab;
    public RankSystem.RankingData[] rank;

    public void UpdateRankingUI()
    {
        rank = GetComponent<RankSystem>().top10RankingData;
        for (int i = 0; i<10; i++)
        {
            GameObject items = Instantiate(RankListPrefab, RankListContent);

            TextMeshProUGUI rankText = items.transform.Find("rankText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = items.transform.Find("nameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = items.transform.Find("scoreText").GetComponent<TextMeshProUGUI>();

            rankText.text = (i + 1).ToString();
            nameText.text = rank[i].u_name;
            scoreText.text = rank[i].u_score.ToString();
        }
    }
}
