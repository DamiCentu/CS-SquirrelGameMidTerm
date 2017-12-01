using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFly : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableGlide");
    }
}
