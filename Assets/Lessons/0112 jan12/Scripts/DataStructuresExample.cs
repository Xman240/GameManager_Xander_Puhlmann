using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DataStructuresExample : MonoBehaviour
{
    int i;
    float o;
    string s;
    char c;

    float maxSpeed;

    /*
     * 
     * leaderboard - top 5 times -> array or a list\
     * ghost data
     * fastest time -> float (seconds passed), string, Time
     * profiles -> name, vehicle, colour, high score --> List
*/
    // arrays, lists, dictionaries
    private List<InventoryItem> inventory;
    public Dictionary<InventoryItem, int> inventoryDictionary = new Dictionary<InventoryItem, int>();

    private void Start()
    {
        InventoryItem shield = new InventoryItem();
        shield.itemName = "shield";
        shield.weight = 10;
        shield.durability = 10;
        shield.dmg = 0;

        /* foreach(InventoryItem item in inventory)
         {
             if(item.itemName == shield.itemName)
             {
                 item.count++;
                 return;
             }
         }*/
    //inventory.Add(shield);

    AddItem(shield);
        AddItem(shield);


    }

    public void AddItem(InventoryItem item)
    {
        if (inventoryDictionary.ContainsKey(item))
        {
            inventoryDictionary[item]++;
        }
        else
        {
            inventoryDictionary.Add(item, 1);
        }
        
        Debug.Log(item.itemName + " count = " + inventoryDictionary[item]);
    }


}

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int weight;
    public int dmg;
    public int durability;
}

public class Profile
{
    int highscore;
    string profileName;
    VehicleColor vehicleColor;
    GameObject vehicle;

    
}

public enum VehicleColor
{
    WHITE,
    BLACK,
    RED,
    PURPLE,
    BLUE
}
