using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    public Animation openDoor;
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        Debug.Log("OpenDoor Happened");
        openDoor.Play();
    }
}
