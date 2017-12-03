using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnFalling :MonoBehaviour ,IState {
    public FallingBehaviour _fall;
    public NormalMovent _normalMovent;
    public NoAction _noAction;
    private string name = "Fall";
    private float _timer;
    public float minTimeToGlide = 0.5f;
    /*  bool IState.MatchesRequirements()
      {
          return (!PlayerBrain.instance.onGround && !PlayerBrain.instance.jumping);
      }
      */

    private void Start()
    {
        StateMachine.instance.AddState(this);
    }
    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_normalMovent, _fall, _noAction);
        _timer = 0;
        PlayerAnimator.instance.Fall(true);
    }

    void IState.Finish()
    {
        PlayerAnimator.instance.Fall(false);
    }

    string IState.GetName()
    {
        return name;
    }

    string IState.NextState()
    {
        if (PlayerBrain.instance.NearClimbingArea() && MyInputManager.instance.GetKey("Climb"))
        {
            print("NearClimbingArea" + PlayerBrain.instance.NearClimbingArea());
            return "Climb";

        }
        if (MyInputManager.instance.GetKey("Jump")) {
            if (_timer <= PlayerBrain.instance.timeToJumpAfterFall && PlayerBrain.instance.canJumoOnAir && MyInputManager.instance.GetKey("Jump"))
            {
                PlayerBrain.instance.canJumoOnAir = false;
                return "Jump";
            }
            else if (PlayerBrain.instance.allowGlide && _timer >= minTimeToGlide) {
                return "Glide";
            }
          /*  else if (PlayerBrain.instance.onGround)
            {
                return "Floor";
            }*/
           
        }
        if (PlayerBrain.instance.onGround) {
            return "Floor";
        }
        if (PlayerBrain.instance.rollingArea)
        {
            return "Roll";
        }
        return null;
    }

    void IState.Process()
    {
        _timer += Time.deltaTime;
        if (MyInputManager.instance.GetKey("Sneak") || _normalMovent.currentVelocity < 0.8)
        {
            _normalMovent.ShouldWalk(true);
        }
        else
        {
            _normalMovent.ShouldWalk(false);
        }
        _normalMovent.PreventGettingStuck();
        PlayerAnimator.instance.SetSpeed(_normalMovent.currentVelocity);
    
    }

    void IState.setParam(object param)
    {
    }
}
