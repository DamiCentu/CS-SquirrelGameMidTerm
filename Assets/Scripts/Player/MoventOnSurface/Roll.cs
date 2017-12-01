using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour, IMoventOnSurface
{
    // public float _runningVelocity;
    //   private float _sneakVelocity;
 //   public float runningVelocity;
   // public float walkVelocity;
    public float velocity;
    //internal float currentVelocity;
    public Rigidbody rb;
    public Transform playerTransform;
    private Transform platformTransform;
    public LayerMask LayersRollable;

    internal void setRollingArea(Transform rollingArea)
    {
        platformTransform = rollingArea;
    }

    public Vector3 _lastDirection { get; private set; }

    void IMoventOnSurface.Active()
    {
    /*    RaycastHit rayInfo;
        if (Physics.Raycast(playerTransform.position, Vector3.down, out rayInfo, 10, LayersRollable))
        {
            platformTransform = rayInfo.collider.transform;

        }
        else {
            throw new System.Exception("Se esta intentando hace un roll, pero la superficie de abajo no es rolleable");
        }
        */
        //this.transform.rotation = cameraTransform.transform.rotation;
    }
    void IMoventOnSurface.Move()
    {

           Vector3 platformForward = platformTransform.TransformDirection(Vector3.right);
        //Vector3 platformForward = platformTransform.forward;
        //platformForward.y = 0;

        float ver = 1;
        float hor = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(ver, hor);
        _lastDirection = vel.normalized;

        Vector3 rotateDirection_target = (hor * new Vector3(platformForward.z, 0, -platformForward.x)) + 1 * platformForward;

        Vector3 rotateDirection_normal = (rotateDirection_target + playerTransform.forward).normalized;
        Vector3 TEMP_rotateDirection_facing = platformForward;

        if (rotateDirection_normal != Vector3.zero)
            playerTransform.rotation = Quaternion.LookRotation(rotateDirection_normal);

        Vector3 asd = rotateDirection_normal.normalized * velocity * Time.deltaTime;
        asd.y = 0;
        rb.MovePosition(asd + rb.position);
        //rb.AddForce(Vector3.down*100, ForceMode.Force);

    }

    Vector3 IPositionPredictable.currentDirection()
    {
        return _lastDirection;
    }
}
