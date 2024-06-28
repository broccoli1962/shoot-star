using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    public void BulletMove()
    {
        transform.position += new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        if(transform.position.y > 8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Egg")
        {
            Destroy(collision.gameObject);
            ScoreCheck.score += 100;
        }
    }
}
