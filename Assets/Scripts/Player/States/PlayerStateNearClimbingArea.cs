using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNearClimbingArea : MonoBehaviour, IState {
    private string _name = "NearClimbingArea";
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
        PlayerBrain.instance.rb.useGravity = true;
        PlayerBrain.instance.SetBehabiour(_normalMovent, _withoutMoventOnAir, _noAction);
        PlayerAnimator.instance.NearClimbingArea(true);
      
        PlayerBrain.instance.StandUpPosition();
    }


    void IState.Finish()
    {
        PlayerAnimator.instance.NearClimbingArea(false);
    }

    string IState.GetName()
    {
        return _name;
    }

    string IState.NextState()
    {
        if (MyInputManager.instance.GetKey("Climb")&& PlayerBrain.instance.NearClimbingArea())
        {
            return "Climb";

        }
        if (!PlayerBrain.instance.NearClimbingArea())
        {
            return "Floor";

        }
        if (MyInputManager.instance.GetKey("Jump") && PlayerBrain.instance.canJump)
        {
            return "Jump";
        }
        else if (!PlayerBrain.instance.onGround)
        {
            return "Fall";
        }

        return null;
    }

    void IState.Process()
    {

    }
}
