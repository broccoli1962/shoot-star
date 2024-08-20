using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputName : MonoBehaviour
{
    public static TMP_InputField playerNameInput;

    public void Start()
    {
        playerNameInput = GetComponent<TMP_InputField>();
    }
}