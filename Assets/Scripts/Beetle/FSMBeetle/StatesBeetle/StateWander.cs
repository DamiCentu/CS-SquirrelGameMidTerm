using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : State<InputBeetle> {
    public StateWander(FSMBeetle fsm) : base(fsm, "Wander") { }

    float _time;
    Flocking _flocking;
    BeetleBehaviur _beetle;
    LineOfSight _lineOfSight;

    public override void OnEnter() {
        _flocking = (Flocking)((FSMBeetle)this.Fsm).beetleFlocking;
        _beetle = (BeetleBehaviur)((FSMBeetle)this.Fsm).beetle;
        _lineOfSight = (LineOfSight)((FSMBeetle)this.Fsm).beetleLineOfSight;
        _time = 0f;
        _flocking.Wandering = true;
        _lineOfSight.setExitedBehaviour();
    }

    public override void OnUpdate() {
        _time += Time.deltaTime;
    }

    public bool WanderTimeIsOver() {
        return _time > _beetle.WanderTime;
    }

    public override void OnExit() {
        _flocking.Wandering = false;
    }
}
