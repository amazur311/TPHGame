using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectGrabber : MonoBehaviour
{
    public Ray fwdray;
    public RaycastHit hitData;
    public GameObject player;
    //public TextMeshProUGUI pickUpObjText;
    //public TextMeshProUGUI objPickedUpText;
    public float objPickedUpTextTimer;
    public bool objPickedUpBool;
    public bool objPickedUpBoolInvMgr;
    public bool noteTriggered;
    public GameObject objPickedUpTag;
    public GameManager gameManager;
    public InfoInteractible infoInteractible;
    //public  ObjectGrabber Instance;
    // Start is called before the first frame update

    private void Awake()
    {
   

    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
              
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if(gameManager.pickUpObjText != null)
        {
            if (Vector3.Distance(player.transform.position, other.transform.position) <= 2.0f && (other.CompareTag("PickUp") || other.CompareTag("KeyItem") || other.CompareTag("Ammo") || other.CompareTag("Hi-C") || other.CompareTag("Key")))
            {
                gameManager.pickUpObjText.gameObject.SetActive(true);
                gameManager.pickUpObjText.SetText("Pick Up " + other.gameObject.name + "?");

                if (Input.GetKey(KeyCode.E))
                {
                    gameManager.objPickedUpText.SetText(other.gameObject.name + " Picked Up");
                    
                    if (other.gameObject != null)
                    {
                        InteractableObject interactableObject = other.gameObject.GetComponent<InteractableObject>();
                        objPickedUpTag = other.gameObject;
                       
                        if(interactableObject != null)
                        {
                            interactableObject.ToggleActiveState();
                        }
                    }
                    other.gameObject.SetActive(false);
                    gameManager.pickUpObjText.gameObject.SetActive(false);
                    gameManager.objPickedUpText.gameObject.SetActive(true);

                    //Destroy(other.gameObject);
                    //objPickedUpTextTimer = 0;
                    objPickedUpBool = true;
                    objPickedUpBoolInvMgr = true;
                    StartCoroutine(TimerForObjGrabber());


                }
            }


            if (Vector3.Distance(player.transform.position, other.transform.position) <= 2.0f && other.CompareTag("Door")) //This section is for Doors and will call the CheckKeys job that will ensure all objects needed to progress are interacted with
            {
                if(other.GetComponent<CheckKeys>() != null)
                {
                    other.GetComponent<CheckKeys>().CheckIfKeysAreActive();
                }
                //Debug.Log(other.gameObject.name);
                if(other.GetComponent<Door>().enabled == true)
                {
                    gameManager.pickUpObjText.gameObject.SetActive(true);
                    gameManager.pickUpObjText.SetText("Leave Room?");

                    if (Input.GetKey(KeyCode.E))
                    {

                        gameManager.pickUpObjText.gameObject.SetActive(false);
                        gameManager.LoadNewScene(other.gameObject);

                        objPickedUpBool = true;
                        //objPickedUpBoolInvMgr = true;
                        StartCoroutine(TimerForObjGrabber());


                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {

                        gameManager.pickUpObjText.gameObject.SetActive(true);
                        gameManager.pickUpObjText.SetText("Door is Locked");

                        objPickedUpBool = true;
                        //objPickedUpBoolInvMgr = true;
                        StartCoroutine(TimerForObjGrabber());


                    }
                }
                
            }
           
            if (Vector3.Distance(player.transform.position, other.transform.position) <= 2.0f && (other.CompareTag("InfoInteractibleDescription") || other.CompareTag("InfoInteractibleNote"))) //This section will be for iunteractible objects that provide information, like notes, world building objects and immersive elements to provide characterization.
            {
                
                gameManager.pickUpObjText.gameObject.SetActive(true);
                    gameManager.pickUpObjText.SetText(other.gameObject.name);
                    infoInteractible = other.gameObject.GetComponent<InfoInteractible>();
                if (Input.GetKey(KeyCode.E) && noteTriggered == false)
                {
                    
                    gameManager.pickUpObjText.gameObject.SetActive(false);

                    infoInteractible.InfoActivate();
                    noteTriggered = true;
                    //Time.timeScale = 0;

                }

                if (Input.GetKey(KeyCode.Q) && noteTriggered == true)
                {
                    Debug.Log("Q was hit");

                    infoInteractible.InfoDeactivate(infoInteractible.textInfo);
                    //Time.timeScale = 1;
                    noteTriggered = false;
                    
                }
            }

        }
        


    }

    private void OnTriggerExit(Collider other)
    {
        if(gameManager.objPickedUpText !=null)
        {
            gameManager.pickUpObjText.gameObject.SetActive(false);
        }
        

    }

    IEnumerator TimerForObjGrabber()
    {
        
        
        yield return new WaitForSeconds(3.0f);
        objPickedUpBool = false;
        objPickedUpBoolInvMgr = false;
        gameManager.objPickedUpText.gameObject.SetActive(false);
       
    }
}
