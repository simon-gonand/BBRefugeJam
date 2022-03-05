using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeteorCS : MonoBehaviour
{
    [SerializeField] GameObject destroyFeedbacks;
    [SerializeField] float destroyFeedbackDuration;

    public void Collide()
    {
        destroyFeedbacks.transform.parent = transform.parent.transform;
        destroyFeedbacks.SetActive(true);
        Destroy(destroyFeedbacks, destroyFeedbackDuration);
        Destroy(this.gameObject);
    }
}
