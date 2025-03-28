using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneTimer : MonoBehaviour

{

    public float timer;
    private float gameTime;
    public string sceneName;
    public SceneNameSO sceneNameSO;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if(timer <= gameTime)
        {
            sceneNameSO.sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
