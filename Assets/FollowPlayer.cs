using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public float cameraDistance = 3.5f;
    public float mouseSense = 10f;
    float yaw;
    float pitch;
    public float minP = -40f;
    public float maxP = 85f;
    public bool lockCursor;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraController();

    }

    void CameraController()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSense;
        pitch -= Input.GetAxis("Mouse Y") * mouseSense;
        pitch = Mathf.Clamp(pitch, minP, maxP);
        Vector2 cameraController = new Vector2(pitch, yaw);
        transform.eulerAngles = cameraController;
        transform.position = Player.position - transform.forward * cameraDistance;
    }
}
