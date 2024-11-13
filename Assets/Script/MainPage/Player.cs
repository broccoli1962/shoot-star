using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : FlyMove
{
    public bool dead;
    public float Mujuktime = 3;
    public SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(LifeCheck.Life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void mujuk() {

        dead = !dead;

        if(dead)
        {
            sound.HitSound();
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
        if (dead) {
            return;
        }

        if (collision.gameObject.CompareTag("Egg") || collision.gameObject.CompareTag("BossEgg") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            mujuk();
            LifeCheck.Life--;
            Invoke("mujuk", Mujuktime);
            Debug.Log(LifeCheck.Life);
        }
    }
}
