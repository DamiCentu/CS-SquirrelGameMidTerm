using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShortJump : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        //EventManager.instance.ExecuteEvent("ShortJumpInstructions");
        EventManager.instance.ExecuteEvent("EnableShortJump");
    }
}
