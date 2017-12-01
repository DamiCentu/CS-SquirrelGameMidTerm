using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSeekingSquirrel : State<InputBeetle> {
    public StateSeekingSquirrel(FSMBeetle fsm) : base(fsm, "SeekingSquirrel") { }

    Flocking _flocking;
    LineOfSight _lineOfSight;

    public override void OnEnter() {
        _flocking = (Flocking)((FSMBeetle)this.Fsm).beetleFlocking;
        _lineOfSight = (LineOfSight)((FSMBeetle)this.Fsm).beetleLineOfSight;
        _lineOfSight.setExitedBehaviour();
    }

    public override void OnUpdate() {
        if(_lineOfSight.IsInSight)
            _flocking.Target = _lineOfSight.inSight.position;
    }
}
