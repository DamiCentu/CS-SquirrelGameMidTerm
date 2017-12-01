using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJumpText : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableLongJumpText");
    }
}
