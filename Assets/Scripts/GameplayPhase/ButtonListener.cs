using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        //Debug.Log(state.NewInteractableState);
        switch (state.NewInteractableState)
        {
            case InteractableState.ProximityState:
                proximityEvent.Invoke();
                //Debug.Log("ProximityState");
                break;
            case InteractableState.ContactState:
                contactEvent.Invoke();
                //Debug.Log("ContactState");
                break;
            case InteractableState.ActionState:
                actionEvent.Invoke();
                //Debug.Log("ActionState");
                break;
            case InteractableState.Default:
                //Debug.Log("defaultEvent");
                defaultEvent.Invoke();
                break;
        }

        //if (state.NewInteractableState == InteractableState.ProximityState)
        //{
        //    proximityEvent.Invoke();
        //    Debug.Log("ProximityState");
        //}
        //else if (state.NewInteractableState == InteractableState.ContactState)
        //{
        //    contactEvent.Invoke();
        //    Debug.Log("ContactState");
        //}
        //else if (state.NewInteractableState == InteractableState.ActionState)
        //{
        //    actionEvent.Invoke();
        //    Debug.Log("ActionState");
        //}
        //else
        //{
        //    Debug.Log("defaultEvent");
        //    defaultEvent.Invoke();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
