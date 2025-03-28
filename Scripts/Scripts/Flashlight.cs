using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(flashlight.enabled == true)
            {
                flashlight.enabled = false;
                return;
            }
            
            if(flashlight.enabled == false)
            {
                flashlight.enabled = true;
                return;
            }
        } 
    }
}
