using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private CameraPanning cameraPanning;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float zoomMax;

    private Vector3 _startPos;
    public Vector3 startPos { set { _startPos = value; } }
    private float startZ;

    private void Start()
    {
        self.position *= Grid.Instance.width / 10;
        _startPos = self.position;
        startZ = self.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0.0f)
        {
            if (self.position.z >= zoomMax) return;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = speed;
            self.position = Camera.main.ScreenToWorldPoint(mousePos);
            cameraPanning.posBeforePan = self.position;
        }
        else if (Input.mouseScrollDelta.y < 0.0f)
        {
            _startPos.z = startZ;
            if (self.position.z <= _startPos.z) return;
            self.position = Vector3.MoveTowards(self.position, _startPos, speed);
            cameraPanning.posBeforePan = self.position;
        }
    }
}
