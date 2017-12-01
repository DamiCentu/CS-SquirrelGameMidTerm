using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputSquirrel {
    floorCollision,
    jumpPressed,
    wallNearAndClimbPressed,
    leavePlatform,
    timeOfJumpEnded,
    unpressedClimb,
    onHideSpotAndPressedHideKey
}

public class FSMSquirrel : FSM<InputSquirrel> {
    public SquirrelBehaviur squirrel;
}
