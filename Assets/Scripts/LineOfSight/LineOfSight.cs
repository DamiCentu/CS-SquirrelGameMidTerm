using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour {

    [Header("NormalParameters")] 
    public float NormalsightDistance = 7f;
    [Range(0f, 180)] 
    public float NormalsightAngle = 50f;

    [Header("ExitedParameters")] 
    public float ExitedsightDistance = 10f;
    [Range(0f, 180)]
    public float ExitedsightAngle = 90f;

    [Header("MaskToDetect")]
    public LayerMask targetMask;

    float _sightDistance = 7f;
    float _sightAngle = 60f;
    Transform _target;
    bool _isInSight = false;
    Vector3 _lastPosition;

	public Transform inSight { get; private set; }
    public Transform Target { set { _target = value; } }
    public bool IsInSight { get { return _isInSight; } }
    public Vector3 LastPosition { get { return _lastPosition; } }

    public void setNormalBehaviour(){
        _sightDistance = NormalsightDistance;
        _sightAngle = NormalsightAngle;
    }

     public void setExitedBehaviour(){
        _sightDistance = ExitedsightDistance;
        _sightAngle = ExitedsightAngle;
    }

    void Start() {
        setNormalBehaviour();
    }

    void Update () {
		inSight = null;
        _isInSight = false;

        Transform my = transform;
		Transform other = _target;

		var deltaPos = other.position - my.position;

		var angle = Vector3.Angle(transform.forward, deltaPos);

		var sqrDistance = deltaPos.sqrMagnitude;

		if(sqrDistance < _sightDistance * _sightDistance && angle < _sightAngle/2f) {
		    RaycastHit rch;
			if(Physics.Raycast(my.position, deltaPos, out rch, _sightDistance)) {
                //if(Utility.LayerNumberToMask(rch.collider.gameObject.layer) == targetMask) { 
                    inSight = other;
                    _isInSight = true;
                    _lastPosition = other.position;
                    _lastPosition.y = transform.position.y;
               // }
            } 
        } 
    }

	void OnDrawGizmos() {
		var p = transform.position;
		var f = transform.forward;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(p, p+f*_sightDistance);

		Gizmos.color = _sightAngle > 180f ? Color.white : Color.yellow;
		Vector3 lastW = Vector3.zero;
		for(float a = 0; a <= 360f; a += 20f) {
			var v = new Vector3(
				0f,
				Mathf.Sin(Mathf.Deg2Rad * _sightAngle/2f) * _sightDistance,
				Mathf.Cos(Mathf.Deg2Rad * _sightAngle/2f) * _sightDistance
			);
			var w = transform.rotation * Quaternion.AngleAxis(a, Vector3.forward) * v;

			Gizmos.DrawLine(p, p +  w);
			Gizmos.DrawLine(p + lastW, p + w);
			lastW = w;
		}
		if(inSight != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(inSight.position, 1f);
			Gizmos.DrawSphere(p + Vector3.up, 0.5f);
			Gizmos.DrawLine(p + Vector3.up, inSight.position);
		}
			
	}
}
