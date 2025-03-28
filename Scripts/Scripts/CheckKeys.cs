using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeys : MonoBehaviour
{

    
    public ObjectSaveState objectState;
    public ObjectGrabber objectGrabber;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        objectGrabber = GameObject.FindObjectOfType<ObjectGrabber>();

        if (gameObject.CompareTag("Door"))
        {
            gameObject.GetComponent<Door>();
        }

            if (gameObject.CompareTag("RotateDoor"))
            {
                gameObject.GetComponent<DoorRotator>();
            }

        if (gameObject.CompareTag("Drawer"))
            {
            gameObject.GetComponent<DrawerOpen>();
            }
        }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfKeysAreActive()
    {
        bool allActive = true;

        if (key != null)
        {
            for (int i = 0; i < objectState.objList.Count; i++)//checks for the Key in the Object list by name and whether it is active or not. This allows the key to be in a different scene from where the check occurs. I cannot get it to work with multiple keys, but works fine with one
            {

                if (objectState.objList[i].isActive == false && key == objectState.objList[i].objName)
                {
                    allActive = false;
                    break;
                }
                allActive = true;

            }
        }
        else allActive = false;
        

        if (allActive == false)
        {
            gameObject.GetComponent<InteractableObject>().ToggleActiveState();
            

            if (gameObject.CompareTag("Door"))
            {
                gameObject.GetComponent<Door>().enabled = true;
            }

            if (gameObject.CompareTag("RotateDoor"))
            {
                gameObject.GetComponent<DoorRotator>().enabled = true;
            }

            if (gameObject.CompareTag("Drawer"))
            {
                gameObject.GetComponent<DrawerOpen>().enabled = true;
            }

        }
    }
}
