using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideBehaviour : MonoBehaviour,IMoventOnAir
{
    public float initialGlideIntensity=2;
    public float glideIntensity;
    public Rigidbody rb;

    void IMoventOnAir.Active()
    {
       // rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * initialGlideIntensity, ForceMode.Force);
    }
    private void Start()
    {
 
    }
    /* GlideBehaviour(Rigidbody rb, float force)
     {
         _glideIntensity = force;
         _rb = rb;
     }*/
    void IMoventOnAir.Move()
    {
        rb.AddForce(Vector3.down * glideIntensity, ForceMode.Force);
    }

}
