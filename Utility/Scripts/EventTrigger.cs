using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour {
    
    public UnityEvent onTriggerEnter = null;
    public UnityEvent onTriggerExit = null;
    public UnityEvent onTriggerStay = null;
    
    public string[] tagsExcluded = null;
    
    private void FilterAndInvoke(Collider collider, UnityEvent colliderEvent) {
        if (tagsExcluded != null) {
            for (int i = 0; i < tagsExcluded.Length; ++i) {
                if (collider.gameObject.tag == tagsExcluded[i]) return;
            }
        }
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter(Collider collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay(Collider collider) { FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit(Collider collider) { FilterAndInvoke(collider, onTriggerExit); }
}
