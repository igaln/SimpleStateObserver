using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SB.Observables;


public class UIProfileImageState : SBState
{

    public SBObservable<Texture2D> store = new SBObservable<Texture2D>();

    public Image Image;
  

    public void Awake()
    {
       
        FindObjectOfType<SBStateManager>().registerStore<Texture2D>(store,SBStateManager.SBStateName.ONBOARDING);

        //Bind in initializing
        initState(store);
      
    }

    //Binding Code to A Generic Type
    public override void initState<T>(SBObservable<T> model)
    {
        //Translate Data
        model.dataUpdated += (T obj) =>
        {
            Debug.Log("ok new data " + obj);
           
            //Ugly type casting because of how c# handles
            T newT1 = (T)(object)obj;
            Texture2D newT2 = (Texture2D)(object)newT1;

            Image.sprite = Sprite.Create(newT2, new Rect(0.0f, 0.0f, newT2.width, newT2.height), new Vector2(0.5f, 0.5f), 100.0f);
        };
    }
}
