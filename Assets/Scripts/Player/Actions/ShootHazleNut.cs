using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHazleNut : IShootable {
    private float hazleShootVelocity;
    private HazelNut prefabHazleNut;

    public ShootHazleNut(HazelNut prefabHazleNut, float hazleShootVelocity)
    {
        this.prefabHazleNut = prefabHazleNut;
        this.hazleShootVelocity = hazleShootVelocity;
    }

    void IShootable.Shoot()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
