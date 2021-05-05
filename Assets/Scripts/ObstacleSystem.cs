using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSystem : MonoBehaviour{
    [SerializeField] protected GameObject particleGameObject;

    public GameObject ParticleGameObject{
        get => particleGameObject;
        set => particleGameObject = value;
    }

    private void Start(){
        StartOverride();
    }

    private void Update(){
        UpdateOverride();
    }

    protected virtual void StartOverride(){
        particleGameObject.SetActive(false);
    }

    protected virtual void UpdateOverride(){}
    
}