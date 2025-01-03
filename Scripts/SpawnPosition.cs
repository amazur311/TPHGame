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
            player = GameObject.Find("MainChPlay");
        }

        // Subscribe to scene events
       // SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe after a slight delay to ensure OnSceneUnloaded runs first.
        if (!hasUnsubscribed)
        {
            StartCoroutine(UnsubscribeWithDelay());
        }
    }



    private void OnDisable()
    {
        // Unsubscribe after a slight delay
       //StartCoroutine(UnsubscribeWithDelay());
    }

    private IEnumerator UnsubscribeWithDelay()
    {
        yield return null; // Wait one frame to allow OnSceneUnloaded to execute.
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        hasUnsubscribed = true;
        Debug.Log("Unsubscribed from SceneManager events");
    }

    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log(scene);
        if (sceneNameScObj != null)
        {
            sceneNameScObj.sceneName = scene.name; // Set the scene name in the ScriptableObject
            Debug.Log($"Scene unloaded: {scene.name}");
        }
        else
        {
            Debug.LogError("SceneNameSO is not assigned!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this == null) return; // If the object is already destroyed, stop processing.

        if (sceneNameScObj != null && sceneNameCompare == sceneNameScObj.sceneName)
        {
            Debug.Log($"Match found: {sceneNameCompare}. Spawning player.");
            SpawnPos();
        }
        else
        {
            Debug.Log($"No match for {sceneNameCompare}. Scene loaded: {scene.name}");
        }
    }

    private void SpawnPos()
    {
        if (player == null)
        {
            player = GameObject.Find("MainChPlay");
        }


        Debug.Log("SpawnPos activated");
        if (player != null)
        {
            player.transform.position = transform.position;
            sceneNameScObj.sceneName = null;
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned!");
        }
    }
}