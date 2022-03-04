using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Transform target;

    [Header("Stats")]
    [SerializeField]
    private Vector2 deadZone;
    [SerializeField]
    private float upRotationClamp;
    [SerializeField]
    private float downRotationClamp;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        self.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x -= Screen.width / 2;
            mousePosition.y -= Screen.height / 2;

            if ((mousePosition.y < 0.0f && self.position.y >= upRotationClamp) || 
                (mousePosition.y > 0.0f && self.position.y <= downRotationClamp)) return;

            mousePosition.x /= 100;
            mousePosition.y /= -75;
            if (mousePosition.x < deadZone.x && mousePosition.x > -deadZone.x)
                mousePosition.x = 0.0f;
            if (mousePosition.y < deadZone.y && mousePosition.y > -deadZone.y)
                mousePosition.y = 0.0f;

            self.LookAt(target);
            self.Translate(mousePosition * speed * Time.deltaTime);
        }
    }
}
