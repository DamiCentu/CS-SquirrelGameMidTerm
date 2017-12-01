using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyPathEndTrigger : MonoBehaviour { 
    void OnTriggerEnter(Collider other) { 
        CameraManager.instance.ChangeToThirdPerson();
    }
}
