using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)]float spawnTimer = 1f;

    [SerializeField] GameObject bossPrefab;
    [SerializeField] [Range(0, 50)] int bossPoolSize = 1;
    [SerializeField] [Range(0.1f, 30f)] float bossSpawnTimer = 1f;

    [SerializeField] EnemyMover enemyMover;

    GameObject[] pool;
    GameObject[] bossPool;


    private void Awake()
    {
        PopulatePools();
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnBoss());
    }

    private void PopulatePools()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }

        bossPool = new GameObject[bossPoolSize];

        for (int i = 0; i < bossPool.Length; i++)
        {
            bossPool[i] = Instantiate(bossPrefab, transform);
            bossPool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    void EnableBossObjectInPool()
    {
        for (int i = 0; i < bossPool.Length; i++)
        {
            if (bossPool[i].activeInHierarchy == false)
            {
                bossPool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        //Currently Infinite but doesn't break game due to how CoRoutines work
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    IEnumerator SpawnBoss()
    {
        if (enemyMover.WaveNumber == 10)
        {
            EnableBossObjectInPool();
        }
        else
        {
            yield return new WaitForSeconds(bossSpawnTimer);
        }
        yield return new WaitForSeconds(bossSpawnTimer);
    }
}
