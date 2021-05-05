using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour{
    [SerializeField] protected float backwardSpeed;
    [Header("Same value as backwardSpeed if you want to disable")]
    [SerializeField] protected float speedAfterHitObstacle;
    private float _tempBackwardSpeed;

    private void Start(){
        StartOverride();
    }

    private void Update(){
        UpdateOverride();
    }

    protected virtual void PlayerMovement(){
        Vector3 _playerPos = new Vector3(SetPositionX(), SetPositionY(), SetPositionZ());
        transform.position += _playerPos;
    }

    protected virtual void SpeedAfterHitObstacles(){
        if (backwardSpeed < _tempBackwardSpeed){
            backwardSpeed += Time.deltaTime;
        }
    }

    protected virtual void StartOverride(){
        _tempBackwardSpeed = backwardSpeed;
    }

    protected virtual void UpdateOverride(){
        PlayerMovement();
        SpeedAfterHitObstacles();
    }

    protected virtual float SetPositionX(){
        return 0;
    }

    protected virtual float SetPositionY(){
        return 0;
    }

    protected virtual float SetPositionZ(){
        float _zSpeed = backwardSpeed * Time.deltaTime;
        return _zSpeed;
    }

    protected virtual void OnTriggerEnter(Collider other){
        if (other.CompareTag("Obstacles")){
            backwardSpeed = speedAfterHitObstacle;
            other.GetComponent<ObstacleSystem>().ParticleGameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
}