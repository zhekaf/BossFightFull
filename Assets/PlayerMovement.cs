using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Transform cameraT;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.2f;
    CharacterController colider;
    public float jumpHeight = 5.5f;
    public float gravity = -12f;
    float velocityY;

    void Start()
    {
        cameraT = Camera.main.transform;
        colider = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    
    }

    void PlayerController()
    {
        Vector3 playerController = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 pControl = playerController.normalized;
        if (pControl != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(pControl.x, pControl.z) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        float speed = 10 * playerController.magnitude;
        Vector3 velocity = transform.forward * speed + Vector3.up * velocityY;
        colider.Move (velocity * Time.deltaTime);
        velocityY += Time.deltaTime * gravity;
        if (colider.isGrounded)
        {
            velocityY = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (colider.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }
}
