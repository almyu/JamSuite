using UnityEngine;
using UnityEngine.Events;

public class EventMouse : MonoBehaviour {
    
    public UnityEvent onMouseEnter = null;
    public UnityEvent onMouseExit = null;
    public UnityEvent onMouseOver = null;
    public UnityEvent onMouseDown = null;
    public UnityEvent onMouseUp = null;
    
    private void OnMouseEnter() { onMouseEnter.Invoke(); }
    private void OnMouseExit() { onMouseExit.Invoke(); }
    private void OnMouseOver() { onMouseOver.Invoke(); }
    private void OnMouseDown() { onMouseDown.Invoke(); }
    private void OnMouseUp() { onMouseUp.Invoke(); }
}
