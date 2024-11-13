using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossPattern;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBullet;
    [SerializeField]
    private float enemySpeed;
    public int health;
    private string enemyType;

    Transform player;
    List<Ticking> patterns = new List<Ticking>();
    List<Ticking[]> nextPattern = new List<Ticking[]>();

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        EnemyMove();
        List<Ticking> endPattern = new List<Ticking>();
        foreach (Ticking move in patterns)
        {
            if (move.tick())
            {
                endPattern.Add(move);
            }
        }
        endPattern.ForEach(pattern => patterns.Remove(pattern));

        if (patterns.Count == 0)
        {
            if (nextPattern.Count == 0)
            {
                if (enemyType == "Normal")
                {
                    NormalPattern();
                }else if(enemyType == "Strong")
                {
                    StrongPattern();
                }
                else
                {
                    return;
                }
            }
            foreach (var item in nextPattern[0])
            {
                patterns.Add(item);
                item.firstTick();
            }
            nextPattern.RemoveAt(0);
        }
    }

    private void NormalPattern()
    {
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 60, 1, 80, enemyBullet),
        });
    }

    private void StrongPattern()
    {
        nextPattern.Add(new Ticking[] {
            new RoundShoot(this.transform, 40, 6, 65, enemyBullet)});
    }

    public interface Ticking
    {
        public bool tick();
        public void firstTick() { }
    }

    public class RoundShoot : Ticking
    {
        Transform transform;
        float speed;
        int bulletCount;
        int delay;
        GameObject enemyBullet;
        public RoundShoot(Transform transform, float speed, int bulletCount, int delay, GameObject enemyBullet)
        {
            this.speed = speed;
            this.bulletCount = bulletCount;
            this.delay = delay;
            this.enemyBullet = enemyBullet;
            this.transform = transform;
        }

        int tok;

        public bool tick()
        {
            if (++tok == delay)
            {
                float angleStep = 360f / bulletCount;
                for (int i = 0; i < bulletCount; i++)
                {
                    float angle = angleStep * i * Mathf.Deg2Rad;
                    Vector3 direct = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

                    GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

                    EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.targetPosition = transform.position + direct;
                        bulletScript.bulletSpeed = speed;
                    }
                }
                return true;
            }
            return false;
        }
    }

    public class ShootBullet : Ticking
    {
        Transform target;
        float speed;
        int bulletCount;
        int delay;
        GameObject enemyBullet;
        Transform transform;
        public ShootBullet(Transform transform, Transform target, float speed, int bulletCount, int delay, GameObject enemyBullet)
        {
            this.target = target;
            this.speed = speed;
            this.bulletCount = bulletCount;
            this.delay = delay;
            this.enemyBullet = enemyBullet;
            this.transform = transform;
        }

        int tok;

        public bool tick()
        {
            if (++tok == delay)
            {
                tok = 0;
                GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

                EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
                if (bulletScript != null)
                {
                    bulletScript.targetPosition = target.position;
                    bulletScript.bulletSpeed = speed;
                }
                if (--bulletCount == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }

    private void EnemyMove()
    {
        Vector3 move = Vector3.zero;
        move = new Vector3(0, -10, 0);
        transform.position += move * enemySpeed * Time.deltaTime;

        if(transform.position.y < -50f)
        {
            Destroy(gameObject);
        }

    }

    public void SetStats(int health, float speed, string enemyType)
    {
        this.health = health;
        this.enemySpeed = speed;
        this.enemyType = enemyType;
    }
}
