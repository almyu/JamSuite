using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour {
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;
    
    public string[] tagsExcluded;
    
    private void FilterAndInvoke(Collider2D collider, UnityEvent colliderEvent) {
        for (int i = 0; i < tagsExcluded.Length; ++i) {
            if (collider.gameObject.tag == tagsExcluded[i]) return;
        }
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerExit); }
}
