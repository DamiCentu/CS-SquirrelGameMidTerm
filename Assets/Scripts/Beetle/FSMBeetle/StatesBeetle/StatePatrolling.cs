using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrolling : State<InputBeetle> {
    public StatePatrolling(FSMBeetle fsm) : base(fsm, "Patrolling") { }

    Flocking _flocking;
    BeetleBehaviur _beetle;
    LineOfSight _lineOfSight;

    public override void OnEnter() {
        _flocking = (Flocking)((FSMBeetle)this.Fsm).beetleFlocking;
        _beetle = (BeetleBehaviur)((FSMBeetle)this.Fsm).beetle; 
        _lineOfSight = (LineOfSight)((FSMBeetle)this.Fsm).beetleLineOfSight;
        _lineOfSight.setNormalBehaviour();
    }

    public override void OnUpdate() {
        patrol();
    }

    void patrol() {
        _flocking.Target = _beetle.CurrentWaypoint.transform.position;

        if (Utility.InRange(_beetle.transform.position, _beetle.CurrentWaypoint.transform.position, _beetle.CurrentWaypoint.radius))
            _beetle.CurrentWaypoint = _beetle.CurrentWaypoint.next;
    }
}
