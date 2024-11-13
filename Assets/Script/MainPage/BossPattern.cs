using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject enemyBullet;

    [SerializeField]
    private float bossSpeed;
    public int health;
    private string attackPattern;
    
    Transform player;
    List<Ticking> patterns = new List<Ticking>();
    List<Ticking[]> nextPattern = new List<Ticking[]>();

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        patterns.Add(new MoveToPosition(this.transform, new Vector3(0, 15, 0), 20, true));
    }

    private void FixedUpdate()
    {
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
            if(nextPattern.Count == 0)
            {
                float rand = Random.value;
                //패턴 반복
                if(attackPattern == "Pattern1")
                {
                    if (rand <= 0.4)
                    {
                        Pattern3();
                    }
                    else
                    {
                        Pattern1();
                    }
                }
                else if(attackPattern == "Pattern2")
                {
                    if (rand <= 0.2)
                    {
                        Pattern2();
                    }
                    else if(rand <= 0.6)
                    {
                        Pattern4();
                    }
                    else
                    {
                        Pattern1();
                    }
                }else if(attackPattern == "Pattern3")
                {
                    if (rand <= 0.2)
                    {
                        Pattern2();
                    }
                    else if(rand <= 0.6)
                    {
                        Pattern5();
                    }else if(rand <= 0.7)
                    {
                        Pattern3();
                    }
                    else
                    {
                        Pattern4();
                    }
                }
                return;
            }
            foreach (var item in nextPattern[0])
            {
                patterns.Add(item);
                item.firstTick();
            }
            nextPattern.RemoveAt(0);
        }
    }

    public void SetStats(int health, float speed, string attackPattern)
    {
        this.health = health;
        this.bossSpeed = speed;
        this.attackPattern = attackPattern;
    }

    public void Pattern1()
    {
        //왼쪽 이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(-20, 0, 0), 20, true),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 100, enemyBullet),
        });
        //오른쪽 위 대각선
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(40, -10, 0), 20, true),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 100, enemyBullet),
        });
        //왼쪽 이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(-40, 0, 0), 20, true),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 100, enemyBullet),
        });
        //오른쪽 아래 대각선
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(40, 10, 0), 20, true),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 100, enemyBullet),
        });
        //중앙이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, this.transform.parent.position - new Vector3(0,15,0), 20, false),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 100, enemyBullet),
        });
    }

    public void Pattern2()
    {
        
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 80, 20, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 100, 5, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 3, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 80, 20, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 120, 5, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 3, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 80, 20, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 2, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] {
            new ShootBullet(this.transform,player, 150, 5, 5, enemyBullet),
            new ShotgunBullet(this.transform,player, 40, 3, 50, enemyBullet),
        });
    }

    public void Pattern3()
    {
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, player.position, 60, false),
            new RoundShoot(this.transform, 40, 16, 50, enemyBullet),
        });
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, this.transform.parent.position-new Vector3(0,15,0), 60, false),});
    }

    public void Pattern4()
    {
        //왼쪽 이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(-20, 0, 0), 20, true),
            new ShootBullet(this.transform,player, 120, 10, 5, enemyBullet),
        });
        //오른쪽 이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(40, 0, 0), 20, true),
        });
        //왼쪽 이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(-40, 0, 0), 20, true),
            new ShootBullet(this.transform,player, 120, 10, 5, enemyBullet),
        });
        //중앙이동
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, new Vector3(20, 0, 0), 20, true),
        });
    }

    public void Pattern5()
    {
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, this.transform.parent.position-new Vector3(0,40,0), 60, false), });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 40, 4, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 50, 8, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 60, 12, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 70, 16, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 80, 20, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 85, 24, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new RoundShoot(this.transform, 90, 28, 50, enemyBullet) });
        nextPattern.Add(new Ticking[] { new MoveToPosition(this.transform, this.transform.parent.position-new Vector3(0, 15, 0), 60, false), });
    }

    public interface Ticking
    {
        public bool tick();
        public void firstTick() { }
    }

    public class MoveToPosition : Ticking
    {
        Transform transform;
        Vector3 target;
        float speed;
        bool now;

        public MoveToPosition(Transform transform, Vector3 target, float speed, bool now)
        {
            this.transform = transform;
            this.target = target;
            this.speed = speed;
            this.now = now;
        }

        public bool tick()
        {
            if(transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                return false;
            }
            return true;
        }

        public void firstTick() { //위치이동 (현재 위치 기반) , 아닐시 (절대 위치 기반)
            if (now)
            {
                target = transform.position + target;
            }
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
                if(bulletScript != null)
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

    public class ShotgunBullet : Ticking
    {
        Transform target;
        float speed;
        int bulletCount;
        int delay;
        GameObject enemyBullet;
        Transform transform;
        public ShotgunBullet(Transform transform, Transform target, float speed, int bulletCount, int delay, GameObject enemyBullet)
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
                for (int i = -bulletCount; i <= bulletCount; i++)
                {
                    GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

                    EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.targetPosition = new Vector3(target.position.x + i * 3, target.position.y, target.position.z);
                        bulletScript.bulletSpeed = speed;
                    }
                }
                return true;
            }
            return false;
        }
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
}
