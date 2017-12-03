using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnJump : MonoBehaviour,IState
{
    public JumpBehaviour _jump;
    public NormalMovent _normalMovent;
    public NoAction _noAction;
    private float _timer;
    private string _name = "Jump";
    public float maxTimeToMakeShortJump;
    private bool allowLongJump = false;

    void IState.setParam(object param)
    {
    }
    private void Start()
    {
        StateMachine.instance.AddState(this);
        EventManager.instance.SubscribeEvent("EnableLongJump", AllowLongJump);                 
    }

    public void AllowLongJump(params object[] parametersWrapper)
    {

        allowLongJump = true;
    }

    void IState.Begin()
    {
        PlayerBrain.instance.rb.useGravity = false;
        PlayerBrain.instance.canJump = false;
        PlayerBrain.instance.canJumoOnAir= false;
        _timer = 0;
        PlayerBrain.instance.SetBehabiour(_normalMovent, _jump, _noAction);
        PlayerBrain.instance.jumping = true;
        PlayerAnimator.instance.Jump(true);
        PlayerBrain.instance.NormalPosition();
        //PlayerBrain.instance.onGround=false;
    }

    void IState.Finish()
    {
        PlayerBrain.instance.rb.useGravity = true;
        PlayerAnimator.instance.Jump(false);
        PlayerBrain.instance.jumping = false;
        
    }

    void IState.Process()
    {
        _timer += Time.deltaTime;
        if ((!MyInputManager.instance.GetKey("Jump") && _timer <= maxTimeToMakeShortJump)|| !allowLongJump)
        {
            _jump.SetShortJump();

        }
        PlayerAnimator.instance.SetSpeed(_normalMovent.currentVelocity);
    }
    string IState.NextState()
    {
        if (PlayerBrain.instance.NearClimbingArea() && MyInputManager.instance.GetKey("Climb"))
        {
            return "Climb";

        }
        if (_timer >= _jump.currentJumpDuration)
        {
            PlayerBrain.instance.rb.velocity = Vector3.zero;
            return "Fall";
        }
        return null;
    }

    string IState.GetName()
    {
        return _name;
    }
}
