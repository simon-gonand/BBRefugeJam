using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Transform zRotateAround;

    [Header("Stats")]
    public Vector2 deadZone;
    [SerializeField]
    private float upRotationClamp;
    [SerializeField]
    private float downRotationClamp;
    [SerializeField]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x -= Screen.width / 2;
            mousePosition.y -= Screen.height / 2;

            if ((mousePosition.y < 0.0f && self.eulerAngles.z > 90.0f && -(360 - self.eulerAngles.z) <= upRotationClamp) || 
                (mousePosition.y > 0.0f && self.eulerAngles.z >= downRotationClamp)) return;

            mousePosition.x /= 100;
            mousePosition.y /= 75;
            if (mousePosition.x < deadZone.x && mousePosition.x > -deadZone.x)
                mousePosition.x = 0.0f;
            if (mousePosition.y < deadZone.y && mousePosition.y > -deadZone.y)
                mousePosition.y = 0.0f;

            self.RotateAround(self.position, zRotateAround.forward, mousePosition.y * speed * Time.deltaTime);

            mousePosition.y = mousePosition.x;
            mousePosition.x = 0.0f;
            self.Rotate(mousePosition * speed * Time.deltaTime);
        }
    }
}
