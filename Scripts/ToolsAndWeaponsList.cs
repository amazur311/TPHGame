using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ToolsAndWeaponsList", menuName = "Custom/ToolsAndWeaponsList", order = 2)]
public class ToolsAndWeaponsList : ScriptableObject
{
    [SerializeField]
    private string uniqueID = System.Guid.NewGuid().ToString(); // Automatically generate a unique ID

    public List<CustomData> inventoryAndWeapons;

    // Expose the ID for debugging or external use
    public string UniqueID => uniqueID;

    // Save inventory data
    public void SaveInventory()
    {
        for (int i = 0; i < inventoryAndWeapons.Count; i++)
        {
            string key = $"{uniqueID}_Weapon_{i}";
            PlayerPrefs.SetInt(key, inventoryAndWeapons[i].playerCarries ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // Load inventory data
    public void LoadInventory()
    {
        for (int i = 0; i < inventoryAndWeapons.Count; i++)
        {
            string key = $"{uniqueID}_Weapon_{i}";
            if (PlayerPrefs.HasKey(key))
            {
                inventoryAndWeapons[i].playerCarries = PlayerPrefs.GetInt(key) == 1;
            }
        }
    }

    // Reset inventory, e.g., for a new game
    public void ResetInventory()
    {
        for (int i = 0; i < inventoryAndWeapons.Count; i++)
        {
            inventoryAndWeapons[i].playerCarries = false;
        }
    }
}

[System.Serializable]
public class CustomData
{
    public bool playerCarries;
    public string name;
    public bool isConsumableOrRanged;
    public int count;

}
