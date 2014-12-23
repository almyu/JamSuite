using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class InterstageFade : MonoBehaviour {

    public AnimationCurve inTransition = AnimationCurve.EaseInOut(0f, 0f, 0.5f, 1f);
    public AnimationCurve outTransition = AnimationCurve.EaseInOut(0f, 1f, 0.5f, 0f);

    public void FadeToLevel(string level) {
        StartCoroutine(DoFade(level));
    }

    private IEnumerator DoFade(string level) {
        DontDestroyOnLoad(gameObject);
        yield return StartCoroutine(DoAnimate(inTransition));

        Application.LoadLevel(level);
        yield return StartCoroutine(DoAnimate(outTransition));

        Destroy(gameObject);
    }

    private IEnumerator DoAnimate(AnimationCurve curve) {
        var renderer = GetComponent<SpriteRenderer>();
        var duration = curve[curve.length - 1].time;

        for (var t = 0f; t <= duration; t += Mathf.Max(0.001f, Time.deltaTime)) {
            renderer.color = renderer.color.WithA(curve.Evaluate(t));
            yield return null;
        }
    }
}
