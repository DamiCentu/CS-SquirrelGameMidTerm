using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWalk : MonoBehaviour {



    void OnTriggerEnter(Collider other)
    {
        EventManager.instance.ExecuteEvent("EnableWalk");
    }
}
