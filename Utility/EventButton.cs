using UnityEngine;
using UnityEngine.Events;

public class EventButton : MonoBehaviour
{
    public string buttonName;

    public UnityEvent onButtonDown;
    public UnityEvent onButtonStay;
    public UnityEvent onButtonUp;

    private void Update() {
        if (Input.GetButtonDown(buttonName)) onButtonDown.Invoke();
        if (Input.GetButton(buttonName)) onButtonStay.Invoke();
        if (Input.GetButtonUp(buttonName)) onButtonUp.Invoke();
    }
}
