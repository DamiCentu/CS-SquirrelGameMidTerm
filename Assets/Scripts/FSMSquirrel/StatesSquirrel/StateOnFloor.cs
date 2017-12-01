using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOnFloor : State<InputSquirrel> {
    public StateOnFloor(FSMSquirrel fsm) : base(fsm, "OnFloor") { }

    //SquirrelBehaviur _squirrel;

    //public override void OnEnter() { 
    //    _squirrel = (SquirrelBehaviur)((FSMSquirrel)this.Fsm).squirrel;
    //    _squirrel.PhObj.Floored = true;
    //}
    //public override void OnUpdate() {
    //    if (!_squirrel.PhObj.Jumping && !DetectCollision.CollideTo(_squirrel.PhObj.ColliderBoundsCenter, Vector2.down, _squirrel.PhObj.ColliderBoundsY, _squirrel.PhObj.maskToCollideFloor))//!_squirrel.PhObj.Grounded)
    //        _squirrel.ProcessInput(InputSquirrel.leavePlatform);
    //}

    //public override void OnExit() {
    //    _squirrel.PhObj.Floored = false;
    //}
}
