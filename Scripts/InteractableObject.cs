using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public ObjectSaveState objectState;
    public string gameObjectName;// Reference to the Scriptable Object

    public void Awake()
    {
        gameObjectName = gameObject.name;
        if(gameObject.CompareTag("Key")|| gameObject.CompareTag("PickUp") || gameObject.CompareTag("KeyItem") || gameObject.CompareTag("Ammo") || gameObject.CompareTag("Hi-C"))
        {
            //gameObject.SetActive(objectState.objList[i].isActive);
        }

        if (gameObject.CompareTag("Door"))
        {
           // gameObject.GetComponent<Door>().enabled = objectState.isActive;
        }

        for(int i = 0; i<objectState.objList.Count; i++)
        {
            if (gameObjectName == objectState.objList[i].objName)
            {
                gameObject.SetActive(objectState.objList[i].isActive);
                return;
            }
        }
        
    }
    // Example method to change state
    public void SetActiveState(bool active)
    {
        //objectState.isActive = active;
        //SaveState();  // Save state whenever it changes

        for (int i = 0; i < objectState.objList.Count; i++)
        {
            if (gameObjectName == objectState.objList[i].objName)
            {
                objectState.objList[i].isActive = !objectState.objList[i].isActive;
            }
        }
    }
    
    // Example method to toggle state
    public void ToggleActiveState() //This will toggle the corresponding object in the Object List to inactive so that it is persistent
    {
        // objectState.isActive = !objectState.isActive;
        // SaveState();
        for (int i = 0; i < objectState.objList.Count; i++)
        {
            if (gameObjectName == objectState.objList[i].objName)
            {
                objectState.objList[i].isActive = !objectState.objList[i].isActive;
                return;
            }
        }

    }


    /*private void SaveState()
    {
        GameManager.Instance.SaveObjectState();
    }*/
}
