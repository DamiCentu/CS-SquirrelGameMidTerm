using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour {
    public Waypoint StartPath;

    void OnTriggerEnter(Collider other) { 
        CameraManager.instance.SetCinematic(StartPath);
    }
}
