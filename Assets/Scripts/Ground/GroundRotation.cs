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
    private float mouseSpeed;
    [SerializeField]
    private float clickRotationSpeed;

    private bool canRotate = true;
    private float[] rotationBuffer = { 0.0f, 0.0f, 0.0f, 0.0f };

    public void Rotate(float angle)
    {
        for (int i = 0; i < rotationBuffer.Length; ++i)
        {
            if (rotationBuffer[i] == 0.0f)
            {
                rotationBuffer[i] = angle;
                return;
            }
        }
    }

    private void LerpRotation()
    {
        if (canRotate)
        {
            for (int i = 0; i < rotationBuffer.Length; ++i)
            {
                if (rotationBuffer[i] != 0.0f)
                {
                    StartCoroutine(LerpRotationCoroutine(rotationBuffer[i]));
                    rotationBuffer[i] = 0.0f;
                    return;
                }
            }
        }
    }

    private IEnumerator LerpRotationCoroutine(float angle)
    {
        canRotate = false;
        float t = 0.0f;
        while (t < (angle > 0.0f ? angle : -angle))
        {
            t += Time.deltaTime * clickRotationSpeed;
            if (angle > 0.0f)
                self.Rotate(Vector3.up, clickRotationSpeed * Time.deltaTime);
            else
                self.Rotate(Vector3.up, -clickRotationSpeed * Time.deltaTime);
            yield return null;
        }

        canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x -= Screen.width / 2;
            mousePosition.y -= Screen.height / 2;

            mousePosition.x /= 100;
            mousePosition.y /= 75;
            if (mousePosition.x < deadZone.x && mousePosition.x > -deadZone.x)
                mousePosition.x = 0.0f;
            if (mousePosition.y < deadZone.y && mousePosition.y > -deadZone.y)
                mousePosition.y = 0.0f;

            if (!(mousePosition.y < 0.0f && self.eulerAngles.z > 90.0f && -(360 - self.eulerAngles.z) <= upRotationClamp) ||
                !(mousePosition.y > 0.0f && self.eulerAngles.z >= downRotationClamp))            
                self.RotateAround(self.position, zRotateAround.forward, mousePosition.y * mouseSpeed * Time.deltaTime);

            mousePosition.y = -mousePosition.x;
            mousePosition.x = 0.0f;
            self.Rotate(mousePosition * mouseSpeed * Time.deltaTime);
        }

        LerpRotation();
    }
}
