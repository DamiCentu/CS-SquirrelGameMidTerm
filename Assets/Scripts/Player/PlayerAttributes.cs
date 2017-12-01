using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    public float runningSpeed;
    public float glideSpeed;
    public static PlayerAttributes instance;
    internal Rigidbody rb;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        rb = this.GetComponent<Rigidbody>();
    }
}
