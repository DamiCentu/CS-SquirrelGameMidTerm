using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBeetle : FSM<InputBeetle> {
    public BeetleBehaviur beetle;
    public Flocking beetleFlocking;
    public LineOfSight beetleLineOfSight;
}

public enum InputBeetle {
    InSight,
    LostSight,
    SoundHearded, 
    ReachedPosition,
    finishedWandering
}
