using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    //public List<MapState> mapStates = new List<MapState>();
    public GameState gameState;
    public Transform mapParent;
    private EnemySpawner spawner;
    
    private MapState currentMapState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        TreasurePickup.PickupTreasure +=  UpdateTreasureCount;
    }

    private void Start()
    {
        foreach (MapState mapState in gameState.mapStates)
        {
            mapState.InitializeDictionary();
        }

        InitializeMap(0);
    }
    public void InitializeMap(int mapID_)
    {
        foreach (MapState mapState in gameState.mapStates)
        {
            if (mapState.mapID == mapID_)
            {
                currentMapState = mapState;
                BeginEnemySpawn(currentMapState);
                break;
            }
        }
    }

    public void BeginEnemySpawn(MapState map)
    {
        spawner = mapParent.GetComponentInChildren<EnemySpawner>();
        foreach (EnemyState enemy in map.enemyStates)
        {

            if (enemy.currentHP > 0) spawner.Spawn(enemy.enemyID, enemy.currentHP,enemy.maxHP);
        }

        if (spawner.activeEnemies.Count == 0 && !map.givenTreasure)
        {
            //this is called in the weird scenario where treasure doesnt spawn and you leave and come back into a map. it will give you treasure then
            OnAllEnemiesKilled();
        }
    }

    public void OnAllEnemiesKilled(Vector3 position)
    {
        if (currentMapState.givenTreasure) return;
        
        currentMapState.givenTreasure = true;

        if (spawner != null)
        {
            spawner.SpawnTreasure(position);
        }
    }
    
    // this is a backup method. 
    public void OnAllEnemiesKilled()
    {
        if (currentMapState.givenTreasure) return;
        
        currentMapState.givenTreasure = true;

        if (spawner != null)
        {
            spawner.SpawnTreasure();
        }
    }

    public void UpdateTreasureCount()
    {
        Instance.gameState.coinsCollected = GetTreasureCount();
    }

    private int GetTreasureCount()
    {
        int count = 0;
        foreach (MapState m in gameState.mapStates)
        {
            if (m.givenTreasure) count++;
        }

        return count;
    }
    

    [ContextMenu("Reset Enemies")]
    public void ResetEnemies()
    {
        foreach (MapState m in gameState.mapStates)
        {
            foreach (EnemyState e in m.enemyStates)
            {
                e.currentHP = e.maxHP;
            }
        }
    }

    public void ResetToNewGame()
    {
        foreach (MapState m in gameState.mapStates)
        {
            m.givenTreasure = false;
            foreach (EnemyState e in m.enemyStates)
            {
                e.currentHP = e.maxHP;
            }
        }
        gameState.coinsCollected = 0;
    }
    

    [ContextMenu("Try Save")]
    public void SaveGameState()
    {
        if (spawner != null)
        {
            List<Enemy> enemies = spawner.activeEnemies;
            foreach (Enemy enemy in enemies)
            {
                currentMapState.enemyDictionary[enemy.enemyID].currentHP = enemy.HP;
                Debug.Log(currentMapState.enemyDictionary[enemy.enemyID].currentHP);
            }
        }
        else
        {
            Debug.LogWarning("Can't save game state without spawner.");
        }

    }
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;
    public bool givenTreasure;
    [NonSerialized] public Dictionary<int, EnemyState> enemyDictionary;

    public void InitializeDictionary()
    {
        enemyDictionary = new Dictionary<int, EnemyState>();
        foreach (EnemyState enemy in enemyStates)
        {
            enemyDictionary.Add(enemy.enemyID, enemy);
        }
    }
}

[Serializable]
public class EnemyState
{
    public int enemyID;
    public int currentHP;
    public int maxHP;
}

[Serializable]
public class GameState
{
    public int coinsCollected;
    public List<MapState> mapStates;
    
}
