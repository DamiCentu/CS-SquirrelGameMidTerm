using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovent : MonoBehaviour, IMoventOnSurface
{
    // public float _runningVelocity;
    //   private float _sneakVelocity;
    public float runningVelocity;
    public float walkVelocity;
    public float velocity;
    internal float currentVelocity;
    public Rigidbody rb;
    public Transform playerTransform;
    public Transform playerCenter;
    public Transform cameraTransform;
    private Vector3 _lastDirection;
    public LayerMask wallLayersMask;
    public LayerMask floorLayersMask;

    void IMoventOnSurface.Active()
    {
        velocity = runningVelocity;  
    }
    void IMoventOnSurface.Move()
    {
        rb.velocity = new Vector3(0, rb.velocity.y,0);
        Vector3 cameraForward = cameraTransform.TransformDirection(Vector3.forward);
        cameraForward.y = 0;

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(ver, hor);

        _lastDirection = vel.normalized;

        if (vel.magnitude > 1) vel= vel.normalized;
        currentVelocity = vel.magnitude;


        // hago un vector ortogonal
        Vector3 rotateDirection_target = hor * new Vector3(cameraForward.z, 0, -cameraForward.x) + ver * cameraForward;

        Vector3 rotateDirection_normal = (rotateDirection_target + playerTransform.forward).normalized;
        Vector3 TEMP_rotateDirection_facing = cameraForward;

        if (rotateDirection_normal != Vector3.zero)
            playerTransform.rotation = Quaternion.LookRotation(rotateDirection_normal);

        //Vector3 TEMP_moveDirection_facing = transform.TransformDirection(new Vector3(hor, 0, ver));

        bool is_moving = ver != 0.0 || hor != 0.0;

       // _animator.SetFloat("Speed", vel.magnitude);

        if (is_moving)
        {
            Vector3 asd = rotateDirection_normal.normalized * velocity * Time.deltaTime;
            asd.y = 0;
           if (ShouldMove()) {
                rb.MovePosition(asd + rb.position);
            }

        }

    }

    public void ShouldWalk(bool v) {

        if (v) {
            velocity = walkVelocity;
            //print("Camino!");
        }
        else {
            //print("corro");
            velocity = runningVelocity;
        }
    }

    internal  void PreventGettingStuck()
    {
        RaycastHit info;
        if(!PlayerBrain.instance.onGround && Physics.Raycast(playerCenter.position, playerTransform.forward, out info,0.3f, floorLayersMask))
        {
            rb.MovePosition( info.normal + rb.position);
        }
   /*     if (!PlayerBrain.instance.onGround && Physics.Raycast(playerCenter.position, playerTransform.forward, out info, 0.3f, wallLayersMask))
        {
            rb.MovePosition(info.normal + rb.position);
        }*/
    }

    public bool ShouldMove()
    {
        return (!Physics.Raycast(playerCenter.position, playerTransform.forward, 0.3f, wallLayersMask) // si no me estoy chocando con una pared default
                && !(!PlayerBrain.instance.onGround && Physics.Raycast(playerCenter.position, playerTransform.forward, 0.3f, floorLayersMask)));// y si no estoy cayendome y chocandome contra algo

    }
    Vector3 IPositionPredictable.currentDirection()
    {
        return _lastDirection;
    }
}
