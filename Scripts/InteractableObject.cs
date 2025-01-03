using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public ObjectSaveState objectState;  // Reference to the Scriptable Object

    public void Awake()
    {
        if(gameObject.CompareTag("Key")|| gameObject.CompareTag("PickUp") || gameObject.CompareTag("KeyItem") || gameObject.CompareTag("Ammo") || gameObject.CompareTag("Hi-C"))
        {
            gameObject.SetActive(objectState.isActive);
        }

        if (gameObject.CompareTag("Door"))
        {
            gameObject.GetComponent<Door>().enabled = objectState.isActive;
        }
    }
    // Example method to change state
    public void SetActiveState(bool active)
    {
        objectState.isActive = active;
        //SaveState();  // Save state whenever it changes
    }
    
    // Example method to toggle state
    public void ToggleActiveState()
    {
        objectState.isActive = !objectState.isActive;
       // SaveState();
    }


    /*private void SaveState()
    {
        GameManager.Instance.SaveObjectState();
    }*/
}
