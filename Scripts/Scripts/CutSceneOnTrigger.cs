using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneOnTrigger : MonoBehaviour
{

    public string sceneName;
    public string eventID;
    public EventManagerSO eventManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!eventManager.GetEventState(eventID))
            {
                eventManager.SetEventState(eventID, true); // Mark event as triggered
                eventManager.LogAllEventIDs();
                TriggerCutscene();
                
            }
        }


    }

    public void TriggerCutscene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
