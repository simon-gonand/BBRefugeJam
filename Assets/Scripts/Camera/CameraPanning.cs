using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform self;
    [SerializeField]
    private GroundRotation rotation;

    [Header("Stats")]
    [SerializeField]
    private Vector3 panClamp;

    private Vector3 posBeforePan;

    private void Start()
    {
        posBeforePan = self.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x -= Screen.width / 2;
            mousePosition.y -= Screen.height / 2;

            mousePosition /= 100.0f;

            if ((mousePosition.x < rotation.deadZone.x && mousePosition.x > -rotation.deadZone.x) ||
                (mousePosition.x < 0.0f && self.position.x <= posBeforePan.x - panClamp.x) ||
                (mousePosition.x > 0.0f && self.position.x >= posBeforePan.x + panClamp.x))
                mousePosition.x = 0.0f;
            if ((mousePosition.y < rotation.deadZone.y && mousePosition.y > -rotation.deadZone.y) ||
                (mousePosition.y < 0.0f && self.position.y <= posBeforePan.y - panClamp.y) ||
                (mousePosition.y > 0.0f && self.position.y >= posBeforePan.y + panClamp.y))
                mousePosition.y = 0.0f;

            self.Translate(mousePosition * Time.deltaTime);
        }
    }
}
