using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private GroundRotation ground;
    [SerializeField]
    private CameraZoom zoom;
    [SerializeField]
    private float upClamp;
    [SerializeField]
    private float downClamp;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isApocalypseLaunched)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Physics.Raycast(self.position, self.forward, out hit);
                target = hit.point;
            }
            if (Input.GetMouseButton(1))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.x -= Screen.width / 2;
                mousePosition.y -= Screen.height / 2;

                if ((mousePosition.y < 0.0f && self.position.y >= upClamp) ||
                    (mousePosition.y > 0.0f && self.position.y <= downClamp))
                {
                    mousePosition.y = 0.0f;
                }

                mousePosition /= 100;
                mousePosition.y = -mousePosition.y;
                if (mousePosition.x < ground.deadZone.x && mousePosition.x > -ground.deadZone.x)
                    mousePosition.x = 0.0f;
                if (mousePosition.y < ground.deadZone.y && mousePosition.y > -ground.deadZone.y)
                    mousePosition.y = 0.0f;

                self.LookAt(target);
                self.Translate(mousePosition * ground.mouseSpeed * Time.deltaTime);

                zoom.startPos = self.position;
            }
        }
    }
}
