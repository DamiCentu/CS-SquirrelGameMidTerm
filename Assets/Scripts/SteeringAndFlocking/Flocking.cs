using UnityEngine;
using System.Collections;

public class Flocking : Steering {

    [Header ("Avoidance")] 
    public float raycastLength = 3f;
    public float avoidanceMultiplier;
    public float rhAngle = 20f;
    public LayerMask raycastLayers;

    bool _wandering = false;

    public bool Wandering { get { return _wandering; } set { _wandering = value; } }

    void FixedUpdate () {
		ResetForces();
        
        //obtacleAvoidance
        var rLeft =  Quaternion.AngleAxis(rhAngle, transform.up) * velocity.normalized ;
        var rRight = Quaternion.AngleAxis(-rhAngle, transform.up) * velocity.normalized;

        Debug.DrawLine(transform.position, transform.position + rLeft, Color.magenta, Time.fixedDeltaTime);
        Debug.DrawLine(transform.position, transform.position + rRight, Color.magenta, Time.fixedDeltaTime);

        RaycastHit rh;
        if(Physics.Raycast(transform.position,velocity, out rh, raycastLength, raycastLayers) ||
           Physics.Raycast(transform.position, rLeft, out rh, raycastLength, raycastLayers) ||
           Physics.Raycast(transform.position, rRight, out rh, raycastLength, raycastLayers)) {
            AddForce(Avoidance(transform.position, rh.transform.position) * avoidanceMultiplier);
        } 

        if (_wandering)
            AddForce(WanderRandomPos());
        else
            AddForce(Seek(targetTransform));

        ApplyForces();
	}
    
	override protected void OnDrawGizmos() {
		base.OnDrawGizmos();
	}
}
