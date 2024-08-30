using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public GameObject calc;
    public TextMeshProUGUI uploadText;

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
        TextMeshProUGUI text = uploadText.GetComponent<TextMeshProUGUI>();
        Debug.Log(text.text.Length);
        if(text.text.Length > 1)
        {
            StartCoroutine(calc.GetComponent<RankSystem>().SendRankingData());
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("한글자 이상 입력 필요");
        }
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
