using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static bool state;
    public GameObject Target;

    private void Start()
    {
        state = false;
    }

    private void Update()
    {
        if (state == false)
        {
            Target.SetActive(false);
        }
        else if(state == true)
        {
            Target.SetActive(true);
        }
    }
}
