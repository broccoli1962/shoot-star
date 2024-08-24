using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemySet;
    public float SpawnCoolTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        Enemy();
        yield return new WaitForSeconds(SpawnCoolTime);
        if (!SystemManager.state)
        {
            StartCoroutine("EnemySpawn");
        }
    }

    void Enemy()
    {
        GameObject egg = Instantiate(enemy) as GameObject;
        egg.transform.parent = enemySet.transform;
        egg.transform.position = new Vector3(Random.Range(-10f, 10f), transform.position.y, 0);
        egg.GetComponent<Enemy>().enemySpeed = Random.Range(2, 4);
    }
}