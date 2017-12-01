using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour { 

    public Rigidbody rb;
      //  public float velocity;
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }


    public void addImpulse( float forceMultiplier)
    {
        this.gameObject.SetActive(true);
        this.rb = this.GetComponent<Rigidbody>();
        // print(rb);
        //print(direction);
        //this.transform.LookAt(position + direction);
        rb.AddForce(this.transform.forward* forceMultiplier, ForceMode.Impulse);
    }
}
