using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20.0f;
    public int bulletDamage = 20;
    void Update()
    {
        BulletMove();
    }

    public void BulletMove()
    {
        transform.position += new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        if(transform.position.y > 31f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            Enemy monster = collision.gameObject.GetComponent<Enemy>();
            monster.health -= bulletDamage;

            if (monster.health <= 0)
            {
                Destroy(collision.gameObject);
                ScoreCheck.score += 100;
            }
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("BossEgg"))
        {
            //Boss boss = collision.gameObject.GetComponent<Boss>();
            BossPattern boss = collision.gameObject.GetComponent<BossPattern>();
            boss.health -= bulletDamage;

            if(boss.health <= 0)
            {
                Destroy(collision.gameObject);
                ScoreCheck.score += 1000;
            }
            Destroy(gameObject);
        }
    }
}
