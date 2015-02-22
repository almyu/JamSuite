using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour {
    
    public LayerMask filter = 1; // Default
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;

    public string buttonName; // if empty - invoke without button
    
    private void FilterAndInvoke(Collider collider, UnityEvent colliderEvent) {
        int mask = 1 << collider.gameObject.layer;
        if ((mask & filter.value) == 0) return;
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter(Collider collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay(Collider collider) { if(buttonName == "" || Input.GetButtonUp(buttonName)) FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit(Collider collider) { FilterAndInvoke(collider, onTriggerExit); }
}
