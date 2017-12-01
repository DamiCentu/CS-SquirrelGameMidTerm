using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableClimb : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
       //EventManager.instance.ExecuteEvent("EnableClimb");
       EventManager.instance.ExecuteEvent("ClimbInstructions");
    }

}
