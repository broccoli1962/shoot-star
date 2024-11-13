using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using TMPro;

public class EnemyCreator : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject enemySet;
    public int currentStage = 1;

    private StageJsonData stageData;
    private StageData currentStageData;
    private bool stageClear = false;
    private TextMeshProUGUI stageNumber;

    void Start()
    {
        stageNumber = GameObject.Find("InGameUI").transform.Find("Stage").GetComponent<TextMeshProUGUI>();
        LoadStageData();
        StartCoroutine("StageNum");
        StartCoroutine("SpawnWaves");
    }

    private void Update()
    {
        if (stageClear)
        {
            NextStage();
        }
    }

    private void LoadStageData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StageData.json");
        string jsonString = File.ReadAllText(filePath);
        stageData = JsonConvert.DeserializeObject<StageJsonData>(jsonString);
        Debug.Log(stageData.enemies);
        currentStageData = stageData.stages[currentStage - 1];
    }

    IEnumerator SpawnWaves()
    {
        foreach(WaveData wave in currentStageData.waves)
        {
            for(int i= 0; i<wave.enemyCount; i++)
            {
                SpawnEnemy(wave.enemyType, wave.spawnX);
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
        SpawnBoss();

        yield return new WaitUntil(()=> GameObject.FindGameObjectWithTag("BossEgg") == null);

        stageClear = true;
    }

    void NextStage()
    {
        currentStage++;

        if(currentStage <= stageData.stages.Length)
        {
            currentStageData = stageData.stages[currentStage - 1];
        }
        else
        {
            SystemManager.state = true;
            Debug.Log("전 스테이지 클리어");
        }

        StartCoroutine("StageNum");
        stageClear = false;
        StartCoroutine("SpawnWaves");
    }

    IEnumerator StageNum()
    {
        stageNumber.gameObject.SetActive(true);
        stageNumber.text = "Stage" + currentStage;
        yield return new WaitForSeconds(3f);
        stageNumber.gameObject.SetActive(false);
    }

    void SpawnEnemy(string enemyType, float spawnX)
    {
        GameObject prefab = enemyPrefabs.FirstOrDefault(e => e.name == enemyType);
        if(prefab == null)
        {
            Debug.LogError("Enemy 프리팹을 찾을 수 없습니다 " + enemyType);
            return;
        }

        GameObject enemy = Instantiate(prefab);
        enemy.transform.parent = enemySet.transform;
        enemy.transform.position = new Vector3(spawnX, transform.position.y, 0);

        EnemyData data = stageData.enemies[enemyType];
        enemy.GetComponent<Enemy>().SetStats(data.health, data.speed , enemyType);
    }

    void SpawnBoss()
    {
        string bossType = currentStageData.boss;
        GameObject prefab = bossPrefabs.FirstOrDefault(b => b.name == bossType);
        if(prefab == null)
        {
            Debug.LogError("Boss 프리팹을 찾을 수 없습니다 " + bossType);
            return;
        }

        GameObject boss = Instantiate(prefab);
        boss.transform.parent = enemySet.transform;
        boss.transform.position = new Vector3(0f, transform.position.y, 0f);

        BossData data = stageData.bosses[bossType];
        //boss.GetComponent<Boss>().SetStats(data.health, data.speed, data.attackPattern);
        boss.GetComponent<BossPattern>().SetStats(data.health, data.speed, data.attackPattern);
    }
}

[System.Serializable]
public class EnemyData
{
    public int health;
    public float speed;
}

[System.Serializable]
public class BossData : EnemyData
{
    public string attackPattern;
}

[System.Serializable]
public class WaveData
{
    public string enemyType;
    public int enemyCount;
    public float spawnInterval;
    public float spawnX;
}
[System.Serializable]
public class StageData
{
    public int stageNumber;
    public WaveData[] waves;
    public string boss;
}
[System.Serializable]
public class StageJsonData
{
    public StageData[] stages;
    public Dictionary<string, EnemyData> enemies = new Dictionary<string, EnemyData>();
    public Dictionary<string, BossData> bosses = new Dictionary<string, BossData>();
}