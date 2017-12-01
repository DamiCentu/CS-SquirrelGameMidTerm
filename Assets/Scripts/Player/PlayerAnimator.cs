using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private Animator _animator;
    public static PlayerAnimator instance=null;
    private void Awake()
    {
        if (instance == null)
        {
            _animator = GetComponent<Animator>();
            instance = this;
        }

    }

    internal void StartDying()
    {
        _animator.Play("Dead");
    }

    internal void Jump(bool state)
    {
        _animator.SetBool("IsJumping", state);
    }

    internal void Stun(bool state)
    {
        _animator.SetBool("IsStunning", state);
    }

    internal void NearClimbingArea(bool state)
    {
        _animator.SetBool("ShootingMode", state);
    }

    internal void Climb(bool state)
    {
        _animator.SetBool("IsClimbing", state);
    }

    internal void Roll(bool state)
    {
        _animator.SetBool("IsRolling", state);
    }

    internal void Walk(bool state)
    {
        _animator.SetBool("OnGround", state);
    }

    internal void SetClimbSpeed(float speed)
    {
        _animator.SetFloat("ClimbSpeed", speed);
    }

    internal void Fall(bool state)
    {
        _animator.SetBool("isFalling", state);
    }

    internal void Glide(bool state)
    {
        _animator.SetBool("IsFlying", state);
    }
    internal void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
}
