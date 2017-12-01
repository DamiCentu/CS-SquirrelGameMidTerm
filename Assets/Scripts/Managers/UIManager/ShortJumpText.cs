using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortJumpText : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableShortJumpText");
    }

}
