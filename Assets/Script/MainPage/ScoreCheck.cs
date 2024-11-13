using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCheck : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI GameEndTextScore;
    public static float score = 0;

    void Start()
    {
        SetText();
    }
    private void FixedUpdate()
    {
        if(!SystemManager.state)
        {
            score += 100 * Time.deltaTime;
            SetText();
        }
    }
    public void SetText()
    {
        text.text = score.ToString("F0");
        GameEndTextScore.text = score.ToString("F0");
    }
}