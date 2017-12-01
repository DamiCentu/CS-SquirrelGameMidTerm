using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlLimits : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 10) { //Squirrel 
            CameraManager.instance.ChangeToDeathFall();
            MyUiManager.instance.OnDeathFadeOut();
            StateMachine.instance.ChangeStateIfNeeded("Die");
        }
    }
}
