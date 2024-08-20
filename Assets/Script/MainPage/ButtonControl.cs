using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject calc;

    public void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LifeCheck.Life = 3;
        ScoreCheck.score = 0;
    }

    public void UploadBtn()
    {
        StartCoroutine(calc.GetComponent<RankSystem>().SendRankingData());
        
    }


    public void EndBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
