using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource hitSound;
    public AudioClip explode;
    // Start is called before the first frame update
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }
}
