using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletStrategy {
    void playerHitted(IDamagable player);
}
