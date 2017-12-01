using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatleTestBehaviur : MonoBehaviour {
    [Header ("StartWaypoint")]
    public Waypoint startPath;
    [Header("RangesToCheck")]
    public float radiusOfHearing;
    public float radiusOfSoundToChase;

    public Transform _squirrel;

    Waypoint _currentWaypoint;
    Flocking _flocking; 
    BeetleStates _currentState;
    LineOfSight _lineOfSight;
    Vector3 _soundToChasePosition;

    public LineOfSight lineOfSight { get { return _lineOfSight; } }
    
    public enum BeetleStates {
        Patrolling,
        ChasingSquirrel,
        ChasingSound
    }

    void Awake() { 
        _lineOfSight = GetComponent<LineOfSight>();
        //_squirrel = (Squirrel3D)FindObjectOfType(typeof(Squirrel3D));
        _lineOfSight.Target = _squirrel;
        _flocking = GetComponent<Flocking>();
        _currentState = BeetleStates.Patrolling; 
        _currentWaypoint = startPath;
        _flocking.Target = _currentWaypoint.transform.position;
    }

    private void Start()
    {
        if(GameManager.instance != null)
            GameManager.instance.AddBeetle(this);
    }

    void Update() { 
        //Debug.Log(_currentState);
        if (_lineOfSight.IsInSight )//&& _squirrel.isVisible())
            _currentState = BeetleStates.ChasingSquirrel;
        else if (_currentState != BeetleStates.ChasingSound)
            _currentState = BeetleStates.Patrolling;

        switch (_currentState) {
            case BeetleStates.Patrolling: 
                patrol(); 
                break;
            case BeetleStates.ChasingSquirrel: 
                chase(_lineOfSight.inSight.position); 
                break;
            case BeetleStates.ChasingSound:
                chase(_soundToChasePosition);
                if (InRange(_soundToChasePosition, radiusOfSoundToChase))
                    _currentState = BeetleStates.Patrolling;
                break;
        } 
    }

    void chase(Vector3 chasePosition) {  
        _flocking.Target = chasePosition;  
    }

    void patrol() {
        _flocking.Target = _currentWaypoint.transform.position;

        if(InRange(_currentWaypoint.transform.position, _currentWaypoint.radius))
            _currentWaypoint = _currentWaypoint.next; 
    } 

    bool InRange(Vector3 target, float distance) {
        return Vector3.Distance(transform.position, target) < distance;
    }

    public void hearSound(Vector3 sourcePoint) {
        if (_currentState != BeetleStates.ChasingSquirrel && InRange(sourcePoint,radiusOfHearing)) { 
            _soundToChasePosition = sourcePoint;
            _currentState = BeetleStates.ChasingSound;
        } 
    }

    private void OnDrawGizmos() {
        if(_currentState == BeetleStates.ChasingSound) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_soundToChasePosition, radiusOfSoundToChase);
        }

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, radiusOfHearing);
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.layer == 10) { //Squirrel 
            c.GetComponent<PlayerBrain>().TakeDamage(int.MaxValue); // esto es una negrada, arreglar!
            _flocking.enabled = false;
        }
    }
}
