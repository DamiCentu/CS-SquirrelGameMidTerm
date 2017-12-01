using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRolling :MonoBehaviour, IState {

    private string _name = "Roll";
    public WithoutMoventOnAir _withoutMoventOnAir;
    public Roll _roll;
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
        _roll.setRollingArea(PlayerBrain.instance.lastRollingArea);
        PlayerBrain.instance.SetBehabiour(_roll, _withoutMoventOnAir, _noAction);
        PlayerAnimator.instance.Roll(true);
        PlayerBrain.instance.SetDirtParticles(true);
    }

    void IState.Finish()
    {
        PlayerAnimator.instance.Roll(false);
        PlayerBrain.instance.SetDirtParticles(false);
        //   PlayerBrain.instance.StopRolling();
    }

    void IState.Process()
    {

        PlayerAnimator.instance.SetSpeed(1);
    }


    string IState.NextState()
    {
        if (PlayerBrain.instance.onGround)
        {
            return "Floor";

        }

        /*     else if (!PlayerBrain.instance.rollingArea)
             {
                 return "Fall";
             }
             */
        return null;
    }

    string IState.GetName()
    {
        return _name;
    }
}
