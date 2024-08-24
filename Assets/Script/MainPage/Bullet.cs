using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20.0f;
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
        if (collision.gameObject.tag == "Egg")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            ScoreCheck.score += 100;
        }
    }
}
