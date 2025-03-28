using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameStarter : MonoBehaviour
{
    
    public string newGameScene;
    
    public string devPractScene;
    public SceneNameSO loadScene;
    

    // Start is called before the first frame update
    void Start()
    {
        if (loadScene.sceneName == null)
        {
            GameObject.Find("LoadGame Button").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameLoader()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGameLoader() 
    {
        if (loadScene.sceneName != null)
        {

            SceneManager.LoadScene(loadScene.sceneName);
        }
    }

    public void DevPracticeRoomLoader()
    {
        SceneManager.LoadScene(devPractScene);
    }

}
