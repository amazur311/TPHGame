using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPositionHandler : MonoBehaviour
{
    public GameObject player;
    public SceneNameSO sceneNameScObj;
    
    public SpawnPosition[] spawnPos;
    

    //private bool hasUnsubscribed = false;

    private void Start()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            player = GameObject.Find("MainChPlay_112324");
        }

        for(int x = 0; x < spawnPos.Length; x++)
        {
            Debug.Log(spawnPos[x].sceneNameCompare + " : " + sceneNameScObj);
            if (sceneNameScObj != null && spawnPos[x].sceneNameCompare == sceneNameScObj.sceneName)
            {
                Debug.Log($"Match found: {spawnPos[x].sceneNameCompare}. Spawning player.");
                //SpawnPos();
                if (player != null)
                {
                    player.transform.position = spawnPos[x].transform.position;
                    Debug.Log("SpawnPos activated");
                    //sceneNameScObj.sceneName = null;
                }
                break;
            }
            else
            {
                Debug.Log($"No match for {spawnPos[x].sceneNameCompare}. Scene loaded: {SceneManager.GetActiveScene().name}");
            }
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