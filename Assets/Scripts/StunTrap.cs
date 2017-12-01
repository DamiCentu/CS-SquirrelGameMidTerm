using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTrap : MonoBehaviour
{
    Animator anim;
    public int damage = 1;
    public float stunTime = 1;
    public LayerMask stunableLayers;
    private bool isActive = true;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isActive) {
            anim.SetTrigger("Close");
            IDamagable damageable = collider.gameObject.GetComponent<IDamagable>();
            if (damageable != null)
            {
                isActive = false;
                damageable.TakeDamage(damage);
            }
            IStunable stunable = collider.gameObject.GetComponent<IStunable>();
            if (stunable != null)
            {
                isActive = false;
                stunable.Stun(stunTime);
            }
        }
    }
}
