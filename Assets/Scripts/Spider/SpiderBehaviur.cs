using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviur : MonoBehaviour, IDamagable {
    [Header("TypeOfSpider")]
    public spiderType type;
    [Header("BulletsProperties")]
    public float timeToShoot;
    public float bulletSpeed;
    [Header("Transforms")]
    public Transform bulletSpawnPositon;
    public Transform playerPosition;
    public Transform playerHurtZone;
    [Header("SpiderProperties")]
    public float maxLife;
    public float radiusToFirePlayer;
    [Header("ShellOfRaycast")]
    public float shell;

    public float radiusOfRandom;

    IPositionPredictable player;

    Animator _anim;

    float _life;
    bool _runningCourutine = false;
    bool _inRange = false;

    Vector3 _posToShoot = new Vector3();
    WaitForSeconds waitToShoot;

    public enum spiderType {
        Normal,
        Stun,
        Poison
    }

    void Start() {
        _anim = GetComponentInChildren<Animator>();
        player = playerPosition.GetComponent<IPositionPredictable>();
        _life = maxLife;
        waitToShoot = new WaitForSeconds(timeToShoot); 
    } 

    

    void Update() {
        if (Vector3.Distance(bulletSpawnPositon.position, playerHurtZone.position) < radiusToFirePlayer) {
            _inRange = true;

            transform.LookAt(new Vector3(playerHurtZone.position.x,transform.position.y , playerHurtZone.position.z)); 

            if (!_runningCourutine) {
                _runningCourutine = true;
                StartCoroutine(shoot());
            }
        }
        else _inRange = false;
    } 

    IEnumerator shoot() {
        yield return waitToShoot;
        while (_inRange && _life > 0f) {

            RaycastHit rh;

            if (!Physics.Raycast(bulletSpawnPositon.position,
                                 playerHurtZone.position - bulletSpawnPositon.position,
                                 out rh, Vector3.Distance(bulletSpawnPositon.position,
                                 playerHurtZone.position) - shell)) {

                _posToShoot = playerHurtZone.position + new Vector3(Random.value, 0f, Random.value) * radiusOfRandom;
                //_posToShoot = playerHurtZone.position;

                var bullet = SpiderBulletManager.instance.giveMeBullet()
                    .setType(type)
                    .setPosition(bulletSpawnPositon.position)
                    .setSpeed(bulletSpeed) 
                    .setDirection(_posToShoot - bulletSpawnPositon.position);
                bullet.activeMovement();
                _anim.SetTrigger("Shoot");
                yield return waitToShoot;
            }
            yield return null;
        }
        _runningCourutine = false;
    }

    public void TakeDamage(int damage) {
        _life -= damage;
        if (_life <= 0)
            Die();
    }

    public void Die() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision c) {
        
    }

    public bool showRadiusOfRandomInPlayer = false;

    void OnDrawGizmos() {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, radiusToFirePlayer);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_posToShoot, 1f);


        if (showRadiusOfRandomInPlayer) { 
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(playerHurtZone.position, radiusOfRandom);
        }
    }
}
