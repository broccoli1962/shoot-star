using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RankSystem : MonoBehaviour
{
    public RankList hi;
    public string PlayerName;
    public float score;

    public string url = "https://port-0-node-lycoe7dz7739380b.sel5.cloudtype.app/ranking";
    public string url2 = "https://port-0-node-lycoe7dz7739380b.sel5.cloudtype.app/data";

    private void Awake()
    {
        hi = GetComponent<RankList>();
    }

    public IEnumerator SendRankingData()
    {
        PlayerName = InputName.playerNameInput.text;
        score = ScoreCheck.score;

        List<IMultipartFormSection> form = new List<IMultipartFormSection> ();
        form.Add(new MultipartFormDataSection("playerName", PlayerName));
        form.Add(new MultipartFormDataSection("score", score.ToString()));

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        Debug.Log(PlayerName);
        Debug.Log(score);

        yield return request.SendWebRequest();

        string result = request.downloadHandler.text;
        Debug.Log(result);
        StartCoroutine(ReceiveRankingData());
        Debug.Log("코루틴2 실행");

        while (!request.isDone)
        {
            Debug.Log("Download progress: " + request.downloadProgress);
        }

        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("에러 발생 : " + request.error);
        }
        else
        {
            Debug.Log("랭킹 데이터 전송 완료");
        }
    }

    [System.Serializable]
    public class RankingData
    {
        public int u_no;
        public string u_name;
        public int u_score;
    }

    public RankingData[] top10RankingData;

    IEnumerator ReceiveRankingData()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();

        UnityWebRequest request = UnityWebRequest.Post(url2, form);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("에러 발생 :" + request.error);
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        top10RankingData = JsonConvert.DeserializeObject<RankingData[]>(result);

        foreach (RankingData data in top10RankingData)
        {
            Debug.Log($"u_no: {data.u_no}, u_name: {data.u_name}, u_score: {data.u_score}");
        }
        hi.UpdateRankingUI();
    }
}