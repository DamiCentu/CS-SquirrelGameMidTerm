using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonHitBulletBehaviur : IBulletStrategy {

    int _damage;
    int _effectDamage;
    //IDamagable _player;
    float _interval;
    int _quantity;

    public PoisonHitBulletBehaviur(/*IDamagable target,*/int hitDamage, int effectDamage,float intervalBetweenDamage,int quantityOfEffectHits) {
        _damage = hitDamage;
        _effectDamage = effectDamage;
        //_player = target;
        _interval = intervalBetweenDamage;
        _quantity = quantityOfEffectHits;
    }

    public void playerHitted(IDamagable player) {
        //Debug.Log("Poisoned"); 
        GameManager.instance.DoEffectDamageTo(player, _damage, _effectDamage, _interval, _quantity);
    } 
}
