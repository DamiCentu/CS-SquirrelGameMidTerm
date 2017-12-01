using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public Transform CheckpointTransform;
    void OnTriggerEnter(Collider other) {
        object[] param = (new object[1]);
        param[0] = CheckpointTransform;
        EventManager.instance.ExecuteEvent("SaveCheckPoint", param);
        //print("entro al checkpoint!");
    }
}
