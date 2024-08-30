using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : FlyMove
{
    public bool dead;
    public float Mujuktime = 3;
    public SpriteRenderer render;
    private AudioSource hitSound;
    public AudioClip explode;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        hitSound = GetComponent<AudioSource>();
    }

    public void mujuk() {

        dead = !dead;

        if(dead)
        {
            hitSound.PlayOneShot(explode);
            render.color = new Color(1, 1, 1, 0.3f);
            gameObject.layer = 6;
        }
        else
        {
            render.color = new Color(1, 1, 1, 1);
            gameObject.layer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            LifeCheck.Life--;

            mujuk();

            Invoke("mujuk", Mujuktime);

            Debug.Log(LifeCheck.Life);
        }
    }
}
