using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour {
    void OnCollisionEnter(Collision collision)
    {
        EventManager.instance.ExecuteEvent("FinalPart");
    }
}
