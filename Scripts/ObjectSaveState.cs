using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectState", menuName = "Custom/Object State", order = 1)]
public class ObjectSaveState : ScriptableObject
{
    [SerializeField]
    private string uniqueID = System.Guid.NewGuid().ToString();


    //public bool isActive = true;
    public List<CustomObjectData> objList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        // Save object state when the GameObject is disabled or scene changes
        //GameManager.Instance.SaveObjectState();
    }
}

[System.Serializable]
public class CustomObjectData
{
    public string objName;
    public bool isActive;
    
}