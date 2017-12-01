using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJumping : State<InputSquirrel> {
    public StateJumping(FSMSquirrel fsm) : base(fsm, "Jumping") { }

    SquirrelBehaviur squirrel;

    //public override void OnEnter() { 
    //    squirrel = (SquirrelBehaviur)((FSMSquirrel)this.Fsm).squirrel;
    //    //_timer = 0f;
    //    squirrel.PhObj.SetJump(); 
    //}
    //public override void OnUpdate() {
    //    if (!squirrel.PhObj.Jumping 
    //        || DetectCollision.CollideTo(squirrel.PhObj.ColliderBoundsCenter, Vector2.down, squirrel.PhObj.ColliderBoundsY, squirrel.PhObj.maskToCollideFloor) 
    //        || DetectCollision.CollideTo(squirrel.PhObj.ColliderBoundsCenter, Vector2.up, squirrel.PhObj.ColliderBoundsY, squirrel.PhObj.maskToCollideFloor)) {
    //        //Debug.Log(squirrel.PhObj.DetCol.CollideTo(squirrel.PhObj.ColliderBoundsCenter, Vector2.up, squirrel.PhObj.ColliderBoundsY, squirrel.PhObj.maskToCollide));

    //        squirrel.PhObj.Jumping = false;
    //        squirrel.ProcessInput(InputSquirrel.timeOfJumpEnded); 
    //    }
    //}
}
