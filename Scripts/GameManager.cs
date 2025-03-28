using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI gamePauseText;
    public ToolsAndWeaponsListMono inventoryAndWeaponsCheck;
    public RawImage inventoryScreen;
    public RawImage weaponsScreen;
    public ObjectGrabber objectGrabber;
    public GameObject dummyObj;
    public int hiCcount;
    public int ammoCount;
    public TextMeshProUGUI pickUpObjText;
    public TextMeshProUGUI objPickedUpText;
    public TextMeshProUGUI hiCcountText;
    public TextMeshProUGUI ammoCountText;
    public static GameManager Instance;
    private GameObject canvasHolder;
    public int randomAmmoCountPickUp;
    public RawImage[] mapImage;
    public ObjectSaveState objectState;

    private string savePath;
   
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvasHolder = GameObject.Find("CanvasHolder");
  
           
            DontDestroyOnLoad(canvasHolder);
        
        


        InitializeReferences();

        

       // savePath = Path.Combine(Application.persistentDataPath, "objectSaveStates.json");
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        dummyObj = GameObject.Find("DummyObj");
        objectGrabber = GameObject.Find("ConeColliderGrabber").GetComponent<ObjectGrabber>();

        SceneManager.sceneUnloaded += OnSceneUnloaded;




    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.P))
        {
            GamePauser();
            gamePauseText.gameObject.SetActive(true);
            //inventoryScreen.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            GameUnpauser();
            gamePauseText.gameObject.SetActive(false);
            inventoryScreen.gameObject.SetActive(false);
            weaponsScreen.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetKeyDown(KeyCode.M) && objectState.objList[0].isActive == false)
        {
            mapImage[0].enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            mapImage[0].enabled = false; ;
        }

        while (objectGrabber.objPickedUpBoolInvMgr == true)
        {
            //InventoryManager();
            objectGrabber.objPickedUpBoolInvMgr = false;
        }
    }

    public void MapCheckerAndEnabler() //I will need to think about how I do this. If I have multiple indoor maps how will the game know which to show if there are multiple images/floors within an area. Ideally the player could pull up any map using a button, but that might be too sophisticated.
    {
        for (int i = 0; i < mapImage.Length; i++)//loops through all mapImage objects, going to have to check some static value that each area holds (empty game Object with a name in each scene? i.e. School, Floor 1)
        {
        }
    }

    public void GamePauser()
    {
        Time.timeScale = 0;
        inventoryAndWeaponsCheck.TogglePause();
        
    }

    public void GameUnpauser()
    {
        Time.timeScale = 1;
        inventoryAndWeaponsCheck.TogglePause();
    }

    public void InventoryManager()
    {
        //Debug.Log("InvMgr called");



        if (objectGrabber.objPickedUpTag.CompareTag("Hi-C"))
        {
            hiCcount++;

        }

        if (objectGrabber.objPickedUpTag.CompareTag("Ammo"))
        {
            randomAmmoCountPickUp = Random.Range(1, 6);

            ammoCount = ammoCount + randomAmmoCountPickUp;
        }
        
        hiCcountText.SetText(hiCcount.ToString());
        ammoCountText.SetText(ammoCount.ToString());
    }

    public void WeaponManager()
    {

    }


    public void HiCUsed()
    {
        if (hiCcount > 0)
        {
            hiCcount--;
        }


        objectGrabber.objPickedUpTag = dummyObj;
        InventoryManager();





    }

    public void AmmoUsed()
    {
        

        if (ammoCount > 0)
        {
            ammoCount--;
        }
        objectGrabber.objPickedUpTag = dummyObj;
        InventoryManager();





    }

    public void LoadNewScene(GameObject door)
    {

        //TrackActiveInteractablesInScene();
        

        
        
            if (door.GetComponent<Door>().enabled == true)
            {
                SceneManager.LoadScene(door.GetComponent<Door>().sceneName);

            }

        



    }

    public List<GameObject> TrackActiveInteractablesInScene()
    {
        int interactablesCount;

        List<GameObject> interactables = new List<GameObject>();
        interactables.AddRange(GameObject.FindGameObjectsWithTag("Ammo"));
        interactables.AddRange(GameObject.FindGameObjectsWithTag("Hi-C"));
        interactablesCount = interactables.Count;
        Debug.Log(interactables + " + " + interactablesCount);
        /* This function will hold the GetActive state of all interactable objects (i.e. did the player character pick up the ammo, which causes the object to be set to Inactive) when a scene is destroyed (before loading new scene)
           If the player character re-enters the scene, another method SetActiveInteractables will reference the states stored by this function and set each of the objects to the appropriate state 
           so that only objects the player hasn't interacted with show up on reloading the scene */

        return interactables;
    }

    /*public void SaveObjectState()
    {
        // Save object state to PlayerPrefs or a file
        // Create a serializable container to store states
         List<ObjectSaveData> saveData = new List<ObjectSaveData>();

        // Iterate through all interactable objects with ObjectSaveState script attached
         InteractableObject[] objectStates = FindObjectsOfType<InteractableObject>();
        

        // Gather all states into the container
        foreach (InteractableObject state in objectStates)
        {
            ObjectSaveData data = new ObjectSaveData();
            data.isActive = state.objectState.isActive; //save Necessary Properties
            Debug.Log("ObjectSaveData: " + data.isActive);
            saveData.Add(data);
            Debug.Log("SaveData:" + saveData.Count);
        }

        // Save the serialized data to disk
        string json = JsonUtility.ToJson(saveData);
        Debug.Log("JSON Data: " + json);
        savePath = Path.Combine(Application.persistentDataPath, "objectSaveStates.json");
        File.WriteAllText(savePath, json);
        
        Debug.Log("Saved object state to: "+ savePath);
    }*/

    public void LoadObjectState()
    {
        // Check if save file exists
        /*if (File.Exists(savePath))
        {
            // Read JSON data from file
            string json = File.ReadAllText(savePath);
            List<ObjectSaveData> saveData = JsonUtility.FromJson<List<ObjectSaveData>>(json);

            // Apply the loaded states to the objects
            for (int i = 0; i < Mathf.Min(saveData.Count, objectStates.Count); i++)
            {
                objectStates[i].isActive = saveData[i].isActive;
            }
        }
        else
        {
            // If no save file exists, initialize states to default
            for (int i = 0; i < objectStates.Count; i++)
            {
                objectStates[i].isActive = true;  // Default state
            }
        }*/
    }

    public void GetActiveInteractablesInScene()
    {
       
    }

    private void InitializeReferences()
    {
        // Find and initialize UI elements and other important GameObjects
        if (gamePauseText == null)
        {
            gamePauseText = GameObject.Find("GamePaused").GetComponent<TextMeshProUGUI>();
            if (gamePauseText != null)
            {
                gamePauseText.gameObject.SetActive(false);
            }
        }

        if (hiCcountText == null)
        {
            hiCcountText = GameObject.FindGameObjectWithTag("hiCcountUI").GetComponent<TextMeshProUGUI>();
        }

        if (ammoCountText == null)
        {
            ammoCountText = GameObject.FindGameObjectWithTag("ammoCountUI").GetComponent<TextMeshProUGUI>();
        }

        if (inventoryScreen == null)
        {
            inventoryScreen = GameObject.FindGameObjectWithTag("inventoryScreen").GetComponent<RawImage>();
            if (inventoryScreen != null)
            {
                inventoryScreen.gameObject.SetActive(false);
            }
        }

        objectGrabber = GameObject.Find("ConeColliderGrabber").GetComponent<ObjectGrabber>();
        dummyObj = GameObject.Find("DummyObj");

        if (objPickedUpText == null)
        {
            objPickedUpText = GameObject.Find("objPickedUp").GetComponent<TextMeshProUGUI>();
            if (objPickedUpText != null)
            {
                objPickedUpText.gameObject.SetActive(false);
            }
        }

        if (pickUpObjText == null)
        {
            pickUpObjText = GameObject.Find("pickUpObj").GetComponent<TextMeshProUGUI>();
            if (pickUpObjText != null)
            {
                pickUpObjText.gameObject.SetActive(false);
            }
        }

    }

    private void OnDestroy()
    {
       SceneManager.sceneLoaded -= OnSceneLoaded;
       SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // OnEnable is called when the script instance is being loaded or reloaded
    private void OnEnable()
    {
        
        
       SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

   private void OnSceneUnloaded(Scene scene)
    {
        // Save object state when scene is unloaded
        //SaveObjectState();
         Debug.Log("SaveObjectState Ran");
    }

    // Called each time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
       
        //LoadObjectState();
        
    }

    private class ObjectSaveData
    {
        public bool isActive;
        // Add other properties as needed
    }

    

}


