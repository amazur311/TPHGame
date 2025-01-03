using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LogoToGame : MonoBehaviour
{

    
    public PlayableDirector playLogo;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        playLogo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(playLogo.state != PlayState.Playing)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
