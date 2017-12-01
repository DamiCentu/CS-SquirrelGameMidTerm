using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : ActionNodeBeetle { 
    public override void Execute(BeetleBehaviur reference) { 
        reference.ProcessInputBeetle(InputBeetle.finishedWandering);
    }
}