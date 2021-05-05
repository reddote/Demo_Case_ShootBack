using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEventSystem : MonoBehaviour{
    public static GameEventSystem instance;

    private void Awake(){
        #region Singleton
        if (instance != null)
        {
            return;

        }
        instance = this;
        #endregion
    }

    public UnityEvent afterGameFinished;//Unity Event for finishing game events

    public event Action UIUpdater;//Event for updating UI

    public void UpdateUI(){
        UIUpdater?.Invoke();
    }
}