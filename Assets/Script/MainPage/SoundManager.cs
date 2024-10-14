using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioTrack;
    public AudioClip explode;
    public AudioClip shoot;
    // Start is called before the first frame update
    void Start()
    {
        audioTrack = GetComponent<AudioSource>();
    }
    public void HitSound()
    {
        audioTrack.PlayOneShot(explode);
    }
    public void ShootSound()
    {
        audioTrack.PlayOneShot(shoot);
    }
}
