using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IActions {
    public IShootable currentShootAbility;
    public List<IShootable> ShootsAbility = new List<IShootable>();
    public float nutShootVelocity;
    public float hazleShootVelocity;
    public Nut prefabNut;
    public HazelNut prefabHazleNut;
    private int mode=1;
    public Transform player;
    public enum ShootModes { Nut, HazleNut };
    private ShootModes _currentShootMode;


    void Awake()
    {
        ShootsAbility.Add( new ShootNut(prefabNut,nutShootVelocity));
        ShootsAbility.Add(new ShootHazleNut(prefabHazleNut,hazleShootVelocity));
        currentShootAbility = ShootsAbility[0];
    }


    void IActions.Active()
    {

    }

    void IActions.Do()
    {

        int currentMode = MyInputManager.instance.GetFireMode();
        if (currentMode>=0 && currentMode < ShootsAbility.Count  && currentShootAbility != ShootsAbility[currentMode])
        {
            currentShootAbility= ShootsAbility[currentMode];
        }
        currentShootAbility.Shoot();
    }
}
