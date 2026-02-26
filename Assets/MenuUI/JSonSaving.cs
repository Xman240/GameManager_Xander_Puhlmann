using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JSonSaving : MonoBehaviour
{
    
    //public string filePath;
    public string saveName;
    private string SavePath => Path.Combine(Application.persistentDataPath, saveName + ".json");
    public static JSonSaving Instance;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    [ContextMenu("JSON Save")]
    public void SaveData()
    {
        //string file = filePath + saveName + ".json";
        string filePathFixed = Path.Combine(Application.persistentDataPath, saveName + ".json");
        string json = JsonUtility.ToJson(GameStateManager.Instance.gameState, true);

        File.WriteAllText(filePathFixed, json);
    }

    public bool HasSaveData()
    {
        return File.Exists(SavePath);
    }

    public bool LoadData()
    {
        if (!HasSaveData())
        {
            Debug.Log("No save data found");
            return false;
        }

        string json = File.ReadAllText(SavePath);
        
        JsonUtility.FromJsonOverwrite(json, GameStateManager.Instance.gameState);

        foreach (MapState mapState in GameStateManager.Instance.gameState.mapStates)
        {
            mapState.InitializeDictionary();
        }
        GameStateManager.Instance.UpdateTreasureCount();
        Debug.Log("Loaded " + GameStateManager.Instance.gameState.mapStates.Count);
        return true;

    }

    public void DeleteSave()
    {
        if (HasSaveData())
        {
            File.Delete(SavePath);
            Debug.Log("Save deleted");
        }
    }
}
