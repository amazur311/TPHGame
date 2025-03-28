using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ToolsAndWeaponsListMono : MonoBehaviour
{
    [SerializeField]
    public ToolsAndWeaponsList toolsAndWeaponsState; // Non-runtime object
    public ToolsAndWeaponsList inventoryListState;   // Non-runtime object
    public GameObject weaponScreen;
    public GameObject inventoryScreen;

    private ToolsAndWeaponsList runtimeToolsAndWeaponsList; // Runtime object for tools and weapons
    private ToolsAndWeaponsList runtimeInventoryListState;  // Runtime object for inventory
    private bool isPaused = false;  // Track whether the game is paused

    // Flag to determine if syncing should happen (for testing purposes)
    [SerializeField]
    private bool syncToRuntime = false;

    void Start()
    {
        ClearPlayerPrefsForScriptableObjects();
        runtimeToolsAndWeaponsList = Instantiate(toolsAndWeaponsState);
        runtimeInventoryListState = Instantiate(inventoryListState);

        toolsAndWeaponsState.SaveInventory();
        inventoryListState.SaveInventory();

        weaponScreen.SetActive(false);
        inventoryScreen.SetActive(false);

        runtimeToolsAndWeaponsList.LoadInventory();
        runtimeInventoryListState.LoadInventory();
    }

    void SyncToRuntime()
    {
        if (syncToRuntime)
        {
            runtimeToolsAndWeaponsList.inventoryAndWeapons = new List<CustomData>(toolsAndWeaponsState.inventoryAndWeapons);
            runtimeInventoryListState.inventoryAndWeapons = new List<CustomData>(inventoryListState.inventoryAndWeapons);

            // Update UI states after syncing
            UpdateUI();
        }
    }

    void Update()
    {
        SyncToRuntime();
    }

    public void TogglePause()
    {
        // Check if the game is paused by evaluating Time.timeScale
        bool gameIsPaused = Time.timeScale == 0;

        // Only update isPaused if it has changed
        if (isPaused != gameIsPaused)
        {
            isPaused = gameIsPaused;

            // Update the UI based on the pause state
            if (isPaused)
            {
                CheckWeapons();
                CheckInventory();
            }
            else
            {
                weaponScreen.SetActive(false);
                inventoryScreen.SetActive(false);
            }
            Debug.Log("isPaused: " + isPaused + "gameIsPaused: " + gameIsPaused);
        }
    }

    private void UpdateUI()
    {
        // Update both inventory and weapon UI based on runtime data
        CheckWeapons();
        CheckInventory();
    }

    public void CheckWeapons()
    {
        if (!isPaused)
        {
            weaponScreen.SetActive(false);
            return;
        }

        bool hasWeapon = runtimeToolsAndWeaponsList.inventoryAndWeapons.Exists(item => item.playerCarries);

        weaponScreen.SetActive(hasWeapon);
        if (hasWeapon)
        {
            SetActiveWeaponChildren();
        }
    }

    public void CheckInventory()
    {
        if (!isPaused)
        {
            inventoryScreen.SetActive(false);
            return;
        }

        bool hasInventoryItem = runtimeInventoryListState.inventoryAndWeapons.Exists(item => item.playerCarries);

        inventoryScreen.SetActive(hasInventoryItem);
        if (hasInventoryItem)
        {
            SetActiveInventoryChildren();
        }
    }

    public void SetActiveWeaponChildren()
    {
        for (int x = 0; x < weaponScreen.transform.childCount; x++)
        {
            GameObject childScreen = weaponScreen.transform.GetChild(x).gameObject;
            childScreen.SetActive(runtimeToolsAndWeaponsList.inventoryAndWeapons[x].playerCarries);
        }
    }

    public void SetActiveInventoryChildren()
    {
        for (int x = 0; x < inventoryScreen.transform.childCount; x++)
        {
            GameObject childScreen = inventoryScreen.transform.GetChild(x).gameObject;
            childScreen.SetActive(runtimeInventoryListState.inventoryAndWeapons[x].playerCarries);
        }
    }

    public void UpdateWeaponStatus(int index, bool isActive)
    {
        if (index >= 0 && index < runtimeToolsAndWeaponsList.inventoryAndWeapons.Count)
        {
            runtimeToolsAndWeaponsList.inventoryAndWeapons[index].playerCarries = isActive;
            runtimeToolsAndWeaponsList.SaveInventory();
            UpdateUI();
        }
    }

    public void UpdateInventoryStatus(int index, bool isActive)
    {
        if (index >= 0 && index < runtimeInventoryListState.inventoryAndWeapons.Count)
        {
            runtimeInventoryListState.inventoryAndWeapons[index].playerCarries = isActive;
            runtimeInventoryListState.SaveInventory();
            UpdateUI();
        }
    }

    private void OnApplicationQuit()
    {
        runtimeToolsAndWeaponsList.SaveInventory();
        runtimeInventoryListState.SaveInventory();
    }


    void ClearPlayerPrefsForScriptableObjects()
    {
        PlayerPrefs.DeleteAll();
    }
}