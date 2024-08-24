using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        Vector3 move = Vector3.zero;
        move = new Vector3(0, -10, 0);
        transform.position += move * enemySpeed * Time.deltaTime;

        if(transform.position.y < -50f)
        {
            Destroy(gameObject);
        }

    }
}
