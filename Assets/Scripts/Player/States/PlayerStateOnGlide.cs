using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnGlide : MonoBehaviour, IState
{
    public GlideBehaviour _glide;
    public NormalMovent _normalMovent;
    public NoAction _noAction;
    public new string name = "Glide";


    private void Start()
    {
        StateMachine.instance.AddState(this);
    }
    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_normalMovent, _glide, _noAction);
        PlayerAnimator.instance.Glide(true);
    }

    void IState.Finish()
    {
        PlayerAnimator.instance.Glide(false);
    }

    string IState.GetName()
    {
        return name;
    }
    void IState.setParam(object param)
    {
    }
    string IState.NextState()
    {
        if (PlayerBrain.instance.NearClimbingArea() && MyInputManager.instance.GetKey("Climb"))
        {
            return "Climb";

        }
        if (PlayerBrain.instance.onGround)
        {
            return "Floor";
        }
        else if (!MyInputManager.instance.GetKey("Glide"))
        {
            return "Fall";
        }
        else if(PlayerBrain.instance.rollingArea)
        {
            return "Roll";
        }
        return null;
    }

    void IState.Process()
    {

    }
}
