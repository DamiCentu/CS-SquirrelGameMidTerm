using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour, IPositionPredictable,IDamagable, IStunable
{
    private IMoventOnAir _onAirMovent;
    private IMoventOnSurface _onMoventSurface;
    private IActions _actions;
    public bool onGround=false;
    public static PlayerBrain instance ;
    internal bool jumping;
    internal bool canJump;
    public float timeToJumpAfterFall;
    internal Transform lastRollingArea;

    internal bool canJumoOnAir;
    internal Rigidbody rb;
    internal CapsuleCollider _capsuleCollider;
    internal SphereCollider _capsuleSphere;
    public int life;
    private Dictionary<String, int> amountBullets=new Dictionary<string, int>();
    private Vector3 _capsuleCenterNormalPosition = new Vector3(0f, 0.08f, 0.13f);
    private Vector3 _capsuleCenterStandUpPosition = new Vector3(0f, 0.16f, 0.07f);

    public Transform HeadPosition;
    public float maxDistanceClimbingArea;
    public LayerMask ClimbableLayers;
    public Transform shootZone;
    public Transform projection;
    public LayerMask layersToProject;
    internal bool rollingArea;
    public ParticleSystem dirt;
    private float _timer;
    private float timeToFall = 0.1f;

    internal bool allowGlide=false;
    private bool isDead = false;
    private bool stayOnTheGround;
    internal bool finishGame=false;
    public int maxLife=100;

    internal PlayerBrain SetPosition(Vector3 vector3)
    {
        this.transform.position = vector3;
        return this;
    }

    internal PlayerBrain Setlife(int v)
    {
        if (life>0) this.life = v;
        return this;
    }

    public void SetDirtParticles(bool value) {
        dirt.gameObject.SetActive(value);
    }

    void LateUpdate()
    {
        if (stayOnTheGround)
        {
            if (Physics.Raycast(this.transform.position, Vector3.down, 0.1f)) {
                onGround = true;
         //       _timer = 0;
           }
        }
        else {
            if (!Physics.Raycast(this.transform.position, Vector3.down, 0.1f)) {
  //              if (_timer > timeToFall)
                   onGround = false;
//                    _timer += Time.deltaTime;
            }
        }
    }

    internal PlayerBrain SetRotation(Quaternion quaternion)
    {
        this.transform.rotation = quaternion;
        return this;
    }


    public void TakeDamage(int damage)
    {
        life -= damage;
        
        if (life <= 0)
        {
            life = 0;
            Die();
        }
        MyUiManager.instance.UpdateLife(  life/ (float)maxLife);
    }

    private void Die()
    {
        isDead = true;
        StateMachine.instance.ChangeStateIfNeeded("Die");
        EventManager.instance.ExecuteEvent("PlayerDie");
    }

    public void Awake() {
        life = maxLife;
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
        rb = this.GetComponent<Rigidbody>();
        _capsuleCollider = this.GetComponent<CapsuleCollider>();
        _capsuleSphere = this.GetComponent<SphereCollider>();
        _timer = timeToFall;
    }

    void Start() {
        EventManager.instance.SubscribeEvent("EnableGlide", EnableGlide);
        CheckPointManager checkpointManager = UnityEngine.Object.FindObjectOfType<CheckPointManager>();
        checkpointManager.CreateEvents();
        EventManager.instance.SubscribeEvent("Roll", SetRolling);
        //   GameManager.instance.LoadCheckPoint();
    }

    private void SetRolling(object[] parameterContainer)
    {
        print("lo setee");

        lastRollingArea = (Transform)parameterContainer[0];
    }

    private void EnableGlide(params object[] parametersWrapper)
    {
        allowGlide = true;
    }

    internal void Roll()
    {
        _capsuleCollider.enabled=false;
        _capsuleSphere.enabled=true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }
    internal void StopRolling()
    {
        _capsuleCollider.enabled = true;
        _capsuleSphere.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
    internal void SetBehabiour(IMoventOnSurface onMoventSurface, IMoventOnAir onAirMovent, IActions action)
    {
        _onMoventSurface = onMoventSurface;
        _onAirMovent = onAirMovent;
        _actions = action;

        _onAirMovent.Active();
        _onMoventSurface.Active();
        _actions.Active();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (_onAirMovent!=null) _onAirMovent.Move();
        if (_onMoventSurface != null) _onMoventSurface.Move();
        if (_actions != null) _actions.Do();

    }

    internal void StandUpPosition()
    {
        _capsuleCollider.direction = 1;//EJE y EN 2 PATAS
        (_capsuleCollider as CapsuleCollider).center = _capsuleCenterStandUpPosition;
    }

    internal void NormalPosition()
    {
        _capsuleCollider.direction = 2; //EJE y EN 4 PATAS
        (_capsuleCollider as CapsuleCollider).center = _capsuleCenterNormalPosition;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 9 )
        {
            stayOnTheGround = true;

           // rb.useGravity = true;
           // onGround = true;
        }
        else if (collision.gameObject.layer == 18) // RollingArea
        {
            rollingArea = true;
            //lastRollingArea = collision.gameObject.transform;
        }
    }

    internal bool NearClimbingArea()
    {

        RaycastHit ray;
        if (Physics.Raycast(transform.position, this.transform.forward, out ray, maxDistanceClimbingArea, ClimbableLayers))
        {
            //print("estoy cerca del area de trepar");
            return true;
        }
        return false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9 ) 
        {
            stayOnTheGround = false; 

    //        if (! Physics.Raycast(this.transform.position, Vector3.down, 0.1f))
      //          onGround = false;
        }
        else if (collision.gameObject.layer == 18 ) // RollingArea
        {
            rollingArea = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 13) //NUt
        {
            AddBullet("Nut");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 14) //HazleNUt
        {
            AddBullet("HazleNut");
            Destroy(collision.gameObject);
        }
    }

    private void AddBullet(string bullet) {
        if (amountBullets.ContainsKey(bullet))
        {
            amountBullets[bullet] += 1;
        }
        else {
            amountBullets[bullet] = 1;
        }
    }

    internal bool HasBullet(string bullet)
    {
        if (amountBullets.ContainsKey(bullet))
        {
            return amountBullets[bullet] > 0;
        }
        return false;
    }

    internal void RemoveBullet(string bullet)
    {
        if (amountBullets.ContainsKey(bullet))
        {
            amountBullets[bullet] -= 1;
        }
    }

    public Vector3 currentDirection()
    {
        return _onMoventSurface.currentDirection();
    }

    public void Stun(float stunTime)
    {
        if (!isDead)
        {
            StateMachine.instance.ChangeStateIfNeeded("Stun",stunTime);
        }
    }
}
