using System;
using UnityEngine;

public class BulletForSmg : Bullet{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Npc")){//we want to return pool after hit something 
            bulletPool.ReturnStackAccessor(GetComponent<Bullet>());
        }
    }
}