using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLostSquirrel : ActionNodeBeetle { 
    public override void Execute(BeetleBehaviur reference) { 
        reference.ProcessInputBeetle(InputBeetle.LostSight);
    }
}
