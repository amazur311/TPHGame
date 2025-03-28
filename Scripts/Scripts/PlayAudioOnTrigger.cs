using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    public bool playOnce = false;
    public bool alreadyPlayed = false;
    public AudioSource audio;
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
            if(playOnce == true && alreadyPlayed == true)
            {
                return;
               
            } else 
            {
                audio.Play();
                alreadyPlayed = true;
            }
            
        }
    }
}
