using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateClimbing : State<InputSquirrel> { 
    public StateClimbing(FSMSquirrel fsm) : base(fsm, "Climbing") { }

    SquirrelBehaviur squirrel;
    Vector2 direction;

    //public override void OnEnter() { 
    //    squirrel = (SquirrelBehaviur)((FSMSquirrel)this.Fsm).squirrel;
    //    squirrel.PhObj.Climbing = true;

    //    if (squirrel.LookingRight) direction = Vector2.right;
    //    else direction = Vector2.left;
        
    //}

    //public override void OnUpdate() {
    //    float vertical = Input.GetAxisRaw("Vertical");

    //    var positioTop = new Vector2(squirrel.PhObj.ColliderBoundsCenter.x, squirrel.PhObj.ColliderBoundsCenter.y + squirrel.PhObj.ColliderBoundsY);
    //    var positionBottom = new Vector2(squirrel.PhObj.ColliderBoundsCenter.x, squirrel.PhObj.ColliderBoundsCenter.y - squirrel.PhObj.ColliderBoundsY);

    //    if (!DetectCollision.CollideTo(positionBottom, direction, squirrel.PhObj.ColliderBoundsX, squirrel.MaskOfClimbObj) && vertical < 0f
    //        || !DetectCollision.CollideTo(positioTop, direction, squirrel.PhObj.ColliderBoundsX, squirrel.MaskOfClimbObj) && vertical > 0f)  
    //            squirrel.PhObj.ComputeVelocityY(0f);
    //    //squirrel.PhObj.ComputeVelocityY(vertical);
    //    else squirrel.PhObj.ComputeVelocityY(vertical);

    //    if (!MyInputManager.instance.GetKey("Climb"))
    //        squirrel.ProcessInput(InputSquirrel.unpressedClimb);
    //}

    //public override void OnExit() {
    //    squirrel.PhObj.Climbing = false;
    //}
}
