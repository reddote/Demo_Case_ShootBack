using System;
using UnityEngine;

public class NpcController : CharacterControllerScript{
    [SerializeField] protected float health;
    [SerializeField] protected GameObject particleGameObject;
    private Transform _player;
    
    protected override void OnTriggerEnter(Collider other){
        base.OnTriggerEnter(other);
        if (other.CompareTag("Bullet")){
            var _temp = other.GetComponent<Bullet>();
            HealthModifier(_temp.Damage);
            
            //if pool is not active player dead can cause this 
            //so fired bullets will disable instead of returning pool
            if (other.GetComponentInParent<BulletPooling>()){
                other.GetComponentInParent<BulletPooling>().ReturnStackAccessor(_temp);
            } else{
                other.gameObject.SetActive(false);
            }
        }
    }

    protected override void StartOverride(){
        base.StartOverride();
        Initialize();
    }

    protected virtual void Initialize(){
        _player = GameObject.FindWithTag("Player").transform;
        particleGameObject.SetActive(false);
    }

    protected override void UpdateOverride(){
        base.UpdateOverride();
        KillChecker();
        MoveTowardsToPlayer();
    }

    //npc get close enough player will shoot and kill the player.
    protected virtual void MoveTowardsToPlayer(){
        if (Mathf.Abs(transform.position.z - _player.transform.position.z) <= 0.2f){
            transform.LookAt(_player);
            particleGameObject.SetActive(true);
            LevelController.instance.isFinished = true;
            LevelController.instance.isPlayerDead = true;
        }
    }

    //every dead trigger updateUI event from gameevents
    protected virtual void KillChecker(){
        if (health <= 0){
            GameEventSystem.instance.UpdateUI();
            Destroy(gameObject);
        }
    }

    protected void HealthModifier(float modifier){
        health -= modifier;
    }

    //if all npc is dead game is finished
    //it will check 1 cause last npc check this method before destroy so 1 means 0 elements.
    private void OnDestroy(){
        if (transform.parent.childCount <= 1){
            LevelController.instance.isFinished = true;
        }
    }
}