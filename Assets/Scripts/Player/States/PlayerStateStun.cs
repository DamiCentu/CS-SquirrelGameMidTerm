using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateStun : MonoBehaviour, IState
{
    public FallingBehaviour _fall;
    public NoMovent _noMovent;
    public NoAction _noAction;
    private float stunTime=1;
    private void Start()
    {
        StateMachine.instance.AddState(this);
    }


    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_noMovent, _fall, _noAction);
        PlayerAnimator.instance.Stun(true);

    }
    void IState.setParam(object param)
    {
        stunTime = (float)param;
    }
    public void Finish()
    {
        PlayerAnimator.instance.Stun(false);
    }

    public void Process()
    {
        stunTime -= Time.deltaTime;
    }

    public string NextState()
    {
        if (stunTime <= 0) {
            return "Fall";
        }
        return null;
    }

    public string GetName()
    {
        return "Stun";
    }
}
