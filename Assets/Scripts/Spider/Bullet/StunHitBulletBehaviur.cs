using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunHitBulletBehaviur : IBulletStrategy { 

    int _damage;
    //IDamagable _player;
    float _stunTime;

    public StunHitBulletBehaviur(/*IDamagable target,*/int hitDamage, float stunTime) {
        _damage = hitDamage;
        //_player = target;
        _stunTime = stunTime;
    }

    public void playerHitted(IDamagable player) {
        //Debug.Log("Stunned");
        GameManager.instance.StunAndDamageTo(player, _damage, _stunTime);
    }
}
