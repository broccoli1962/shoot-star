using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCheck : MonoBehaviour
{
    public static int Life = 3;
    public RawImage life1;
    public RawImage life2;
    public RawImage life3;
    private void FixedUpdate()
    {
        if(Life == 2)
        {
            life1.gameObject.SetActive(false);
        }
        if(Life == 1)
        {
            life2.gameObject.SetActive(false);
        }
        if(Life <= 0)
        {
            life3.gameObject.SetActive(false);
            SystemManager.state = true;
            //GameEnd();
        }
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
