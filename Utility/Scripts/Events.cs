using UnityEngine;
using UnityEngine.Events;
using System;

namespace JamSuite.Events {

    [Serializable] public class BoolEvent : UnityEvent<bool> {}
    [Serializable] public class IntEvent : UnityEvent<int> {}
    [Serializable] public class FloatEvent : UnityEvent<float> {}
    [Serializable] public class Vector2Event : UnityEvent<Vector2> {}
    [Serializable] public class Vector3Event : UnityEvent<Vector3> {}
    [Serializable] public class Vector4Event : UnityEvent<Vector4> {}
    [Serializable] public class RectEvent : UnityEvent<Rect> {}
    [Serializable] public class StringEvent : UnityEvent<string> {}
}
