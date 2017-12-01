using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBehaviur : MonoBehaviour {
    [Header("StartWaypoint")]
    public Waypoint startPath;
    [Header("RangesToCheck")]
    public float radiusOfHearing;
    public float radiusOfSoundToChaseAndLastPosition;

    [Header("WanderTime")]
    public float WanderTime;

    public Transform _squirrel;

    Waypoint _currentWaypoint;
    Flocking _flocking; 
    LineOfSight _lineOfSight;
    Vector3 _soundToChasePosition;
    FSMBeetle _fsm;

    public Waypoint CurrentWaypoint { get { return _currentWaypoint; } set { _currentWaypoint = value; } }
    public Vector3 SoundToChasePosition { get { return _soundToChasePosition; } } 
    //public LineOfSight lineOfSight { get { return _lineOfSight; } } 

    void Awake()
    {
        _lineOfSight = GetComponent<LineOfSight>();
        //_squirrel = (Squirrel3D)FindObjectOfType(typeof(Squirrel3D));
        _lineOfSight.Target = _squirrel;
        _flocking = GetComponent<Flocking>(); 
        _currentWaypoint = startPath;
        _flocking.Target = _currentWaypoint.transform.position;

        setFSM();
    }

    private void Start()
    {
        //if (GameManager.instance != null)
            //GameManager.instance.AddBeetle(this);
    }

    void Update() {
        _fsm.Update();

        if (_lineOfSight.IsInSight)
            ProcessInputBeetle(InputBeetle.InSight); 

        if (_fsm.Current == seekingSquirrel && !_lineOfSight.IsInSight)
            ProcessInputBeetle(InputBeetle.LostSight);

        if (_fsm.Current == lostSquirrel && lostSquirrel.reachedPosition())
                ProcessInputBeetle(InputBeetle.ReachedPosition);

        if (_fsm.Current == chasingSound && chasingSound.reachedPosition())
                ProcessInputBeetle(InputBeetle.ReachedPosition);

        if (_fsm.Current == wander && wander.WanderTimeIsOver())
            ProcessInputBeetle(InputBeetle.finishedWandering);

    }

    public void hearSound(Vector3 sourcePoint) {
        if (Utility.InRange(transform.position,sourcePoint, radiusOfHearing)) {
            _soundToChasePosition = sourcePoint;
            ProcessInputBeetle(InputBeetle.SoundHearded);
        }
    }

    void OnDrawGizmos() {
        if (_fsm !=null && chasingSound != null && _fsm.Current == chasingSound) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_soundToChasePosition, radiusOfSoundToChaseAndLastPosition);
        }

        if(_fsm != null && lostSquirrel != null && _fsm.Current == lostSquirrel) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_lineOfSight.LastPosition, radiusOfSoundToChaseAndLastPosition);
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

    StatePatrolling patrolling;
    StateWander wander;
    StateLostSquirrel lostSquirrel;
    StateChasingSound chasingSound;
    StateSeekingSquirrel seekingSquirrel;

    void setFSM() {
        _fsm = new FSMBeetle();
        _fsm.beetle = this;
        _fsm.beetleFlocking = _flocking;
        _fsm.beetleLineOfSight = _lineOfSight;

        patrolling = new StatePatrolling(_fsm);
        wander = new StateWander(_fsm);
        lostSquirrel = new StateLostSquirrel(_fsm);
        chasingSound = new StateChasingSound(_fsm);
        seekingSquirrel = new StateSeekingSquirrel(_fsm);


        //patrolling
        patrolling.transitions[InputBeetle.InSight] = seekingSquirrel;
        patrolling.transitions[InputBeetle.SoundHearded] = chasingSound;

        //seekingSquirrel
        seekingSquirrel.transitions[InputBeetle.LostSight] = lostSquirrel;

        //lostSquirrel
        lostSquirrel.transitions[InputBeetle.ReachedPosition] = wander;

        //wander
        wander.transitions[InputBeetle.InSight] = seekingSquirrel;
        wander.transitions[InputBeetle.SoundHearded] = chasingSound;
        wander.transitions[InputBeetle.finishedWandering] = patrolling;

        //chasingSound
        chasingSound.transitions[InputBeetle.ReachedPosition] = wander;
        chasingSound.transitions[InputBeetle.InSight] = seekingSquirrel; 

        _fsm.SetInitial(patrolling);
    }
    
    public void ProcessInputBeetle(InputBeetle input) {
        _fsm.ProcessInput(input);
    }
}
