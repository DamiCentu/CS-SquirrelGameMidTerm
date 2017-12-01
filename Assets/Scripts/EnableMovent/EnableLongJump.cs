using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLongJump : MonoBehaviour {
    //public Waypoint StartPath;
    
    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableLongJump");
        // EventManager.instance.ExecuteEvent("LongJumpInstructions");
        //CameraManager.instance.SetCinematic(StartPath);
    }
    void OnTriggerStay(Collider other)
    {
    }
}
