using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<Enemy> activeEnemies = new List<Enemy>();
    public EnemyDB enemyDatabase;

    [Header("Treasure")] 
    public GameObject treasurePrefab;
    public Transform treasureSpawnPoint;
    
    private int aliveCount = 0;

    public void Spawn(int enemyID, int hp, int maxHP)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        EnemySO enemySO = enemyDatabase.Get(enemyID);
        GameObject tmp = Instantiate(enemySO.prefab, spawnPoint.position, Quaternion.identity);

        Enemy e = tmp.GetComponent<Enemy>();
        e.MaxHP = maxHP;
        e.HP = hp;
        activeEnemies.Add(e);
        e.enemyID = enemyID;
        e.ATK = enemySO.ATK;
        e.DEF = enemySO.DEF;
        
        aliveCount++;
        e.UpdateHP(e.HP, e.MaxHP);

        e.OnDied += () =>
        {
            Vector3 deathPosition = e.transform.position;
            HandleEnemyDied(e, deathPosition);
        };
    }

    private void HandleEnemyDied(Enemy e, Vector2 position)
    {
        aliveCount--;

        if (aliveCount <= 0)
        {
            GameStateManager.Instance.OnAllEnemiesKilled(position);
        }
    }

    public void ClearEnemies()
    {
        foreach (Enemy e in activeEnemies)
        {
            Destroy(e.gameObject);
        }
        aliveCount = 0;
        activeEnemies.Clear();
    }

    public void SpawnTreasure()
    {
        if (treasurePrefab != null && treasureSpawnPoint != null)
        {
            Instantiate(treasurePrefab, treasureSpawnPoint.position, Quaternion.identity);
        }
    }
    
    public void SpawnTreasure(Vector3 position)
    {
        if (treasurePrefab != null)
        {
            Instantiate(treasurePrefab, position, Quaternion.identity);
        }
    }
}

