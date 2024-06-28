using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCheck : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static float score = 0;

    void Start()
    {
        SetText();
    }
    private void FixedUpdate()
    {
        SetText();
        score += 100*Time.deltaTime;
    }
    public void SetText()
    {
        text.text = score.ToString();
    }
}