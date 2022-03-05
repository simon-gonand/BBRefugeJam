using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private CameraPanning cameraPanning;

    private Vector3 startPos;

    private void Start()
    {
        startPos = self.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0.0f)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1.0f;
            self.position = Camera.main.ScreenToWorldPoint(mousePos);
            cameraPanning.posBeforePan = self.position;
        }
        else if (Input.mouseScrollDelta.y < 0.0f)
        {
            if (self.position.z <= startPos.z) return;
            self.position = Vector3.MoveTowards(self.position, startPos, 1.0f);
            cameraPanning.posBeforePan = self.position;
        }
    }
}
