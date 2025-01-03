using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoInteractible : MonoBehaviour
{
    // Start is called before the first frame update
    public string infoTextString;
    public GameObject descriptionPrefab;
    public GameObject notePrefab;
    public Canvas textInfo;
    
    //public TextMeshProUGUI infoText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Canvas InfoActivate() //Creates a new GameObject that a textmeshproUGUI object is set based on the inspector input. Returns the Canvas COmponent so that the Deactivate function can destroy the object
    {

        //infoText.gameObject.SetActive(true);
        if (gameObject.CompareTag("InfoInteractibleDescription"))
        {
            textInfo = Instantiate(descriptionPrefab, new Vector3(700f, -400, 0), Quaternion.identity).GetComponent<Canvas>();

            TextMeshProUGUI textObj = textInfo.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            textObj.SetText(infoTextString);
            return textInfo;
        }
        else if (gameObject.CompareTag("InfoInteractibleNote"))
        {
            textInfo = Instantiate(notePrefab, new Vector3(700f, -400, 0), Quaternion.identity).GetComponent<Canvas>();

            TextMeshProUGUI textObj = textInfo.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            textObj.SetText(infoTextString);
            return textInfo;
        }
        else return textInfo;
    }
        

    public void InfoDeactivate(Canvas textInfo)//Destroys the Canvas that was created by InfoActivate
    {
        Destroy(textInfo.gameObject);
    }

}
