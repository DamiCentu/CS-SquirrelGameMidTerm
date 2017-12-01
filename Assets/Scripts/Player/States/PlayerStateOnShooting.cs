using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnShooting : MonoBehaviour, IState {
    public WithoutMoventOnAir _withoutMoventOnAir;
    public NoMovent _noMovent;
    public Shoot _shoot;
    public Transform player;
    //public CameraController camController;

    void IState.setParam(object param)
    {
    }
    private void Start()
    {
        StateMachine.instance.AddState(this);
    }

    void IState.Begin()
    {

        PlayerBrain.instance.SetBehabiour(_noMovent, _withoutMoventOnAir,_shoot);
        PlayerAnimator.instance.NearClimbingArea(true);
       // camController.MakeTransitionTo(CameraController.TransitionType.ChangeToShooting);
        PlayerBrain.instance.StandUpPosition();
    }


    void IState.Finish()
    {
        PlayerAnimator.instance.NearClimbingArea(false);
        //camController.MakeTransitionTo(CameraController.TransitionType.ChangeToThird);
    }

    string IState.GetName()
    {
        return "Shoot";
    }

    string IState.NextState()
    {
        if (MyInputManager.instance.GetAxis("Aim")==0) {
            return "Floor";
        }
        return null;
    }

    void IState.Process()
    {
        Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 rotateDirection_target = new Vector3(cameraForward.x, player.forward.y, cameraForward.z);
        player.rotation = Quaternion.LookRotation(rotateDirection_target);
    }
}
