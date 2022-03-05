using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] AnimationCurve attenuation;
    [SerializeField] Transform t;
    [SerializeField] float size = 1f;
    bool isShaking = false;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    public void shake(float duration)
    {
        if (isShaking) return;
        StartCoroutine(shakeRoutine(duration));
    }

    IEnumerator shakeRoutine(float duration)
    {
        isShaking = true;

        Vector3 startPos = t.position;
        float time = 0;

        while (time<duration)
        {
            Vector3 randPoint = Random.insideUnitSphere * size;
            transform.position = startPos + randPoint * attenuation.Evaluate(time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }
}
