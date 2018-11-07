using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SB.Observables;



public abstract class SBState : MonoBehaviour {

    public abstract void initState<T>(SBObservable<T> model);

}

public class SBStateManager : MonoBehaviour {

    public enum SBStateName
    {
        ONBOARDING
    }


    ObservableList<SBState> sbStates = new ObservableList<SBState>();
    Dictionary<SBStateName,object> sbStores = new Dictionary<SBStateName,object>();

    private void Awake()
    {
        sbStates.ItemAdded += (obj) => {
            Debug.Log("handle State");
         
        };
    }

    private void Start()
    {
        object dataStore = null;
        if(sbStores.TryGetValue(SBStateName.ONBOARDING, out dataStore)) {
            StartCoroutine(TakeSnapshot(100, 100, (SBObservable<Texture2D>)dataStore));
        }
       
    }

    public void registerState(SBState newState) {
        sbStates.Add(newState);
    }

    public void registerStore<T>(SBObservable<T> newStore,SBStateName stateName)
    {
        sbStores.Add(stateName, newStore);
    }


    // JUST FOR TESETING
    public IEnumerator TakeSnapshot(int width, int height,SBObservable<Texture2D> store)
    {
        yield return new WaitForSeconds(4.1F);
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        store.Data = texture;
    }

}
