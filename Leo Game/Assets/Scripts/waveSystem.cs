using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSystem : MonoBehaviour
{
    public GameObject[] enemySpawnPoints;
    private int spawnerAmmount;
    public float spawnTimer;
    public GameObject[] enemyTypes;
    private bool canSpawn;

    private void Awake()
    {
        StartCoroutine(SpawnMoreEnemys(spawnTimer));
        spawnerAmmount = enemySpawnPoints.Length;
    }

    private IEnumerator SpawnMoreEnemys(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canSpawn = true;
    }

    private void Update()
    {
        if(canSpawn)
        {
            for (int i = 0; i < spawnerAmmount; i++)
            {
                int whichEnemyTpye = Random.Range(0, 3);
                Instantiate(enemyTypes[whichEnemyTpye], (enemySpawnPoints[i].transform));
            }
            canSpawn = false;
            StartCoroutine(SpawnMoreEnemys(spawnTimer));
        }
    }

}
