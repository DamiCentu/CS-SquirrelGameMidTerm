using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideText : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableGlideText");
    }
}
