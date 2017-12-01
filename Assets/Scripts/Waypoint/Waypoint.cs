using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public Waypoint next;
    public Waypoint prev;
    public float radius;

    [Header("SoloParaCinematica")]
    public float speedToNextWP;

    //[Header("SoloParaDollyPath")]
    //public Waypoint 

    //[Header("SoloParaDollyCam")]
    //public float maxVel;

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);

        if (next != null) { 
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }

        if (prev != null) { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, prev.transform.position);
        }

    }
}
