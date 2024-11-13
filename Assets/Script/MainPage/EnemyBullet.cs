using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBullet : MonoBehaviour
{
    public float delay = 5f;
    public float bulletSpeed;
    public Vector3 targetPosition;
    public Vector3 direction;

    private void Start()
    {
        direction = (targetPosition - transform.position).normalized;
    }
    private void Update()
    {
         transform.position += direction * bulletSpeed * Time.deltaTime;
        if (transform.position.y < -34f || transform.position.y > 34f || transform.position.x > 30f || transform.position.x < -30f)
         {
             Destroy(gameObject);
         }
    }
}
