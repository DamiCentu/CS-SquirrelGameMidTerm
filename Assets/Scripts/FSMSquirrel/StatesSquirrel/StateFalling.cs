using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFalling : State<InputSquirrel> {
    public StateFalling(FSMSquirrel fsm) : base(fsm, "Falling") { }

    //SquirrelBehaviur _squirrel;

    //public override void OnEnter() { 
    //    _squirrel = (SquirrelBehaviur)((FSMSquirrel)this.Fsm).squirrel;
    //    _squirrel.PhObj.Falling = true;
    //}
    //public override void OnUpdate() {
    //    if (_squirrel.PhObj.Grounded)
    //        _squirrel.ProcessInput(InputSquirrel.floorCollision);
    //}

    //public override void OnExit() {
    //    _squirrel.PhObj.Falling = false;
    //}
}
