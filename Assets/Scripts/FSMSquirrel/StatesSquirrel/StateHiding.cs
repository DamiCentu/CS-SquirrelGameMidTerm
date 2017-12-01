using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHiding : State<InputSquirrel> {
    public StateHiding(FSMSquirrel fsm) : base(fsm, "Hiding") { }

    //SquirrelBehaviur _squirrel;
    //BoxCollider2D _col;
    ////Vector3 _positionToReturn;

    //public override void OnEnter() { 
    //    _squirrel = (SquirrelBehaviur)((FSMSquirrel)this.Fsm).squirrel;
    //    //_positionToReturn = _squirrel.transform.position;
    //    _col = _squirrel.GetComponent<BoxCollider2D>();
    //    _col.enabled = false;
    //    _squirrel.PhObj.Hiding = true;
    //}

    //public override void OnUpdate() {
    //    //_squirrel.PhObj.ComputeVelocityY(0f);
    //    if (MyInputManager.instance.GetKeyUp("Hide"))
    //        _squirrel.ProcessInput(InputSquirrel.onHideSpotAndPressedHideKey);
    //}

    //public override void OnExit() {
    //    //_squirrel.PhObj.movePositionToHide(_positionToReturn);
    //    _col.enabled = true;
    //    _squirrel.PhObj.Hiding = false;
    //}
}
