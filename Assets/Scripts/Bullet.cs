using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour{
    [FormerlySerializedAs("_speed")] [SerializeField] protected float speed;
    protected float damage;
    [SerializeField] protected BulletPooling bulletPool;

    public BulletPooling BulletPool{
        get => bulletPool;
        set => bulletPool = value;
    }

    public float Speed{
        get => speed;
        set => speed = value;
    }

    public float Damage{
        get => damage;
        set => damage = value;
    }

    private void Start(){
        bulletPool = GetComponentInParent<BulletPooling>();
        StartOverride();
    }

    private void Update(){
        UpdateOverride();
    }

    protected virtual void StartOverride(){}
    
    protected virtual void UpdateOverride(){
        transform.position -= Vector3.forward * (Time.deltaTime * speed);
    }
}