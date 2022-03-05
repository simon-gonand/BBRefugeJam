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
    public float upRotationClamp;
    public float downRotationClamp;
    public float rotateSpeed;

    [SerializeField]
    private float clickRotationSpeed;

    private float rotationAngle = 0.0f;

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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0.0f)
        {
            self.Rotate(Vector3.up * horizontalInput * rotateSpeed * Time.deltaTime);
        }

        if (verticalInput != 0.0f)
        {
            if ((verticalInput > 0.0f && rotationAngle > upRotationClamp) ||
                (verticalInput < 0.0f && rotationAngle < downRotationClamp))
            {
                rotationAngle += -verticalInput * Time.deltaTime * rotateSpeed;
                self.RotateAround(self.position, zRotateAround.forward, -verticalInput * Time.deltaTime * rotateSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
            Rotate(-90.0f);
        if (Input.GetKeyDown(KeyCode.E))
            Rotate(90.0f);

        LerpRotation();
    }
}
