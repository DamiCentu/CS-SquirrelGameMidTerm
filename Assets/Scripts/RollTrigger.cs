using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTrigger : MonoBehaviour {
    public Transform rollDirection;
    void OnTriggerEnter(Collider collider)
    {
        object[] parametros = new object[1];
        parametros[0] = rollDirection;
        EventManager.instance.ExecuteEvent("Roll", parametros);
    }
}
