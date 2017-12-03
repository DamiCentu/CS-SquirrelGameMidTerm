using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour, IMoventOnAir {
    public AnimationCurve jumpCurve;
    private float _timer;
    public float longJumpDuration;
    public float longJumpIntensity;
    public float shortJumpDuration;
    public float shortJumpIntensity;
    internal float currentJumpDuration;
    internal float currentJumpIntensity;
    public float timeToJumpAfterFall;
    public Rigidbody rb;
    void IMoventOnAir.Active()
    {
        currentJumpDuration = longJumpDuration;
        currentJumpIntensity = longJumpIntensity;
    //    rb.useGravity = false;
    }
    /*  JumpBehaviour(Rigidbody rb, float force, AnimationCurve jumpCurve, float jumpDuration) {
          this.jumpCurve = jumpCurve;
          this.jumpDuration = jumpDuration;
          jumpIntensity = force;
          this.rb = rb;
      }
      */
    void IMoventOnAir.Move()
    {
        float curveValue = jumpCurve.Evaluate(_timer / currentJumpDuration);
        rb.velocity = new Vector3(rb.velocity.x, curveValue * currentJumpIntensity, rb.velocity.z);
    }

    void Start () {
        _timer = 0;
       // rb.useGravity = false;
    }

    public void SetShortJump()
    {
        currentJumpDuration = shortJumpDuration;
        currentJumpIntensity = shortJumpIntensity;
    }


	void Update () {
        _timer -= Time.deltaTime;
	}
}
