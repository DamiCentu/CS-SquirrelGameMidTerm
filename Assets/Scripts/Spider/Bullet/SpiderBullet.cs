using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour { 
    [Header("Damage")]
    public int NormalHitDamage = 15;
    public int PoisonHitDamage = 15;
    public int StunHitDamage = 15;
    public LayerMask damageToLayer;

    [Header("EffectDamage")]
    public int PoisonEffectDamage = 5;
    public int quantityOfEffectsHits;
    public float intervalBetweenEffectsDamage = 2f;

    [Header("TimeStunned")]
    public float TimeStunned = 3f;

    [Header("TimeToDisapear")]
    public float deactivationTime = 10f;

    IBulletStrategy _strategy;
    IDamagable playerDamageable;
    Rigidbody _rb;
    bool _movementActive = false;
    float speed;

    PoisonHitBulletBehaviur _poison;
    NormalHitBulletBehaviur _normal;
    StunHitBulletBehaviur _stun;

    void Start() {
        if(GameManager.instance.Player != null)
            playerDamageable = GameManager.instance.Player.GetComponent<IDamagable>();
        _poison = new PoisonHitBulletBehaviur(/*playerDamageable,*/ PoisonHitDamage, PoisonEffectDamage,intervalBetweenEffectsDamage,quantityOfEffectsHits);
        _stun = new StunHitBulletBehaviur(/*playerDamageable,*/ StunHitDamage,TimeStunned);
        _normal = new NormalHitBulletBehaviur(/*playerDamageable,*/ NormalHitDamage);
        _rb = GetComponent<Rigidbody>();
    }

    public SpiderBullet setType(SpiderBehaviur.spiderType type) {
        switch (type) {
            case SpiderBehaviur.spiderType.Poison:
                _strategy = _poison;
                break;
            case SpiderBehaviur.spiderType.Stun:
                _strategy = _stun;
                break;
            case SpiderBehaviur.spiderType.Normal:
                _strategy = _normal;
                break;
        }
        return this;
    }

    public SpiderBullet setSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public SpiderBullet setPosition(Vector3 pos) {
        transform.position = pos;
        return this;
    } 

    float _time = 0f;

    void Update() {
        _time += Time.deltaTime; 
    } 

    void FixedUpdate() {
        if (!_movementActive)
            return;

        if (_time > deactivationTime)
            SpiderBulletManager.instance.ReturnBulletToPool(this); 

        _rb.position += transform.forward * speed * Time.deltaTime;
    }

    public SpiderBullet setDirection(Vector3 forward) {
        transform.forward = forward;
        return this;
    }

    public void activeMovement() {
        _movementActive = true;
    }

    public void deactivateMovement() {
        _movementActive = false;
    }

    public void Initialize() {
        _time = 0f;
    }

    public void Dispose() {
        deactivateMovement();
       // _rb.position = new Vector3(0f, 1000f, 0f);
    }

    public static void InitializeBullet(SpiderBullet bulletObj) {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Initialize();
    }

    public static void DisposeBullet(SpiderBullet bulletObj) {
        bulletObj.Dispose();
        bulletObj.gameObject.SetActive(false);
    }

    public float radiusOfOverlap = .5f;
    Vector3 posOverlap = new Vector3();

    void OnTriggerEnter(Collider c) {
        if (playerDamageable == null)
            playerDamageable = GameManager.instance.Player.GetComponent<IDamagable>();

        var o = Physics.OverlapSphere(transform.position, radiusOfOverlap, damageToLayer);
        if(o.Length > 0) {
            posOverlap = transform.position;
            if (_strategy != null)
                _strategy.playerHitted(playerDamageable);
        }

        //if (c.gameObject.layer == 10) { //Squirrel 
        //    if(_strategy != null)
        //        _strategy.playerHitted(playerDamageable); //comentado hasta que se implemente idamageable en squirrel
        //    //Debug.Log("bla");
        //}
        if(c.gameObject.layer != 15 && c.gameObject.layer != 16 && c.gameObject.layer != 19 ) //spider //spiderBullet //checkpoint
            SpiderBulletManager.instance.ReturnBulletToPool(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posOverlap, radiusOfOverlap);
    }
}
