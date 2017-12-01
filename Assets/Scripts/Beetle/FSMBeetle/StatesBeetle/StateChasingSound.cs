using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChasingSound : State<InputBeetle> {
    public StateChasingSound(FSMBeetle fsm) : base(fsm, "ChasingSound") { }

    Flocking _flocking;
    BeetleBehaviur _beetle;
    LineOfSight _lineOfSight;

    public override void OnEnter() {
        _flocking = (Flocking)((FSMBeetle)this.Fsm).beetleFlocking;
        _beetle = (BeetleBehaviur)((FSMBeetle)this.Fsm).beetle;
        _lineOfSight = (LineOfSight)((FSMBeetle)this.Fsm).beetleLineOfSight;
        _lineOfSight.setExitedBehaviour();
    }

    public override void OnUpdate() {  
        _flocking.Target = _beetle.SoundToChasePosition;
    }

    public bool reachedPosition() {
        return Utility.InRange(_beetle.transform.position, _beetle.SoundToChasePosition, _beetle.radiusOfSoundToChaseAndLastPosition);
    }
}
