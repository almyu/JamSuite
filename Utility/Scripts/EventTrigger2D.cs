using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour {
    
    public UnityEvent onTriggerEnter = null;
    public UnityEvent onTriggerExit = null;
    public UnityEvent onTriggerStay = null;
    
    public string[] tagsExcluded = null;
    
    private void FilterAndInvoke(Collider2D collider, UnityEvent colliderEvent) {
        if (tagsExcluded != null) {
            for (int i = 0; i < tagsExcluded.Length; ++i) {
                if (collider.gameObject.tag == tagsExcluded[i]) return;
            }
        }
        
        colliderEvent.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerEnter); }
    private void OnTriggerStay2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerStay); }
    private void OnTriggerExit2D(Collider2D collider) { FilterAndInvoke(collider, onTriggerExit); }
}
