using UnityEngine;

public class WeaponSystems : MonoBehaviour{
    [SerializeField] protected int weaponID;
    [Header("100/fireRate = fire per second")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected float damage;
    [SerializeField] protected BulletPooling bulletPool;
    private float _tempFireRate = 0;

    protected void Start(){
        StartOverride();
    }
    
    protected void Update(){
        UpdateOverride();
    }

    protected virtual void Fire(){
        _tempFireRate -= Time.deltaTime;
        if (_tempFireRate <= 0){
            bulletPool.Shoot(damage);
            _tempFireRate = 100 / fireRate;
        }
    }

    protected virtual void StartOverride(){
        _tempFireRate = 100 / fireRate;
    }

    protected virtual void UpdateOverride(){
       Fire();
    }
}