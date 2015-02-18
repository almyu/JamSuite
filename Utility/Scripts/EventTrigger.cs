using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour {
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;
    
    public string[] tagsExcluded;
    
    private void FilterAndInvoke(Collider collider, UnityEvent colliderEvent) {
        for (int i = 0; i < tagsExcluded.Length; ++i) {
            if (collider.gameObject.tag == tagsExcluded[i]) return;
        }
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter(Collider collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay(Collider collider) { FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit(Collider collider) { FilterAndInvoke(collider, onTriggerExit); }
}
