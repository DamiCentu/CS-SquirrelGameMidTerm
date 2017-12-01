using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSeek : ActionNodeBeetle { 
    public override void Execute(BeetleBehaviur reference) {
        reference.ProcessInputBeetle(InputBeetle.InSight);
    }
}
