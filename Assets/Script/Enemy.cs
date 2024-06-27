using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        Vector3 move = Vector3.zero;
        move = new Vector3(0, -3, 0);
        transform.position += move * enemySpeed * Time.deltaTime;

        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }

    }
}
