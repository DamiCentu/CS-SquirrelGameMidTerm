using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNut : MonoBehaviour, IShootable
{
    public Nut prefabNut;
    private float nutShootVelocity;
    bool canShoot = true;


    public ShootNut(Nut prefabNut, float nutShootVelocity)
    {
        this.prefabNut = prefabNut;
        this.nutShootVelocity = nutShootVelocity;
    }

    void IShootable.Shoot()
    {
        
        if ( MyInputManager.instance.GetAxis("Shoot")>0&& PlayerBrain.instance.HasBullet("Nut") && canShoot)
        {
            canShoot = false;
            //   Vector3 sp = Camera.main.WorldToScreenPoint(transform.position + offsetShoot);
            // Vector3 dir = (Input.mousePosition - sp).normalized;
            //  dir.z = transform.position.z;
            print(PlayerBrain.instance.shootZone.position);
            if (prefabNut == null) print("Che es null");
            Nut nut = Instantiate(prefabNut, PlayerBrain.instance.shootZone.position, PlayerBrain.instance.shootZone.rotation);
            if (nut == null) print("Che es null nut");
            nut.addImpulse(nutShootVelocity);
            PlayerBrain.instance.RemoveBullet("Nut");
            //listLinearNut.RemoveAt(0);
        }
        if (MyInputManager.instance.GetAxis("Shoot") == 0) {
            canShoot = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
