using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour {

    void OnDrawGizmos()
    {
        
        Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff");
    }
}
