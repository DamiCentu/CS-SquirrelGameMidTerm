using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PhysicsObject))]
public class SquirrelBehaviur : MonoBehaviour {

    public LayerMask MaskOfClimbObj;

    FSMSquirrel _fsm;
    //PhysicsObject _phObj;
    StateClimbing _climbing;
    StateHiding _hiding;

    bool _lookingRight = false;
    //Vector3 _positionToHide;

   // public PhysicsObject PhObj { get { return _phObj; } }
    public bool LookingRight { get { return _lookingRight; } }
    //public Vector3 PositionToHide { get { return _positionToHide; } }

   // float _timerTJ =0;
   // float _timeToJump = 0.2f;

    void Awake() {
        //_phObj = GetComponent<PhysicsObject>();
        setFSM();
    }

    void Start () {
    }

    void FixedUpdate() {
        _fsm.Update();
        //_timerTJ += Time.deltaTime;
        //Debug.Log(_fsm.Current);
        float horizontal = Input.GetAxisRaw("Horizontal");


        if (horizontal > 0) _lookingRight = true; //esto lo hago para cuanto no sea 0 mantenga la vista para algun lado
        else if (horizontal < 0) _lookingRight = false;

        

        if (_lookingRight) climbIfCan(Vector2.right);
        else climbIfCan(Vector2.left);

         // if (Input.GetKeyDown(KeyCode.Space)){
        //if(InputManager.GetKeyDown("Jump"))
        if (MyInputManager.instance.GetKeyDown("Jump")) {// && _timerTJ > _timeToJump) {
            //_timerTJ = 0f;
            ProcessInput(InputSquirrel.jumpPressed);
         }

        if(_fsm.Current != _climbing && _fsm.Current != _hiding) { 
           // _phObj.ComputeVelocityX(horizontal);
        }

        //if (Input.GetKey(KeyCode.LeftShift) {
        //    if(lookingRight)  // hacer que agarre para la derecha
        //}
    }

    //bool canClimb;

    void climbIfCan (Vector2 direction) {
        //Debug.DrawRay(_phObj.ColliderBoundsCenter, new Vector2(direction.x + _phObj.ColliderBoundsX, direction.y) , Color.yellow, Time.deltaTime);
        //Debug.DrawLine(_phObj.ColliderBoundsCenter, new Vector2(_phObj.ColliderBoundsCenter.x + _phObj.ColliderBoundsX, _phObj.ColliderBoundsCenter.y), Color.yellow, Time.deltaTime);
       if (MyInputManager.instance.GetKey("Climb")) {  // hacer que agarre para la derecha
           // if (DetectCollision.CollideTo(_phObj.ColliderBoundsCenter, direction, _phObj.ColliderBoundsX, MaskOfClimbObj)) {
              //  if (_phObj.Jumping) { 
               //     _phObj.Jumping = false;
                    //ProcessInput(InputSquirrel.timeOfJumpEnded);
                }
                //ProcessInput(InputSquirrel.wallNearAndClimbPressed); 
            //}
       // }
    }

    void setFSM() {
        _fsm = new FSMSquirrel();
        _fsm.squirrel = this;

        var onFloor = new StateOnFloor(_fsm);
        var jumping = new StateJumping(_fsm);
        _climbing = new StateClimbing(_fsm);
        var falling = new StateFalling(_fsm);
        _hiding = new StateHiding(_fsm);


        //onFloorTo
        onFloor.transitions[InputSquirrel.jumpPressed] = jumping;
        onFloor.transitions[InputSquirrel.wallNearAndClimbPressed] = _climbing;
        onFloor.transitions[InputSquirrel.leavePlatform] = falling;
        //onFloor.transitions[InputSquirrel.ReachedDestination] = shootingCurve;
        onFloor.transitions[InputSquirrel.onHideSpotAndPressedHideKey] = _hiding;

        //jumpingTo
        jumping.transitions[InputSquirrel.timeOfJumpEnded] = falling;
        jumping.transitions[InputSquirrel.wallNearAndClimbPressed] = _climbing;
        //jumping.transitions[InputSquirrel.TargetSeen] = planning;

        //climbingTo
        _climbing.transitions[InputSquirrel.jumpPressed] = jumping;
        _climbing.transitions[InputSquirrel.unpressedClimb] = falling;

        //fallingTo
        falling.transitions[InputSquirrel.floorCollision] = onFloor;
        falling.transitions[InputSquirrel.wallNearAndClimbPressed] = _climbing;

        //hidingTo
        _hiding.transitions[InputSquirrel.onHideSpotAndPressedHideKey] = onFloor;

        _fsm.SetInitial(falling);

    }

    public void ProcessInput(InputSquirrel input) {
        _fsm.ProcessInput(input);
    }

    void OnTriggerStay2D(Collider2D c) {
        if (MyInputManager.instance.GetKeyDown("Hide") && c.gameObject.layer == 11) {//hideSpots 
            //_positionToHide = c.gameObject.transform.position;
            ProcessInput(InputSquirrel.onHideSpotAndPressedHideKey);
            
        }
    }
}
