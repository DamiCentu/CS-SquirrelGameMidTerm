using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.AlertBeetles(this.transform.position);
        //print("Alerto");
    }
}
