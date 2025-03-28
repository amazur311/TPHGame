using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StorePreviousSceneName : MonoBehaviour
{

    public SceneNameSO sceneNameScObj;
    public ObjectGrabber objGrabber;
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to scene events
        
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (objGrabber.doorObjForSceneSave != null)
        {
            if (objGrabber.doorObjForSceneSave.CompareTag("Door"))
            {
                SaveScene();
            }
        }
        
    }

    private void SaveScene()
    {
        Debug.Log("saveScene Ran");
        if (sceneNameScObj != null)
        {
            sceneNameScObj.sceneName = SceneManager.GetActiveScene().name ; // Set the scene name in the ScriptableObject
        Debug.Log($"Scene unloaded: {sceneNameScObj.sceneName}");
        }
        else
        {
        Debug.LogError("SceneNameSO is not assigned!");
        }
        
    }


}
