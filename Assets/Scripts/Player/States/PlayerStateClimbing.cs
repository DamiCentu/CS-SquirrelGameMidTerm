using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateClimbing : MonoBehaviour, IState
{
    private string _name = "Climb";
    public WithoutMoventOnAir _withoutMoventOnAir;
    public MoventOnTree _climbMovent;
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
        PlayerBrain.instance.rb.useGravity = false;
        PlayerBrain.instance.SetBehabiour(_climbMovent, _withoutMoventOnAir, _noAction);
        PlayerAnimator.instance.Climb(true);
        PlayerBrain.instance.StandUpPosition();
    }


    void IState.Finish()
    {
        PlayerBrain.instance.rb.useGravity = true;
        PlayerAnimator.instance.Climb(false);
    }

    string IState.GetName()
    {
        return _name;
    }

    void IState.Process()
    {
        //print(_climbMovent.currentVelocity);
        PlayerAnimator.instance.SetClimbSpeed(_climbMovent.currentVelocity);
    }

    string IState.NextState()
    {
        if (!(PlayerBrain.instance.NearClimbingArea() && MyInputManager.instance.GetKey("Climb")))
        {
            return "Fall";
        }

        return null;
    }
}
