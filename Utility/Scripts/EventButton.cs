using UnityEngine;
using UnityEngine.Events;

public class EventButton : MonoBehaviour {

    public string buttonName;

    public UnityEvent getButton;
    public UnityEvent getButtonDown;
    public UnityEvent getButtonUp;
    
    private void Update () {
        if (Input.GetButton(buttonName)) getButton.Invoke();
        if (Input.GetButtonUp(buttonName)) getButtonUp.Invoke();
        if (Input.GetButtonDown(buttonName)) getButtonDown.Invoke();
    }
}
