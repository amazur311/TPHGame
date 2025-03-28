using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPosition : MonoBehaviour
{
    public GameObject player;
    public SceneNameSO sceneNameScObj;
    public string sceneNameCompare;
    

    private bool hasUnsubscribed = false;

    private void Start()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            player = GameObject.Find("MainChPlay_112324");
        }


        if (sceneNameScObj != null && sceneNameCompare == sceneNameScObj.sceneName)
        {
            Debug.Log($"Match found: {sceneNameCompare}. Spawning player.");
            SpawnPos();
        }
        else
        {
            Debug.Log($"No match for {sceneNameCompare}. Scene loaded: {SceneManager.GetActiveScene().name}");
        }


        // Subscribe to scene events
       // SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    private void SpawnPos()
    {
        if (player == null)
        {
            player = GameObject.Find("MainChPlay_112324");
        }


        
        if (player != null)
        {
            player.transform.position = transform.position;
            Debug.Log("SpawnPos activated");
            //sceneNameScObj.sceneName = null;
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned!");
        }
    }
}