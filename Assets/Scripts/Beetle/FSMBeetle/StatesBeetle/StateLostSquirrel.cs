using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLostSquirrel : State<InputBeetle> {
    public StateLostSquirrel(FSMBeetle fsm) : base(fsm, "LostSquirrel") { }

    Flocking _flocking;
    BeetleBehaviur _beetle;
    LineOfSight _lineOfSight;

    public override void OnEnter() {
        _flocking = (Flocking)((FSMBeetle)this.Fsm).beetleFlocking;
        _lineOfSight = (LineOfSight)((FSMBeetle)this.Fsm).beetleLineOfSight; 
        _beetle = (BeetleBehaviur)((FSMBeetle)this.Fsm).beetle;
        _lineOfSight.setExitedBehaviour();
    }

    public override void OnUpdate() {  
        _flocking.Target = _lineOfSight.LastPosition;
    }

    public bool reachedPosition() {
        return Utility.InRange(_beetle.transform.position, _lineOfSight.LastPosition, _beetle.radiusOfSoundToChaseAndLastPosition);
    }
}
