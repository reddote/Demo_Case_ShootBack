using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> : MonoBehaviour where T : MonoBehaviour{
    protected Queue<T> ReusableInstances = new Queue<T>();

    protected virtual void Start(){
        AddTToStackInit();
    }

    //add all objects to stack in the beginning
    protected virtual void AddTToStackInit(){
        T[] _temp = transform.GetComponentsInChildren<T>();
        foreach (var _t in _temp){
            ReusableInstances.Enqueue(_t);
            _t.gameObject.SetActive(false);
        }
    }

    //object spawner and adjuster
    protected virtual void Spawner(T poolObject){
        poolObject.gameObject.SetActive(true);
        poolObject.transform.SetParent(null);
    }

    //Pop from object pool
    protected virtual T StackPop(){
        var T = ReusableInstances.Dequeue();
        Spawner(T);
        return T;
    }

    //Push to object pool
    protected virtual void ReturnTStack(T poolObject){
        ReusableInstances.Enqueue(poolObject);
        poolObject.transform.SetParent(transform);
        poolObject.transform.localPosition= Vector3.zero;
        poolObject.gameObject.SetActive(false);
    }
    
}