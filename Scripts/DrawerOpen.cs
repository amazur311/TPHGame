using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    public float newXPos;
    public float newYPos;
    public float newZPos;
    private float oldZ;
    private float oldY;
    private float oldX;
  
    // Start is called before the first frame update
    void Start()
    {
        oldZ = transform.position.z;
        oldY = transform.position.y;
        oldX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDrawer()
    {
        
        Debug.Log("opendrawer occurred");
        if(Mathf.Abs(transform.position.x - oldX) <= Mathf.Abs(newXPos) && Mathf.Abs(transform.position.y - oldY) <= Mathf.Abs(newYPos) && Mathf.Abs(transform.position.z - oldZ) <= Mathf.Abs(newZPos))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(newXPos, newYPos, newZPos), .02f);
            
        }
        
    }
}
