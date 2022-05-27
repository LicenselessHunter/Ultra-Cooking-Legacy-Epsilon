using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount = 0;
    public GameObject enemyPrefab;
    public float spawnCooldown;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if(gameObject.transform.childCount == 0)
        {
            SpawnEnemies();
        }
    }

    void Update()
    {
        if(!rend.isVisible)
        {
            if(gameObject.transform.childCount < enemyCount)
            {
                spawnCooldown -= Time.deltaTime;
            }

            if(spawnCooldown <= 0)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        for (var i = gameObject.transform.childCount; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            enemy.transform.SetParent(gameObject.transform);
            spawnCooldown = 5;
        }
    }
}