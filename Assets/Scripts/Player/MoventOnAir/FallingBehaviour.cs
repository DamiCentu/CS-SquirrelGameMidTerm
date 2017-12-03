using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : MonoBehaviour, IMoventOnAir
{
    public float fallIntensity;
    public Rigidbody rb;

    FallingBehaviour(Rigidbody rb, float force)
    {
        fallIntensity = force;
        this.rb = rb;
    }

    void IMoventOnAir.Active()
    {
  //      rb.useGravity = false;
    }
    void IMoventOnAir.Move()
    {
        print(rb.velocity);
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        rb.AddForce(Vector3.down * fallIntensity, ForceMode.Force);
        //rb.MovePosition(Vector3.down *fallIntensity*Time.deltaTime);
    }
}
