using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour {
    
    public LayerMask filter = 1; // Default
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;
    
    private void FilterAndInvoke(Collider2D collider, UnityEvent colliderEvent) {
        int mask = 1 << collider.gameObject.layer;
        if ((mask & filter.value) == 0) return;
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerExit); }
}
