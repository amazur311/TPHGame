using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeys : MonoBehaviour
{

    public GameObject[] keys;
    public ObjectGrabber objectGrabber;
    // Start is called before the first frame update
    void Start()
    {
        objectGrabber = GameObject.FindObjectOfType<ObjectGrabber>();

        gameObject.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfKeysAreActive()
    {
        bool allActive = true;

        for (int i = 0; i < keys.Length; i++)//checks if each key is active or not.
        {
            if (keys[i].activeSelf == false)
            {
                allActive = false;

            }
            else allActive = true;
        }

        if (allActive == false)
        {
            gameObject.GetComponent<InteractableObject>().ToggleActiveState();
            gameObject.GetComponent<Door>().enabled = gameObject.GetComponent<InteractableObject>().objectState;
        }
    }
}
