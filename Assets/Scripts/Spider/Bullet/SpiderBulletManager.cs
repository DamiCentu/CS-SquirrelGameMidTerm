using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBulletManager : MonoBehaviour { 

    public SpiderBullet bulletPrefab;

    Pool<SpiderBullet> _bulletPool;

    public static SpiderBulletManager instance { get; private set; }

    void Awake() {
        Debug.Assert(FindObjectsOfType<SpiderBulletManager>().Length == 1);
        if (instance == null)
            instance = this;


        _bulletPool = new Pool<SpiderBullet>(8, BulletFactory, SpiderBullet.InitializeBullet, SpiderBullet.DisposeBullet, true);
    }

    public SpiderBullet giveMeBullet() { 
        return _bulletPool.GetObjectFromPool();
    } 

    public SpiderBullet BulletFactory() {
        return Instantiate<SpiderBullet>(bulletPrefab);
    }

    public void ReturnBulletToPool(SpiderBullet bullet) {
        _bulletPool.DisablePoolObject(bullet);
    } 
}
