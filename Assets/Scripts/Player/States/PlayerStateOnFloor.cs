using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnFloor :MonoBehaviour, IState {

    private string _name="Floor";
    public WithoutMoventOnAir _withoutMoventOnAir;
    public NormalMovent _normalMovent;
    public NoAction _noAction;

    private void Start()
    {
        StateMachine.instance.AddState(this);
    }
    void IState.setParam(object param)
    {
    }
    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_normalMovent, _withoutMoventOnAir, _noAction);
        PlayerAnimator.instance.Walk(true);
        PlayerBrain.instance.canJumoOnAir = true;
    }

    void IState.Finish()
    {
        PlayerAnimator.instance.Walk(false);
        PlayerBrain.instance.SetDirtParticles(false);
    }

    void IState.Process()
    {
        PlayerBrain.instance.SetDirtParticles(_normalMovent.currentVelocity > 0);
        PlayerAnimator.instance.SetSpeed((_normalMovent.velocity/ _normalMovent.runningVelocity) * _normalMovent.currentVelocity);
        if (!MyInputManager.instance.GetKey("Jump"))
        {
            PlayerBrain.instance.canJump = true;
        }
        if (MyInputManager.instance.GetKey("Sneak") || _normalMovent.currentVelocity < 0.8)
        {
            _normalMovent.ShouldWalk(true);
        }

        else {
            _normalMovent.ShouldWalk(false);
        }
    }

        string IState.NextState()
    {
        if (PlayerBrain.instance.rollingArea) {
            return "Roll";
        }
     /*   if (MyInputManager.instance.GetAxis("Aim")>0.5 )
        {
            return "Shoot";
        }*/
        else if (MyInputManager.instance.GetKey("Jump") && PlayerBrain.instance.canJump)
        {
            return "Jump";
        }
        else if (!PlayerBrain.instance.onGround)
        {
            return "Fall";
        }
        else if (PlayerBrain.instance.NearClimbingArea()) {
            print("Cerca dle areeea");
            return "NearClimbingArea";

        }
        else if (MyInputManager.instance.GetKey("Look") )
        {
            return "Look";
        }
        else if (PlayerBrain.instance.finishGame)
        {
            return "Stay";
        }
        return null;
    }

    string IState.GetName()
    {
        return _name;
    }
}   
