using UnityEngine;
using UnityEngine.Events;

public class EventMouse : MonoBehaviour {
    
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseOver;
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;
    
    private void OnMouseEnter() { onMouseEnter.Invoke(); }
    private void OnMouseExit() { onMouseExit.Invoke(); }
    private void OnMouseOver() { onMouseOver.Invoke(); }
    private void OnMouseDown() { onMouseDown.Invoke(); }
    private void OnMouseUp() { onMouseUp.Invoke(); }
}
