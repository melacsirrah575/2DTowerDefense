using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)]float spawnTimer = 1f;

    [SerializeField] GameObject bossPrefab;
    [SerializeField] [Range(0, 50)] int bossPoolSize = 1;
    [SerializeField] [Range(1, 100)] int bossWave = 10;

    int waveNumber = 0;


    GameObject[] pool;
    GameObject[] bossPool;

    private void Awake()
    {
        PopulatePools();
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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

        if (waveNumber >= bossWave)
        {
            waveNumber = 0;
            waveNumber++;
        }
        else
        {
            waveNumber++;
        }
        Debug.Log(waveNumber);
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

            if (waveNumber == bossWave)
            {
                EnableBossObjectInPool();
            }

            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
