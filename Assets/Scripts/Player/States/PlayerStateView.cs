using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateView : MonoBehaviour, IState
{

    public WithoutMoventOnAir _noMoventOnAir;
    public NoMovent _noMovent;
    public NoAction _noAction;
    private float stunTime = Mathf.Infinity;
    private void Start()
    {
        StateMachine.instance.AddState(this);
    }


    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_noMovent, _noMoventOnAir, _noAction);
        CameraManager.instance.ChangeToFirstPerson();

    }
    public void Finish()
    {
        CameraManager.instance.ChangeToThirdPerson();
    }

    public void Process()
    {
    }

    public string NextState()
    {
        if (!MyInputManager.instance.GetKey("Look"))
        {
            return "Floor";
        }
        return null;
    }

    public string GetName()
    {
        return "Look";
    }

    public void setParam(object param)
    {
    }
}
