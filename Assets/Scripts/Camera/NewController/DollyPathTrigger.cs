using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyPathTrigger : MonoBehaviour { 
	public Waypoint startCamPath;
    public Waypoint startPlayerPath;
    public bool canGoBackwards = false;

    void OnTriggerEnter(Collider other) {
        CameraManager.instance.SetDollyPath(startCamPath, startPlayerPath);///, canGoBackwards);
    }
}
