using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : CritterControl
{

    // Start is called before the first frame update
    //void Start()
    //{
    //if (!mainCam)
    //mainCam = GameObject.FindWithTag("Player").GetComponent<PlayerControl>().mainCam;
    //}

    //public Camera mainCam2;
    
    

    //Update is called once per frame
    void Update()
    {
        //Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
        //Vector3 topScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        //Vector3 topScreen = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        //check position of bullet, destroy if too high up, gameObject
        //if(PlayerControl.bullet.transform.position > PlayerControl.Screen.height)
        Debug.Log("To destroy --> Object location is: " + gameObject.transform.position.y);
        if (gameObject.transform.position.y > 10f)
        {
            Debug.Log("Object location is: " + gameObject.transform.position.y);
            Destroy(gameObject);
            Debug.Log("Object about to be destroyed");
        }         
            
    }
}
