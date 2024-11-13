using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public GameObject enemyBullet;
    public float bossSpeed;
    public int health;
    private string[] attackPattern;
    Transform player;

    private int tick;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    
    public void SetStats(int health, float speed, string[] attackPattern)
    {
        this.health = health;
        this.bossSpeed = speed;
        this.attackPattern = attackPattern;
    }

    private IEnumerator Pattern1()
    {
        float moveDistance = 40f;
        float fisrtmoveDistance = moveDistance/2;
        float moveSpeed = bossSpeed;
        bool firstMoving = true;
        bool afterMoving = false;
        int PowerShotCount = 0;

        while (health > 0)
        {
            if (firstMoving)
            {
                yield return StartCoroutine(MoveToPosition(transform.position - new Vector3(0, 15, 0), moveSpeed));
                yield return StartCoroutine(MoveToPosition(transform.position + new Vector3(fisrtmoveDistance, 0, 0), moveSpeed));
                firstMoving = false;
            }
            if (afterMoving)
            {
                yield return StartCoroutine(MoveToPosition(transform.position + new Vector3(fisrtmoveDistance, 0, 0), moveSpeed));
                afterMoving = false;
            }

            yield return LeftMove(moveDistance, moveSpeed, 0);
            yield return NormalShoot();
            yield return RightMove(moveDistance, moveSpeed, 0);
            yield return NormalShoot();
            yield return LeftMove(moveDistance, moveSpeed, 10);
            yield return NormalShoot();
            yield return RightMove(moveDistance, moveSpeed, 0);
            yield return NormalShoot();
            yield return LeftMove(moveDistance, moveSpeed, -10);
            yield return NormalShoot();
            yield return RightMove(moveDistance, moveSpeed, 0);
            yield return NormalShoot();
            PowerShotCount++;

            if (PowerShotCount == 2)
            {
                yield return StartCoroutine(MoveToPosition(transform.position - new Vector3(fisrtmoveDistance, 0, 0), moveSpeed));
                yield return StartCoroutine(ShootBullet(player.transform.position, 80, 2, 0.2f));
                yield return ShotGun(player.transform.position, 40, 3);
                yield return StartCoroutine(ShootBullet(player.transform.position, 40, 4, 0.1f));
                yield return StartCoroutine(ShootBullet(player.transform.position, 40, 2, 0.2f));
                yield return StartCoroutine(ShootBullet(player.transform.position, 80, 8, 0.1f));
                yield return ShotGun(player.transform.position, 40, 5);
                yield return StartCoroutine(ShootBullet(player.transform.position, 40, 4, 0.2f));
                yield return StartCoroutine(ShootBullet(player.transform.position, 80, 8, 0.1f));
                yield return StartCoroutine(ShootBullet(player.transform.position, 40, 2, 0.2f));
                yield return ShotGun(player.transform.position, 40, 3);
                yield return StartCoroutine(ShootBullet(player.transform.position, 80, 4, 0.05f));
                yield return StartCoroutine(ShootBullet(player.transform.position, 100, 1, 0.01f));
                PowerShotCount = 0;
                afterMoving = true;
            }
        }
    }

    private IEnumerator LeftMove(float distance, float speed, float height)
    {
        yield return StartCoroutine(MoveToPosition(transform.position - new Vector3(distance, height, 0), speed));
    }

    private IEnumerator RightMove(float distance, float speed, float height)
    {
        yield return StartCoroutine(MoveToPosition(transform.position + new Vector3(distance, height, 0), speed));
    }

    private IEnumerator NormalShoot()
    {
        yield return StartCoroutine(ShootBullet(player.transform.position, 40, 2, 0.2f));
        yield return StartCoroutine(ShootBullet(player.transform.position, 80, 4, 0.1f));
        yield return StartCoroutine(ShootBullet(player.transform.position, 40, 2, 0.2f));
    }

    private IEnumerator Pattern2()
    {
        while (health > 0)
        {
            yield return NormalShoot();
            yield return null;
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while(transform.position!= target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ShootBullet(Vector3 target, float speed,int bulletCount,float delay)
    {
        for(int i = 0; i < bulletCount; i++)
        {
             GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

             EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
             if (bulletScript != null)
             {
                    bulletScript.targetPosition = target;
                    bulletScript.bulletSpeed = speed;
             }
             yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator ShotGun(Vector3 target, float speed, int bulletCount)
    {
        for(int i = -bulletCount; i<=bulletCount; i++)
        {
            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.targetPosition = new Vector3(target.x + i*3, target.y, target.z);
                bulletScript.bulletSpeed = speed;
            }
        }
        yield return null;
    }
}
