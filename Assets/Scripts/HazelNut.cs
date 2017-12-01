using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazelNut : MonoBehaviour {
    //   public bool active = false;
    public Rigidbody rb;


    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }



    public void addImpulse(Vector3 position, Vector2 direction, float forceMultiplier)
    {
        this.gameObject.SetActive(true);
        rb.position = position;
        //print(rb);
        //print(direction);
        //transform.forward = direction;
        //Debug.Log(direction * forceMultiplier);
        rb.AddForce(direction * forceMultiplier, ForceMode.Impulse);
        //rb.velocity = direction * forceMultiplier;
    }
}
