using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHitBulletBehaviur : IBulletStrategy { 

    int _damage;
    //IDamagable _player;

    public NormalHitBulletBehaviur(/*IDamagable target,*/int hitDamage) {
        _damage = hitDamage; 
        //_player = target;
    }

    public void playerHitted(IDamagable player) {
        //Debug.Log("NormalHit");
        GameManager.instance.DoDamageTo(player, _damage);
    }
}
